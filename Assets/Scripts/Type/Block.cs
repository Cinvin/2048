using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Block : MonoBehaviour {
    private int value;
    public int Value { get { return value; } }
    public Transform trans;
    private Text text;
    void Awake()
    {
        text = GetComponentInChildren<Text>();
        trans = transform;
    }
    private void OnEnable()
    {
        value = Random.Range(1, 3) * 2;
        text.text = value.ToString();
    }
    public void Bigger()
    {
        value *= 2;
        Managers.Stats.UpdateCurrentScore(value);
        text.text = value.ToString();
    }
}
