using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RestartButton : MonoBehaviour {

    void OnClickRestartButton()
    {
        Managers.UI.PopPanel(PanelType.GamePause);

        Managers.Block.CleanBlocks();
        Managers.Stats.ResetCurrentScore();

        Managers.Block.Spawn();
        Managers.Block.Spawn();
        Managers.Block.Movable = true;
    }
    void Awake()
    {
        GetComponent<Button>().onClick.AddListener(OnClickRestartButton);
    }
}
