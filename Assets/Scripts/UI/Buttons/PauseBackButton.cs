using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseBackButton : MonoBehaviour {

    void OnClickPauseBackButton()
    {
        Managers.UI.PopPanel(PanelType.GamePause);
        Managers.States.SetState(typeof(MenuState));
    }
    void Awake()
    {
        GetComponent<Button>().onClick.AddListener(OnClickPauseBackButton);
    }
}
