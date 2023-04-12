using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class UpgradesManager : MonoBehaviour
{
    private static UpgradesHolder upgrades = new UpgradesHolder();
    public static UpgradesHolder Upgrades
    {
        get
        {
            if (upgrades == null)
            {
                upgrades = LoadUpgrades();
            }
            return upgrades;
        }
        set
        {
            upgrades = value;
        }
    }

    private const string LoadDataDirectoryKey = "SelectedDirectoryForLoadData";
    private const string upgradesFilename = "upgrades.txt";


    private void Awake()
    {
        if(Upgrades.Money == 0)
        {
            Upgrades.Money = 999;
        }
        Upgrades = LoadUpgrades();
    }

    public static UpgradesHolder LoadUpgrades()
    {
        string directoryPath = PlayerPrefs.GetString(LoadDataDirectoryKey);
        if (directoryPath == string.Empty)
        {
            directoryPath = Application.persistentDataPath + "/save1";
        }
        try
        {
            string filePath = Path.Combine(directoryPath, upgradesFilename);
            string data = File.ReadAllText(filePath);
            Debug.Log(data);
            UpgradesHolder upgrades = JsonUtility.FromJson<UpgradesHolder>(data);
            return upgrades;
        }
        catch(Exception ex)
        {
            Debug.Log(ex.Message);
            UpgradesHolder upgrades = new UpgradesHolder();
            Upgrades = upgrades;
            Debug.Log(upgrades.KnightUpgrades.AttackSpeedBonusBought);
            SaveUpgrades();
            return upgrades;
        }
    }

    public static void SaveUpgrades()
    {
        string directoryPath = PlayerPrefs.GetString(LoadDataDirectoryKey);
        if(directoryPath == string.Empty)
        {
            directoryPath = Application.persistentDataPath + "/save1";
        }
        string filePath = Path.Combine(directoryPath, upgradesFilename);
        UpgradesHolder upgradesHolder = Upgrades;
        var data = JsonUtility.ToJson(upgradesHolder);
        Debug.Log($"Saving to {filePath} {data}");
        File.WriteAllText(filePath,data);
    }

    public static bool Buy(float price)
    {
        if(Upgrades.Money >= price)
        {
            Upgrades.Money -= price;
            return true;
        }
        return false;
    }
}
