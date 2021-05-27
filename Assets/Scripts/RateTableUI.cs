using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Random = System.Random;

public class RateTableUI : MonoBehaviour
{
    private Transform tableContainer;

    private Random _random = new Random(412421);
    // Start is called before the first frame update
    void Start()
    {
        tableContainer = FindObjectOfType<RateTableUI>().transform;
        for (var i = 1; i < 31; i++)
        {
            tableContainer.GetChild(i - 1).GetChild(0).GetComponent<Text>().text = i.ToString();
            tableContainer.GetChild(i - 1).GetChild(1).GetComponent<Text>().text = "Петя";
            tableContainer.GetChild(i - 1).GetChild(2).GetComponent<Text>().text = _random.Next(1, 1000).ToString();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
