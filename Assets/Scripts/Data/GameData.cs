using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using TMPro;

public class GameData : MonoBehaviour 
{
    public TMP_Text moneyText;
    public Transform castleTransform;
    public float startingCoins;
    public static float CashMultiplierFromAbility = 1;
    public static TMP_Text MoneyText;
    public static Transform CastleTransform { get; private set; }
    public static float Coins { get; private set; }
    public static int CurrentWave { get; set; }

    private void Awake()
    {
        Coins = startingCoins;
        MoneyText = moneyText;
        CastleTransform = castleTransform;
        moneyText.text = Coins.ToString();
    }

    public static bool CanAfford(float coins)
    {
        return Coins >= coins;
    }

    public static void Buy(float coins)
    {
        Coins -= coins;
        MoneyText.text = Coins.ToString();
    }

    public static void GetCoins(float coins)
    {
        Coins += coins;
        MoneyText.text = Coins.ToString();
    }
}
