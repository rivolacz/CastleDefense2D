using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class UpgradesManager : MonoBehaviour
{
    public static UpgradesHolder Upgrades = new UpgradesHolder();
    private const string LoadDataDirectoryKey = "SelectedDirectoryForLoadData";
    private const string upgradesFilename = "upgrades.txt";


    private void Start()
    {
        LoadUpgrades();
    }

    public static UpgradesHolder LoadUpgrades()
    {
        string directoryPath = PlayerPrefs.GetString(LoadDataDirectoryKey);
        if (directoryPath == string.Empty)
        {
            directoryPath = Application.persistentDataPath + "/save1";
        }
        string filePath = Path.Combine(directoryPath, upgradesFilename);
        string data = File.ReadAllText(filePath);
        Debug.Log(data);
        UpgradesHolder upgrades = JsonUtility.FromJson<UpgradesHolder>(data);
        Upgrades = upgrades;
        return upgrades;
    }

    public static void SaveUpgrades()
    {
        string directoryPath = PlayerPrefs.GetString(LoadDataDirectoryKey);
        if(directoryPath == string.Empty)
        {
            directoryPath = Application.persistentDataPath + "/save1";
        }
        string filePath = Path.Combine(directoryPath, upgradesFilename);
        Debug.Log(filePath);
        UpgradesHolder upgradesHolder = Upgrades;
        var data = JsonUtility.ToJson(upgradesHolder);
        Debug.Log(data);
        File.WriteAllText(filePath,data);
    }
}
