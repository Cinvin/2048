using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatsButton : MonoBehaviour {

    void OnClickStatsButton()
    {
        Managers.UI.PushPanel(PanelType.PlayerStats);
    }
    void Awake()
    {
        GetComponent<Button>().onClick.AddListener(OnClickStatsButton);
    }
}
