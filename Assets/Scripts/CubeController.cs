using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeController : MonoBehaviour
{

    [SerializeField] private float movementSpeed = 10f;

    private Vector3 dir6 => -transform.forward;
    private Vector3 dir1 => transform.forward;

    private Vector3 dir4 => transform.up;
    private Vector3 dir3 => -transform.up;

    private Vector3 dir2 => -transform.right;
    private Vector3 dir5 => transform.right;
    
    private bool isMoving;

    private void Awake()
    {
       
    }

    private void Update()
    {
        DebugRays();


        if (Input.GetKeyDown(KeyCode.Alpha1) || Input.GetKeyDown(KeyCode.Keypad1))
        {
            var relativeDir = GetRelativeDirection(dir1);
            SetMovementByDirection(relativeDir);
        }
        if (Input.GetKeyDown(KeyCode.Alpha2) || Input.GetKeyDown(KeyCode.Keypad2))
        {
            var relativeDir = GetRelativeDirection(dir2);
            SetMovementByDirection(relativeDir);
        }
        if (Input.GetKeyDown(KeyCode.Alpha3) || Input.GetKeyDown(KeyCode.Keypad3))
        {
            var relativeDir = GetRelativeDirection(dir3);
        
            SetMovementByDirection(relativeDir);
        }
        if (Input.GetKeyDown(KeyCode.Alpha4) || Input.GetKeyDown(KeyCode.Keypad4))
        {
            var relativeDir = GetRelativeDirection(dir4);
            
            SetMovementByDirection(relativeDir);
        }
        if (Input.GetKeyDown(KeyCode.Alpha5) || Input.GetKeyDown(KeyCode.Keypad5))
        {
            var relativeDir = GetRelativeDirection(dir5);
            SetMovementByDirection(relativeDir);
        }
        if (Input.GetKeyDown(KeyCode.Alpha6) || Input.GetKeyDown(KeyCode.Keypad6))
        {
            var relativeDir = GetRelativeDirection(dir6);
            SetMovementByDirection(relativeDir);
        }
        return;
        if(Input.GetKeyDown(KeyCode.UpArrow)) SetMovementByDirection(Vector3.forward);
        if(Input.GetKeyDown(KeyCode.DownArrow)) SetMovementByDirection(Vector3.back);
        if(Input.GetKeyDown(KeyCode.LeftArrow)) SetMovementByDirection(Vector3.left);
        if(Input.GetKeyDown(KeyCode.RightArrow)) SetMovementByDirection(Vector3.right);
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
    private void DebugRays()
    {
        Debug.DrawRay(transform.position,dir1 * 2, Color.cyan);
        Debug.DrawRay(transform.position,dir6 *2,Color.blue);
        
        Debug.DrawRay(transform.position,dir2 *2, Color.green);
        Debug.DrawRay(transform.position,dir5 *2, Color.magenta);
        
        Debug.DrawRay(transform.position,dir4 *2, Color.red);
        Debug.DrawRay(transform.position,dir3 *2, Color.yellow);

    }
    private void SetMovementByDirection(Vector3 dir)
    {
        if(isMoving) return;
        
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
        
        isMoving = false;
    }
}

