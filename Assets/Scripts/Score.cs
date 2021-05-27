using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
    public double score;
    [SerializeField] Text scoreText;
    public bool isGameEnd;

    void Start()
    {
        score = 0;
        isGameEnd = false;
    }

    void Update()
    {
        if (!isGameEnd)
            score = PlayerPrefs.GetInt("RightAnswerCount");
        scoreText.text = ((int)score).ToString();
        PlayerPrefs.SetInt("LastScore", (int)score);
        if (PlayerPrefs.GetInt("Record") < score)
            PlayerPrefs.SetInt("Record", (int)score);
    }
}
