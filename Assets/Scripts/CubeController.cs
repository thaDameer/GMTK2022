using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

[RequireComponent(typeof(CubePhysics))]
public class CubeController : MonoBehaviour
{
    [SerializeField] private float movementSpeed = 10f;

    private DiceSide currentLeft, currentRight;

    [SerializeField] private DiceSide one, two, three, four, five, six;
    public delegate void DiceSideChanged(DiceSide left, DiceSide right);
    public static event DiceSideChanged OnDiceSideChanged;

    private CubePhysics cubePhysics;
    private bool isMoving;

    ITile currentTile;

    private void Start()
    {
        BaseTile.OnTileComplete += ClearTile;
        cubePhysics = GetComponent<CubePhysics>();
        GetRelativeNumberPosition();
    }

    private void Update()
    {
        CubeMovement();
    }

    private bool IsPathBlocked(Vector3 dir)
    {
        RaycastHit hit;
        var pathDir = dir;
        Debug.DrawRay(transform.position, pathDir * 0.55f, Color.magenta, 1);
        if (Physics.Raycast(transform.position, pathDir, out hit, 0.55f))
        {
            var obstacle = hit.collider.GetComponent<IObstacle>();
            if (obstacle != null)
                obstacle.Collide(hit.point);

            if (hit.collider.gameObject.tag == "Obstacle")
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
        if (currentLeft != leftDiceSide || rightDiceSide != currentRight)
        {
            currentLeft = leftDiceSide;
            currentRight = rightDiceSide;
            OnDiceSideChanged?.Invoke(currentLeft, currentRight);
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
        Debug.Log(diceSideDir);
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
        isMoving = true;

        for (int i = 0; i < 90 / movementSpeed; i++)
        {
            transform.RotateAround(anchor, axis, movementSpeed);
            yield return new WaitForSeconds(0.01f);
        }
        GetRelativeNumberPosition();
        UpdateTile();
        isMoving = false;
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
            transform.DOShakePosition(0.2f, dir, 10, 15, false, true).SetEase(Ease.OutQuint);
            return true;
        }
        else return false;
    }

    private void ClearTile()
    {
        currentTile = null;
    }
    

    private void OnDestroy()
    {
        BaseTile.OnTileComplete -= ClearTile;
    }
}