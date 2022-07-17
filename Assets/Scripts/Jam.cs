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
    }

    public override void TileAction()
    {
        Stickiness -= 1;
        AudioSource.PlayClipAtPoint(stickClip, transform.position);
        particles.Emit(ParticleCount); 
        if (Stickiness <= 0) OnTileComplete.Invoke();
    }

}

