using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public enum UIType { 
    Menu,
    InGame
}
public enum PanelType
{
    GamePause,
    GameOver,
    PlayerStats
}

public class UIManager : MonoBehaviour {
    public GameObject Menu_UI;
    public GameObject InGame_UI;

    public GameObject GamePause;
    public GameObject GameOver;
    public GameObject PlayerStats;

    public float PanelDuration;

    public Text InGameScore;
    public Text GameOverScore;
    public Text LastGameScore;
    public Text HighGameScore;

    public void OnActivateUI(UIType uitype)
    {
        if (uitype == UIType.Menu)
        {
            Menu_UI.SetActive(true);
        }
        else if (uitype == UIType.InGame)
        {
            InGame_UI.SetActive(true);
        }

    }
    public void DeActivateUI(UIType uitype)
    {
        if (uitype == UIType.Menu)
        {
            Menu_UI.SetActive(false);
        }
        else if (uitype == UIType.InGame)
        {
            InGame_UI.SetActive(false);
        }

    }

    public void PushPanel(PanelType panelType)
    {
        if (panelType == PanelType.GamePause)
        {
            GamePause.SetActive(true);
            GamePause.transform.DOMoveY(300, PanelDuration);
            Managers.Block.Movable = false;
        }
        else if(panelType == PanelType.GameOver)
        {
            GameOver.SetActive(true);
            Managers.Stats.UpdateTotalScore();
            GameOver.transform.DOMoveY(300, PanelDuration);
            Managers.Block.Movable = false;
        }
        else if (panelType == PanelType.PlayerStats)
        {
            PlayerStats.SetActive(true);
            PlayerStats.transform.DOMoveY(300, PanelDuration);
        }
    }
    public void PopPanel(PanelType panelType)
    {
        StartCoroutine(AsyncPopPanel(panelType));
    }
    private IEnumerator AsyncPopPanel(PanelType panelType)
    {
        if (panelType == PanelType.GamePause)
        {
            Managers.Block.Movable = true;
            Tween tween = GamePause.transform.DOMoveY(760, PanelDuration);
            yield return tween.WaitForCompletion();
            GamePause.SetActive(false);
        }
        else if (panelType == PanelType.GameOver)
        {
            Managers.Block.Movable = true;
            Tween tween = GameOver.transform.DOMoveY(760, PanelDuration);
            yield return tween.WaitForCompletion();
            GameOver.SetActive(false);
        }
        else if (panelType == PanelType.PlayerStats)
        {
            Tween tween = PlayerStats.transform.DOMoveY(760, PanelDuration);
            yield return tween.WaitForCompletion();
            PlayerStats.SetActive(false);
        }
    }

    public void ResetCurrentScore()
    {
        InGameScore.text = "0";
    }
    public void UpdateCurrentScore(int value)
    {
        InGameScore.text = value.ToString();
    }
    public void UpdateTotalScore(int lastvalue,int highvalue)
    {
        ResetCurrentScore();
        GameOverScore.text = lastvalue.ToString();
        LastGameScore.text = lastvalue.ToString();
        HighGameScore.text = highvalue.ToString();
    }

    // Update is called once per frame
    void Update () {
		
	}
}
