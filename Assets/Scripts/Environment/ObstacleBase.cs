using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ObstacleBase : MonoBehaviour,IObstacle
{
    [SerializeField] protected ParticleSystem particleSystem;
    [SerializeField] protected AudioSource bounceEffect;
    public virtual void Collide(Vector3 collisionPos) { }
}

public interface IObstacle
{
    public abstract void Collide(Vector3 collisionPos);
}