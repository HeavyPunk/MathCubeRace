using System.Collections;
using System.Collections.Generic;
using System.Net.Mime;
using UnityEngine;
using UnityEngine.UI;

public class MusicManager : MonoBehaviour
{
    public bool isMusicOn = true;
    public GameObject musicButtonOff;
    public GameObject musicButtonOn;
    public Sprite spriteGray, spriteGreen, spriteRed;

    private void Start()
    {
        isMusicOn = PlayerPrefs.GetInt("isMusicOn") == 1;
        ChangeColor();
    }
    
    public void ChangeColor()
    {
        if (isMusicOn)
        {
            Debug.Log("MusicOn");
            musicButtonOff.GetComponent<Image>().sprite = spriteGray;
            musicButtonOn.GetComponent<Image>().sprite = spriteGreen;
        }
        else
        {
            Debug.Log("MusicOff");
            musicButtonOff.GetComponent<Image>().sprite = spriteRed;
            musicButtonOn.GetComponent<Image>().sprite = spriteGray;
        }
    }

    public void OnMusicButtonOff()
    {
        isMusicOn = false;
        PlayerPrefs.SetInt("isMusicOn", isMusicOn ? 1 : 0);
        ChangeColor();
    }

    public void OnMusicButtonOn()
    {
        isMusicOn = true;
        PlayerPrefs.SetInt("isMusicOn", isMusicOn ? 1 : 0);
        ChangeColor();
    }
}
