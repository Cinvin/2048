using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseButton : MonoBehaviour {

    void OnClickPauseButton()
    {
        Managers.UI.PushPanel(PanelType.GamePause);
    }
    void Awake()
    {
        GetComponent<Button>().onClick.AddListener(OnClickPauseButton);
    }
}
