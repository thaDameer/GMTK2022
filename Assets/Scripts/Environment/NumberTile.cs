using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class NumberTile : BaseTile
{
    [SerializeField] private ParticleSystem successParticle;
    [SerializeField] private AudioSource successAudio;

    [SerializeField] private NumberTileMovingObject movingObject;
    public enum Number
    {
        One = 1,
        Two = 2,
        Three = 3,
        Four = 4,
        Five = 5,
        Six = 6
    }

    private bool isUnlocked = false;
    [SerializeField] private Number _number;
    [SerializeField]private List<Transform> _images = new List<Transform>();
    private void OnValidate()
    {
        for (int i = 0; i < _images.Count; i++)
        {
            var count = i + 1;
            var image = _images[i];
            if((int)_number == count)
                image.gameObject.SetActive(true);
            else
            {
                image.gameObject.SetActive(false);
            }
        }
    }
    
    public void TryMatchDiceSide(DiceSide diceSide)
    {
        if(isUnlocked) return;
        if (diceSide.Number == (int)_number)
        {
            if(successAudio)
                successAudio.Play();
            if(successParticle)
                successParticle.Play();
            if(movingObject)
                movingObject.MoveObject();
            isUnlocked = true;
            Debug.Log("ITS A MATCH!");
        }
    }
    public override void EnterTile()
    {
        
    }

    public override void TileAction()
    {
        
    }
}