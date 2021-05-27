using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerCoins : MonoBehaviour
{
    public int playerCoins;

    [SerializeField] Text MenuCoins;
    [SerializeField] Text ShopCoins;

    private string _playerCoinsString;

    public bool TryBuySkin(int price)
    {
        if (playerCoins >= price)
        {
            playerCoins -= price;
            PlayerPrefs.SetInt("TotalScore", playerCoins);
            UpdateCoins(_playerCoinsString);
            return true;
        }
        return false;
    }

    private void Update()
    {
        playerCoins = PlayerPrefs.GetInt("TotalScore");
        if (playerCoins >= 0 && playerCoins <= 999)
            _playerCoinsString = playerCoins.ToString();
        if (playerCoins >= 1000 && playerCoins <= 999999)
            _playerCoinsString = playerCoins / 1000 + "." + (playerCoins % 1000) / 10 + "K";
        if (playerCoins >= 1000000)
            _playerCoinsString = playerCoins / 1000000 + "." + (playerCoins / 10000) % 100 + "M";
        UpdateCoins(_playerCoinsString);
    }

    public void UpdateCoins(string playerCoinsString)
    {
        MenuCoins.text = playerCoinsString;
        ShopCoins.text = playerCoinsString;
    }
}
