using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CancelButton : MonoBehaviour {

    void OnClickCancelButton()
    {
        Managers.UI.PopPanel(PanelType.GamePause);
    }
    void Awake()
    {
        GetComponent<Button>().onClick.AddListener(OnClickCancelButton);
    }
}
