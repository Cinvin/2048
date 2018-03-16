using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatsManager : MonoBehaviour {
    private int currentScore;
    public int CurrentScore { get { return currentScore; } }
    private int lastScore;
    public int LastScore { get { return lastScore; } }
    private int highScore;
    public int HighScore { get { return highScore; } }

    void Awake()
    {
        currentScore =0;
        lastScore = 0;
        highScore = 0;
    }
    public void ResetCurrentScore()
    {
        currentScore = 0;
        Managers.UI.ResetCurrentScore();
    }
    public void UpdateCurrentScore(int increase)
    {
        currentScore += increase;
        Managers.UI.UpdateCurrentScore(currentScore);
    }
    public void UpdateTotalScore()
    {
        lastScore = currentScore;
        highScore = Mathf.Max(lastScore, highScore);
        currentScore = 0;
        Managers.UI.UpdateTotalScore(lastScore, highScore);
    }
}
