using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GameData : MonoBehaviour 
{
    public float startingCoins;
    public static float Coins { get; private set; }

    private void Awake()
    {
        Coins = startingCoins;
    }

    public static bool CanAfford(float coins)
    {
        return Coins >= coins;
    }

    public static void Buy(float coins)
    {
        Coins -= coins;
    }
}
