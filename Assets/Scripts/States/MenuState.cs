using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuState : BaseState{
    public override void OnActivate()
    {
        Managers.UI.OnActivateUI(UIType.Menu);
    }
    public override void DeActivate()
    {
        Managers.UI.DeActivateUI(UIType.Menu);
    }
    public override void OnUpdate()
    {

    }

}
