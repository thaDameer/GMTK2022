using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NumberTile : BaseTile
{
    public enum Number
    {
        One = 1,
        Two = 2,
        Three = 3,
        Four = 4,
        Five = 5,
        Six = 6
    }

    [SerializeField] private Number _number;

    [SerializeField]private List<Image> _images = new List<Image>();
    private void OnValidate()
    {
        
    }

    public void TryMatchDiceSide(DiceSide diceSide)
    {
        if (diceSide.Number == (int)_number)
        {
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
