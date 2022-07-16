using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateTile : BaseTile
{
    public override void EnterTile()
    {
        
    }

    public override void TileAction()
    {
        OnTileComplete.Invoke(); 
    }

}
