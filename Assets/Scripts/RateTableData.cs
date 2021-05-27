using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class RateTableData : MonoBehaviour
{
    private SortedList<int, Player> Players;
    
    public class Player
    {
        private int _id;
        private string _name;
        private int _recordScore;

        public string Name => _name;
        public int RecordScore => _recordScore;
    }
} 