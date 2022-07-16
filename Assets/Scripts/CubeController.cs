using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeController : MonoBehaviour
{

    [SerializeField] private float movementSpeed = 10f;
    
    
    private bool isMoving;
    

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.UpArrow)) SetMovementByDirection(Vector3.forward);
        if(Input.GetKeyDown(KeyCode.DownArrow)) SetMovementByDirection(Vector3.back);
        if(Input.GetKeyDown(KeyCode.LeftArrow)) SetMovementByDirection(Vector3.left);
        if(Input.GetKeyDown(KeyCode.RightArrow)) SetMovementByDirection(Vector3.right);
    }

    private void SetMovementByDirection(Vector3 dir)
    {
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
