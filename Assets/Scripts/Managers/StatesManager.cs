using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class StatesManager : MonoBehaviour {
    private BaseState currentState;

    public void SetState(System.Type type)
    {
        if (currentState != null)
        {
            currentState.DeActivate();
        }
        currentState = GetComponentInChildren(type) as BaseState;
        if (currentState != null)
        {
            currentState.OnActivate();
        }
    }
    

    // Use this for initialization
    void Start () {
        SetState(typeof(MenuState));
    }
	
	// Update is called once per frame
	void Update () {
        if (currentState != null)
        {
            currentState.OnUpdate();
        }
    }
}
