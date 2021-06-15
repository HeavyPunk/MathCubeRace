using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class RateTableUI : MonoBehaviour
{
    private Transform tableContainer;
    void Start()
    {
        RateTableData.UpdateTable();
        var players = RateTableData.players;
        var playerPosition = RateTableData.playerPosition;
        tableContainer = FindObjectOfType<RateTableUI>().transform;

        for (var i = 0; i < 30; i++)
        {
            tableContainer.GetChild(i).GetChild(0).GetComponent<Text>().text = (i + 1).ToString();
            tableContainer.GetChild(i).GetChild(1).GetComponent<Text>().text = players[i].Name;
            tableContainer.GetChild(i).GetChild(2).GetComponent<Text>().text = players[i].RecordScore.ToString();
        }

        tableContainer.GetChild(playerPosition - 1).GetComponent<Image>().color = new Color(0.882f, 0.729f, 1);
        
    }
}
