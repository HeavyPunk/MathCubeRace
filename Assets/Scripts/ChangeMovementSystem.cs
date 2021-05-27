using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeMovementSystem : MonoBehaviour
{
    public Toggle toggle;
    
    // Start is called before the first frame update
    void Start()
    {
        toggle.isOn = PlayerPrefs.GetString("SwipeMode").Equals("On");
    }

    // Update is called once per frame
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
