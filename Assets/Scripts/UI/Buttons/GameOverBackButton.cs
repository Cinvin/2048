using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOverBackButton : MonoBehaviour {

    void OnClickGameOverBackButton()
    {
        Managers.UI.PopPanel(PanelType.GameOver);
        Managers.States.SetState(typeof(MenuState));
    }
    void Awake()
    {
        GetComponent<Button>().onClick.AddListener(OnClickGameOverBackButton);
    }
}
