using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class CubeController : MonoBehaviour
{

    [SerializeField] private float movementSpeed = 10f;

    private DiceSide currentLeft, currentRight;

    [SerializeField] private DiceSide one, two, three, four, five, six;
    public delegate void DiceSideChanged(DiceSide left, DiceSide right);
    public static event DiceSideChanged OnDiceSideChanged;
    
    private bool isMoving;
    [SerializeField] private float gravity;

    private void Start()
    {
      
    }

    private void Update()
    {
        //DebugRays();
        if (!IsGrounded())
        {
            //ADD GRAVITY FORCE
        }    
        CubeMovement();
        Debug.Log(IsGrounded());    
      
        return;
        if(Input.GetKeyDown(KeyCode.UpArrow)) TrySetMovementByDirection(Vector3.forward);
        if(Input.GetKeyDown(KeyCode.DownArrow)) TrySetMovementByDirection(Vector3.back);
        if(Input.GetKeyDown(KeyCode.LeftArrow)) TrySetMovementByDirection(Vector3.left);
        if(Input.GetKeyDown(KeyCode.RightArrow)) TrySetMovementByDirection(Vector3.right);
    }

    private void AddFallGravity()
    {
        //transform.position = Vector3.
    }

    private bool IsPathBlocked(Vector3 dir)
    {
        RaycastHit hit;
        var pathDir = dir;
        Debug.DrawRay(transform.position,pathDir * 0.55f,Color.magenta,1);
        if (Physics.Raycast(transform.position, pathDir, out hit,0.55f))
        {

            if (hit.collider.gameObject.tag == "Obstacle")
            {
                DoBlockAnimation();
                //Maybe check for different obstacles
                return true;
            }
           
        }

        return false;
    }

    private void DoBlockAnimation()
    {
        transform.DOShakeScale(0.3f,0.1f,3,0.4f,true).SetEase(Ease.OutQuint);
    }
    private void CubeMovement()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1) || Input.GetKeyDown(KeyCode.Keypad1))
        {
            var relativeDir = GetRelativeDirection(one.Direction);
            TrySetMovementByDirection(relativeDir);
        }
        if (Input.GetKeyDown(KeyCode.Alpha2) || Input.GetKeyDown(KeyCode.Keypad2))
        {
            var relativeDir = GetRelativeDirection(two.Direction);
            TrySetMovementByDirection(relativeDir);
        }
        if (Input.GetKeyDown(KeyCode.Alpha3) || Input.GetKeyDown(KeyCode.Keypad3))
        {
            var relativeDir = GetRelativeDirection(three.Direction);
        
            TrySetMovementByDirection(relativeDir);
        }
        if (Input.GetKeyDown(KeyCode.Alpha4) || Input.GetKeyDown(KeyCode.Keypad4))
        {
            var relativeDir = GetRelativeDirection(four.Direction);
            
            TrySetMovementByDirection(relativeDir);
        }
        if (Input.GetKeyDown(KeyCode.Alpha5) || Input.GetKeyDown(KeyCode.Keypad5))
        {
            var relativeDir = GetRelativeDirection(five.Direction);
            TrySetMovementByDirection(relativeDir);
        }
        if (Input.GetKeyDown(KeyCode.Alpha6) || Input.GetKeyDown(KeyCode.Keypad6))
        {
            var relativeDir = GetRelativeDirection(six.Direction);
            TrySetMovementByDirection(relativeDir);
        }
    }
    private bool IsGrounded()
    {
        RaycastHit hit;
        if(Physics.Raycast(transform.position,Vector3.down,0.55f))
        {
            return true;
        }
        return false;
    }
    private void GetRelativeNumberPosition()
    {
        var leftDiceSide = GetDiceSideByDirection(Vector3.left);
        var rightDiceSide = GetDiceSideByDirection(Vector3.right);
        if (currentLeft != leftDiceSide || rightDiceSide != currentRight)
        {
            currentLeft = leftDiceSide;
            currentRight = rightDiceSide;
            OnDiceSideChanged?.Invoke(currentLeft,currentRight);
        }
        //Debug.Log("left: "+leftDiceSide.Number + ". right: "+ rightDiceSide.Number);
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
    private void DebugRays()
    {
        Debug.DrawRay(transform.position,one.Direction * 2, Color.cyan);
        Debug.DrawRay(transform.position,two.Direction *2,Color.blue);
        
        Debug.DrawRay(transform.position,three.Direction *2, Color.green);
        Debug.DrawRay(transform.position,four.Direction *2, Color.magenta);
        
        Debug.DrawRay(transform.position,five.Direction *2, Color.red);
        Debug.DrawRay(transform.position,six.Direction *2, Color.yellow);

    }
    private void TrySetMovementByDirection(Vector3 dir)
    {
        if(isMoving) return;
        if(IsPathBlocked(dir))
            return;
        var anchor = transform.position + (Vector3.down + dir) * 0.5f;
        var axis = Vector3.Cross(Vector3.up, dir);
        StartCoroutine(RollMovement(anchor, axis));
    }

    private IEnumerator RollMovement(Vector3 anchor, Vector3 axis)
    {
        isMoving = true;
        
        for (int i = 0; i < 90 / movementSpeed; i++)
        {
            transform.RotateAround(anchor,axis,movementSpeed);
            yield return new WaitForSeconds(0.01f);
        }
        GetRelativeNumberPosition();
        isMoving = false;
    }

    private void UpdateDiceRelation()
    {
        
    }
    
}