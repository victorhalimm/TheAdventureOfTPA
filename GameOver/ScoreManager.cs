using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    private GameManager gameManager;
    [SerializeField] private TextMeshProUGUI score;
    void Start()
    {
        gameManager = GameManager.instance;
        setScore(gameManager.currMinute, gameManager.currSecond, gameManager.enemyKilled);
    }

    private void setScore(int totalMinute, int totalSecond, int enemyKill)
    {
        int totalScore = (totalMinute * 60 + totalSecond) * 100 + enemyKill * 500;
        score.text = totalScore.ToString();
    }

}
