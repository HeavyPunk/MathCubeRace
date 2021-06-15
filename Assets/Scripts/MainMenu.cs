using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public int totalScore;
    [SerializeField] Text scoreText;
    [SerializeField] Text recordText;

    void Start()
    {
        DifficultySettings.FirstCheckDifficulty();
        MathActions.FirstCheckOperators();
        totalScore = PlayerPrefs.GetInt("TotalScore");
        totalScore += PlayerPrefs.GetInt("LastScore");
        PlayerPrefs.SetInt("LastScore", 0);
        PlayerPrefs.SetInt("TotalScore", totalScore);
        recordText.text = "Ваш рекорд: " + PlayerPrefs.GetInt("Record"); 
    }

    private void Update()
    {
        totalScore = PlayerPrefs.GetInt("TotalScore");
        if (totalScore >= 0 && totalScore <= 999)
            scoreText.text = totalScore.ToString();
        if (totalScore >= 1000 && totalScore <= 999999)
            scoreText.text = totalScore / 1000 + "." + (totalScore % 1000) / 10 + "K";
        if (totalScore >= 1000000)
            scoreText.text = totalScore / 1000000 + "." + (totalScore / 10000) % 100 + "M";
    }

    public void OnPlayButton()
    {
        SceneManager.LoadScene(1);
    }

    public void OnApplicationPause(bool pauseStatus)
    {
        // Notifications.SendNotificationNews();
        // Notifications.SendNotificationRecord();
    }
}
