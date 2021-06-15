using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using System.Linq;
using Random = System.Random;

public class RateTableData : MonoBehaviour
{
    public struct Player
    {
        private string _name;
        private int _recordScore;

        public Player(string name, int recordScore)
        {
            _name = name;
            _recordScore = recordScore;
            Name = name;
            RecordScore = recordScore;
        }
        public string Name { get; set; }
        public int RecordScore { get; set; }
    }
    
    private static string[] names = {"Петя", "Саша", "Коля", "Паша", "Артем", "Семен", "Данил", "Кирилл", "Егор",
    "Дима", "Миша", "Ваня", "Андрей", "Сергей", "Леша", "Ярослав", "Илья", "Рома", "Никита", "Богдан",
        "Максим", "Захар", "Вова", "Костя", "Матвей", "Денис", "Глеб", "Сеня", "Гриша", "Сева", "Боря"};

    public static Player[] players;
    public static int playerPosition;

    private static Random random = new Random(455445);

    public static void UpdateTable()
    {
        players = new Player[30];
        players[0].Name = "Вы";
        players[0].RecordScore = PlayerPrefs.GetInt("Record");
        
        for (var i = 1; i < 30; i++)
        {
            players[i].Name = names[i-1];
            players[i].RecordScore = random.Next(5, 300);
        }
        Sort();
        Reverse();
        GetPlayerPosition();
    }
    
    private static void Sort()
    {
        Player temp;
        for (int i = 0; i < players.Length; i++)
        {
            for (int j = i + 1; j < players.Length; j++)
            {
                if (players[i].RecordScore > players[j].RecordScore)
                {
                    temp = players[i];
                    players[i] = players[j];
                    players[j] = temp;
                }                   
            }            
        }
    }

    private static void Reverse()
    {
        Player temp;
        for (var i = 0; i < players.Length / 2; i++)
        {
            temp = players[i];
            players[i] = players[players.Length - i - 1];
            players[players.Length - i - 1] = temp;
        }
    }

    private static void GetPlayerPosition()
    {
        for (var i = 0; i < players.Length; i++)
        {
            if (players[i].Name == "Вы")
            {
                playerPosition = i + 1;
            }
        }
    }
} 