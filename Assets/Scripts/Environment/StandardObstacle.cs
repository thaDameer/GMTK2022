using DG.Tweening;
using UnityEngine;

public class StandardObstacle : ObstacleBase
{
    
    public override void Collide(Vector3 collisionPos)
    {
        if(bounceEffect)
            bounceEffect?.Play();   
        var pos = transform.position;
        transform.DOShakePosition(0.2f,0.1f,1,1f).SetEase(Ease.OutBounce).OnComplete(() => 
        {
            transform.position = pos;
        });
        if(particleSystem==null) return;
        particleSystem.transform.position = collisionPos;
        particleSystem.Play();
    }
}