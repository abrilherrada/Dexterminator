using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    private int currentScore;

    public void Add(int score)
    {
        currentScore += score;
        Debug.Log($"Current score: {currentScore}");
    }
}
