using DG.Tweening;
using UnityEngine;

public class NumberTileMovingObject : MonoBehaviour
{
    [SerializeField] private int moveInX;
    [SerializeField] private int moveInY;
    [SerializeField] private int moveInZ;

    private Vector3 movementPos => transform.position + new Vector3(moveInX, moveInY, moveInZ);
    
    [SerializeField] private float duration = 1;
    [SerializeField] private Ease ease = Ease.InQuint;
    
    public void MoveObject()
    {
        transform.DOMove(movementPos, duration).SetEase(ease);
    }
}