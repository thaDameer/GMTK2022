using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalTile : BaseTile
{
    public override void EnterTile()
    {
        EventBroker.Instance.OnCompleteLevel?.Invoke();
    }

    public override void TileAction()
    {
        
    }
}
