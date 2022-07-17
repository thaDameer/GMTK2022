using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jam : BaseTile
{
    //Stickiness determines how many times one needs to press the button to escape
    public float Stickiness;
    [SerializeField] private AudioClip stickClip;
    [SerializeField] private ParticleSystem particles;
    public int ParticleCount = 10; 
    public override void EnterTile()
    {
        Stickiness = (int)UnityEngine.Random.Range(1, 6);
        PlayAudio();
        EmitParticles(); 
    }

    public override void TileAction()
    {
        Stickiness -= 1;
        EmitParticles();
        PlayAudio(); 
         
        if (Stickiness <= 0) OnTileComplete.Invoke();
    }

    public void EmitParticles()
    {
        if (particles != null) particles.Emit(ParticleCount);
    }

    public void PlayAudio()
    {
        if (stickClip != null) AudioSource.PlayClipAtPoint(stickClip, transform.position);
    }

}

