using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Unity.Mathematics;
using UnityEngine;

[RequireComponent(typeof(CubePhysics))]
public class CubeController : MonoBehaviour
{
    [SerializeField] private LayerMask obstacleLayer;
    [SerializeField] private float movementSpeed = 10f;
    [SerializeField] private float deathBedHeight = -10f;

    private DiceSide currentLeft, currentRight, currentJump;

    [SerializeField] private DiceSide one, two, three, four, five, six;
    public delegate void DiceSideChanged(DiceSide left,DiceSide jump, DiceSide right);
    public static event DiceSideChanged OnDiceSideChanged;

    private CubePhysics cubePhysics;
    private bool isMoving, levelStarted;

    public bool isActive = true; 

    ITile currentTile;

    [SerializeField] private AudioClip landSound, preSound, jumpSound;
    [Range(0, 1)]
    [SerializeField] private float walkVolume; 
    

    private void Start()
    {
        BaseTile.OnTileComplete += ClearTile;
        EventBroker.Instance.OnStartLevel += StartLevel;
        cubePhysics = GetComponent<CubePhysics>();
        GetRelativeNumberPosition();
    }

    private void Update()
    {
        if (!isActive || !levelStarted) return;
        if (OnDeathBed()) return;
        
        CubeMovement();
    }

    private void StartLevel() => levelStarted = true;

    private bool OnDeathBed()
    {
        if (transform.position.y > deathBedHeight) return false;
        
        EventBroker.Instance.OnFailLevel?.Invoke();
        StartCoroutine(DestroyAfterSeconds(3f));
        return true;
    }

    private IEnumerator DestroyAfterSeconds(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        Destroy(gameObject);
    }

 
    private bool IsPathBlocked(Vector3 dir)
    {
        RaycastHit hit;
        var pathDir = dir;

        if (Physics.Raycast(transform.position, pathDir, out hit, 0.55f))
        {
            var obstacle = hit.collider.GetComponent<IObstacle>();
            if (obstacle != null)
                obstacle.Collide(hit.point);

            if (hit.collider.gameObject)
            {
                DoBlockAnimation(dir);
                //Maybe check for different obstacles
                return true;
            }
        }

        return false;
    }

    private void DoBlockAnimation(Vector3 direction)
    {
        isMoving = true;
        transform.DOShakeScale(0.3f, 0.1f, 3, 0.4f, true).SetEase(Ease.OutQuint).OnComplete((() =>
                isMoving = false));
    }
    private void CubeMovement()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1) || Input.GetKeyDown(KeyCode.Keypad1))
        {
            var relativeDir = GetRelativeDirection(one.Direction);
            TryExecuteMovementByDirection(relativeDir);
        }
        if (Input.GetKeyDown(KeyCode.Alpha2) || Input.GetKeyDown(KeyCode.Keypad2))
        {
            var relativeDir = GetRelativeDirection(two.Direction);
            TryExecuteMovementByDirection(relativeDir);
        }
        if (Input.GetKeyDown(KeyCode.Alpha3) || Input.GetKeyDown(KeyCode.Keypad3))
        {
            var relativeDir = GetRelativeDirection(three.Direction);

            TryExecuteMovementByDirection(relativeDir);
        }
        if (Input.GetKeyDown(KeyCode.Alpha4) || Input.GetKeyDown(KeyCode.Keypad4))
        {
            var relativeDir = GetRelativeDirection(four.Direction);

            TryExecuteMovementByDirection(relativeDir);
        }
        if (Input.GetKeyDown(KeyCode.Alpha5) || Input.GetKeyDown(KeyCode.Keypad5))
        {
            var relativeDir = GetRelativeDirection(five.Direction);
            TryExecuteMovementByDirection(relativeDir);
        }
        if (Input.GetKeyDown(KeyCode.Alpha6) || Input.GetKeyDown(KeyCode.Keypad6))
        {
            var relativeDir = GetRelativeDirection(six.Direction);
            TryExecuteMovementByDirection(relativeDir);
        }
    }

    private void GetRelativeNumberPosition()
    {
        var leftDiceSide = GetDiceSideByDirection(Vector3.left);
        var rightDiceSide = GetDiceSideByDirection(Vector3.right);
        var jumpSide = GetDiceSideByDirection(Vector3.down);
        if (currentLeft != leftDiceSide || rightDiceSide != currentRight || currentJump != jumpSide)
        {
            currentLeft = leftDiceSide;
            currentRight = rightDiceSide;
            currentJump = jumpSide;
            OnDiceSideChanged?.Invoke(currentLeft, currentJump, currentRight);
        }
    }

    private DiceSide GetDiceSideByDirection(Vector3 direction)
    {
        if (one.Direction == direction)
            return one;
        if (two.Direction == direction)
            return two;
        if (three.Direction == direction)
            return three;
        if (four.Direction == direction)
            return four;
        if (five.Direction == direction)
            return five;
        if (six.Direction == direction)
            return six;
        return null;
    }

    private Vector3 GetRelativeDirection(Vector3 diceSideDir)
    {
        if (diceSideDir == Vector3.forward)
            return Vector3.back;
        if (diceSideDir == Vector3.back)
            return Vector3.forward;
        if (diceSideDir == Vector3.up)
            return Vector3.down;
        if (diceSideDir == Vector3.down)
            return Vector3.up;
        if (diceSideDir == Vector3.right)
            return Vector3.left;
        return diceSideDir == Vector3.left ? Vector3.right : Vector3.zero;
    }
    
    private void TryExecuteMovementByDirection(Vector3 dir)
    {
        if (isMoving) return;
        if (IsPathBlocked(dir))
            return;
        if (IsTileRestricted(dir)) { currentTile.TileAction(); return; }

        if (IsJumpInput(dir))
        {
            PlaySound(jumpSound); 
            cubePhysics.TryJump();
            return;
        }
        var anchor = transform.position + (Vector3.down + dir) * 0.5f;
        var axis = Vector3.Cross(Vector3.up, dir);
        StartCoroutine(RollMovement(anchor, axis));
    }

    private bool IsJumpInput(Vector3 dir)
    {
        return dir == Vector3.up;
    }

    private IEnumerator RollMovement(Vector3 anchor, Vector3 axis)
    {
        SetIsMoving(true);
        bool diceIsRolling = axis != Vector3.zero;
        if (diceIsRolling)
            PlaySound(preSound);
        
        for (int i = 0; i < 90 / movementSpeed; i++)
        {
            transform.RotateAround(anchor, axis, movementSpeed);
            yield return new WaitForSeconds(0.01f);
        }
        if(diceIsRolling)
            PlaySound(landSound); 
        GetRelativeNumberPosition();
        UpdateTile();
        SetIsMoving(false);
    }

    private void SetIsMoving(bool value)
    {
        cubePhysics.PausePhysics = value;
        isMoving = value;
    }

    private void UpdateTile()
    {
        RaycastHit hit;

        if (Physics.Raycast(transform.position, Vector3.down, out hit, 0.55f))
        {
            var tile = hit.collider.gameObject.GetComponent<ITile>();
            if (tile != null)
            {
                currentTile = tile;
                currentTile.EnterTile();
                TryExecuteOnEnterTileAction(currentTile);
                if (currentTile is RotateTile) transform.DORotate(Vector3.up*90, 0.4f, RotateMode.WorldAxisAdd);
            }
            else currentTile = null;
        }

    }

    private void TryExecuteOnEnterTileAction(ITile iTile)
    {
        switch (iTile)
        {
            case Jam jam:
                break;
            case RotateTile oil:
                AscentToParadice();
                break;
            case NumberTile numberTile:
                var diceSide = GetDiceSideByDirection(Vector3.down);
                numberTile.TryMatchDiceSide(diceSide);
                break;
        }
    }
    private bool IsTileRestricted(Vector3 dir)
    {
        if (currentTile is Jam)
        {
            var pos = transform.position; 
            transform.DOShakePosition(0.2f, dir, 10, 0, false, true).SetEase(Ease.OutQuint).OnComplete(() =>
            {
                transform.position = pos;   
            }
            );
           
            return true;
        }
        else return false;
    }

    private void AscentToParadice()
    {
        transform.DOBlendableMoveBy(new Vector3(0, 10, 0), 10, false);
        transform.DOBlendableRotateBy(new Vector3(0, 1080, 0), 10, RotateMode.WorldAxisAdd);
        Debug.Log("ASCNED"); 
    }

    private void ClearTile()
    {
        currentTile = null;
    }
    
    private void PlaySound(AudioClip clip)
    {
        if (clip != null) AudioSource.PlayClipAtPoint(clip, transform.position, walkVolume); 
    }
    private void OnDestroy()
    {
        BaseTile.OnTileComplete -= ClearTile;
        EventBroker.Instance.OnStartLevel -= StartLevel;
    }
}