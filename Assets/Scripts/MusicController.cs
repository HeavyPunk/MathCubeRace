using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicController : MonoBehaviour
{
    void Start()
    {
        AudioListener.pause = PlayerPrefs.GetInt("isMusicOn") == 0;
    }
}
