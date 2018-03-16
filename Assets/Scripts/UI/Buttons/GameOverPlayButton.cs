using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOverPlayButton : MonoBehaviour {

    void OnClickGameOverPlayButton()
    {
        Managers.UI.PopPanel(PanelType.GameOver);

        Managers.Block.CleanBlocks();
        Managers.Stats.ResetCurrentScore();

        Managers.Block.Spawn();
        Managers.Block.Spawn();
        Managers.Block.Movable = true;
    }
    void Awake()
    {
        GetComponent<Button>().onClick.AddListener(OnClickGameOverPlayButton);
    }
}
