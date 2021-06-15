using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GamePause : MonoBehaviour
{
    public GameObject PausePanel;
    public GameObject PauseButton;
    
    private bool _isPause = false;
    
    public void OnResumeButton()
    {
        _isPause = false;
        Time.timeScale = 1;
        AudioListener.pause = PlayerPrefs.GetInt("isMusicOn") == 0;
        PauseButton.SetActive(true);
        PausePanel.SetActive(false);
    }

    public void OnPauseButton()
    {
        _isPause = true;
        Time.timeScale = 0;
        if (PlayerPrefs.GetInt("isMusicOn") != 0)
            AudioListener.pause = PlayerPrefs.GetInt("isMusicOn") != 0;
        PauseButton.SetActive(false);
        PausePanel.SetActive(true);
    }

    public void OnMenuButton()
    {
        _isPause = false;
        Time.timeScale = 1;
        PausePanel.SetActive(false);
        SceneManager.LoadScene(0);
    }
}
