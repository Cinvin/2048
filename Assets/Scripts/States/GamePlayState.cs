using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamePlayState : BaseState{

    public override void OnActivate()
    {
        Managers.UI.OnActivateUI(UIType.InGame);
        Managers.Slot.ShowSlots();
        Managers.Block.Spawn();
        Managers.Block.Spawn();
        Managers.Block.Movable = true;
    }
    public override void DeActivate()
    {
        Managers.UI.DeActivateUI(UIType.InGame);
        Managers.Block.Movable = false;
        Managers.Slot.HideSlots();
        Managers.Block.CleanBlocks();
        Managers.Stats.ResetCurrentScore();
    }
    public override void OnUpdate()
    {
        
    }
}
