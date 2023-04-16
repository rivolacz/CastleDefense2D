using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using Unity.Services.Analytics;
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
            UpgradesHolder upgrades = Newtonsoft.Json.JsonConvert.DeserializeObject<UpgradesHolder>(data);
            return upgrades;
        }
        catch(Exception ex)
        {
            Debug.Log(ex.Message);
            UpgradesHolder upgrades = new UpgradesHolder();
            Upgrades = upgrades;
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
        var data = Newtonsoft.Json.JsonConvert.SerializeObject(upgradesHolder);
        Debug.Log($"Saving to {filePath} {data}");
        File.WriteAllText(filePath,data);
    }

    public static bool Buy(float price)
    {
        if(Upgrades.Coins >= price)
        {
            Upgrades.Coins -= price;
            return true;
        }
        return false;
    }


    public static void SendDataToAnalytics(string upgradeName)
    {
        Dictionary<string, object> parameters = new Dictionary<string, object>()
        {
            { "UpgradeName", upgradeName },
            { "Retries", Upgrades.Retries },
            {"CoinsLeft", Upgrades.Coins },
        };
        AnalyticsService.Instance.CustomData("UpgradeBought", parameters);
    }
}
