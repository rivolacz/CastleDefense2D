using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
//using UnityEngine.Windows;
using System.IO;
using Project.Localization;
using UnityEngine.SceneManagement;

public class LoadGameData : MonoBehaviour
{
    [SerializeField]
    TMP_Text firstSaveText;
    [SerializeField]
    GameObject firstDeleteButton;
    [SerializeField]
    TMP_Text secondSaveText;
    [SerializeField]
    GameObject secondDeleteButton;
    [SerializeField]
    TMP_Text thirdSaveText;
    [SerializeField]
    GameObject thirdDeleteButton;
    [SerializeField]
    GameObject deleteConfirmationPanel;

    private string fileToDelete = string.Empty;

    void Start()
    {
        CheckForSaves();
    }

    public void CheckForSaves()
    {
        string filePath1 = Application.persistentDataPath + "/Save1.txt";
        string filePath2 = Application.persistentDataPath + "/Save2.txt";
        string filePath3 = Application.persistentDataPath + "/Save3.txt";
        CheckForSave(filePath1, firstSaveText,firstDeleteButton);
        CheckForSave(filePath2, secondSaveText, secondDeleteButton);
        CheckForSave(filePath3, thirdSaveText, thirdDeleteButton);
    }

    public void CheckForSave(string fileName, TMP_Text saveText, GameObject deleteButton)
    {
        if (File.Exists(fileName))
        {
            deleteButton.SetActive(true);
            var lastModifiedDate = File.GetLastWriteTime(fileName);
            saveText.text = lastModifiedDate.ToString();
        }
        else
        {
            deleteButton.SetActive(false);
            var localizedText = saveText.gameObject.GetComponent<LocalizedText>();
            localizedText.SetText();
        }
    }

    public void PlayerWantsToDeleteSave(string filename)
    {
        fileToDelete = filename;
        deleteConfirmationPanel.SetActive(true);
    }


    public void DeleteSave()
    {
        deleteConfirmationPanel.SetActive(false);
        string filePath = $"{Application.persistentDataPath}/{fileToDelete}.txt";
        if (File.Exists(filePath))
        {
            File.Delete(filePath);
        }
        CheckForSaves();
    }

    public void SelectedSlot(string filename)
    {
        filename += ".txt";
        if (File.Exists(filename))
        {
            PlayerPrefs.SetString("FilenameForLoadData", filename);
        }
        else
        {
            PlayerPrefs.DeleteKey(filename);
            File.Create(filename);
        }
        SceneManager.LoadScene(1);
    }
}
