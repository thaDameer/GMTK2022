using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public abstract class BaseTile : MonoBehaviour, ITile
{
    public abstract void EnterTile();

    public static Action OnTileComplete;

    public abstract void TileAction();

}
