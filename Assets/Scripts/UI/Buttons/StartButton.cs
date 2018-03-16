using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartButton : MonoBehaviour {

    void OnClickStartButton()
    {
        Managers.States.SetState(typeof(GamePlayState));
    }
    void Awake()
    {
        GetComponent<Button>().onClick.AddListener(OnClickStartButton);
    }
}
