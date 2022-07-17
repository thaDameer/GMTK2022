using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateTile : BaseTile
{
    [SerializeField] private AudioClip clip;
    [SerializeField] private ParticleSystem particles;
    public int ParticleCount = 10; 
    public override void EnterTile()
    {
        
    }

    public override void TileAction()
    {
        if (clip != null) AudioSource.PlayClipAtPoint(clip, transform.position);
        if (particles != null) particles.Emit(ParticleCount); 
        OnTileComplete.Invoke(); 
    }

}
