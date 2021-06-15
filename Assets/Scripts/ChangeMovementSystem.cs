using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeMovementSystem : MonoBehaviour
{
    public Toggle toggle;
    
    void Start()
    {
        toggle.isOn = PlayerPrefs.GetString("SwipeMode").Equals("On");
    }

    void Update()
    {
        if (toggle.isOn)
        {
            PlayerPrefs.SetString("SwipeMode", "On");
        }
        else
            PlayerPrefs.SetString("SwipeMode", "Off");
    }
}
