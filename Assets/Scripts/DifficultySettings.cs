using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DifficultySettings : MonoBehaviour
{
    public GameObject currentDifficulty;

    private Text _currentDifficultyText;
    void Start()
    {
        FirstCheckDifficulty();
        
        _currentDifficultyText = currentDifficulty.transform.GetChild(0).GetComponent<Text>();
        _currentDifficultyText.text = PlayerPrefs.GetString("CurrentDifficulty");
    }
    
    void Update()
    {
        _currentDifficultyText.text = PlayerPrefs.GetString("CurrentDifficulty");
    }

    public void OnLeftButton()
    {
        string difficulty = PlayerPrefs.GetString("CurrentDifficulty");
        
        if (difficulty.Equals("Легко"))
            PlayerPrefs.SetString("CurrentDifficulty", "Тяжело");
        else if (difficulty.Equals("Нормально"))
            PlayerPrefs.SetString("CurrentDifficulty", "Легко");
        else if (difficulty.Equals("Тяжело"))
            PlayerPrefs.SetString("CurrentDifficulty", "Нормально");
    }

    public void OnRightButton()
    {
        string difficulty = PlayerPrefs.GetString("CurrentDifficulty");
        
        if (difficulty.Equals("Легко"))
            PlayerPrefs.SetString("CurrentDifficulty", "Нормально");
        else if (difficulty.Equals("Нормально"))
            PlayerPrefs.SetString("CurrentDifficulty", "Тяжело");
        else if (difficulty.Equals("Тяжело"))
            PlayerPrefs.SetString("CurrentDifficulty", "Легко");
    }

    public static void FirstCheckDifficulty()
    {
        if (!PlayerPrefs.HasKey("CurrentDifficulty"))
            PlayerPrefs.SetString("CurrentDifficulty", "Нормально");
    }
}
