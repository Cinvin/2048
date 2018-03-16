using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStatsBackButton : MonoBehaviour {

    void OnClickPlayerStatsBackButton()
    {
        Managers.UI.PopPanel(PanelType.PlayerStats);
    }
    void Awake()
    {
        GetComponent<Button>().onClick.AddListener(OnClickPlayerStatsBackButton);
    }
}
