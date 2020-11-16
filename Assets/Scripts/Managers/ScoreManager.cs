using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    static public ScoreManager Instance;

    [SerializeField] Text scoreText; 

    private int score;

    private void Awake()
    {
        Instance = this;
    }


    public void IncreaseScore (int value)
    {
        score += value;
        UpdateScoreDisplay();
    }


    public void UpdateScoreDisplay ()
    {
        scoreText.text = score.ToString();
    }


    public int GetScore ()
    {
        return score;
    }
}


