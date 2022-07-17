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
        PlayAudio();
        EmitParticles(); 
    }

    public override void TileAction()
    {
        PlayAudio();
        EmitParticles(); 
        OnTileComplete.Invoke(); 
    }

    public void EmitParticles()
    {
        if (particles != null) particles.Emit(ParticleCount);
    }

    public void PlayAudio()
    {
        if (clip != null) AudioSource.PlayClipAtPoint(clip, transform.position);
    }

}
