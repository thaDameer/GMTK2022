using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalTile : BaseTile
{
    public override void EnterTile()
    {
        GameManager.Instance.levelCleared = true;
        GameManager.Instance.OnLevelClear(); 
    }

    public override void TileAction()
    {
        
    }

}
