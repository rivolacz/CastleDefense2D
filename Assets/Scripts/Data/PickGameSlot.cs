using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using Project.Localization;
using UnityEngine.SceneManagement;
using System;

public class PickGameSlot : MonoBehaviour
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
    [SerializeField]
    Canvas storyCanvas;
    [SerializeField]
    LocalizedText loadingText;

    bool loading = false;
    private string directoryToDelete = string.Empty;
    private const string LoadDataDirectoryKey = "SelectedDirectoryForLoadData";
    private const string AutomaticallyLoadSlotKey = "AutomaticallyLoadSlot";
    private const int PlayerMenuSceneIndex = 1;
    AsyncOperation loadSceneOperation;

    private void Awake()
    {
        if(PlayerPrefs.HasKey(AutomaticallyLoadSlotKey) && Convert.ToBoolean(PlayerPrefs.GetInt(AutomaticallyLoadSlotKey)))
        {
            Debug.Log("Key was set");
            string directory = PlayerPrefs.GetString(LoadDataDirectoryKey);
            Debug.Log(directory);
            if(Directory.Exists(directory))
            {
                Debug.Log("Loading");
                LoadPlayerMenuScene();
            }
        }
    }

    void Start()
    {
        CheckForSaves();
    }

    public void CheckForSaves()
    {
        if (loading) return;
        string directoryPath1 = Application.persistentDataPath + "/save1";
        string directoryPath2 = Application.persistentDataPath + "/save2";
        string directoryPath3 = Application.persistentDataPath + "/save3";
        CheckForSave(directoryPath1, firstSaveText,firstDeleteButton);
        CheckForSave(directoryPath2, secondSaveText, secondDeleteButton);
        CheckForSave(directoryPath3, thirdSaveText, thirdDeleteButton);
    }

    public void CheckForSave(string fileName, TMP_Text saveText, GameObject deleteButton)
    {
        if (Directory.Exists(fileName))
        {
            deleteButton.SetActive(true);
            var lastModifiedDate = Directory.GetLastWriteTime(fileName);
            Debug.Log(fileName + " FOUND " + lastModifiedDate);
            saveText.GetComponent<LocalizedText>().ShouldLocalize(false);
            saveText.text = lastModifiedDate.ToString();
            Debug.Log(saveText.text);
        }
        else
        {
            Debug.Log(fileName + " NOT FOUND ");
            deleteButton.SetActive(false);
            var localizedText = saveText.gameObject.GetComponent<LocalizedText>();
            localizedText.ShouldLocalize(true);
            localizedText.SetText();
        }
    }

    public void PlayerWantsToDeleteSave(string filename)
    {
        directoryToDelete = filename;
        deleteConfirmationPanel.SetActive(true);
    }


    public void DeleteSave()
    {
        deleteConfirmationPanel.SetActive(false);
        string filePath = $"{Application.persistentDataPath}/{directoryToDelete}";
        if (Directory.Exists(filePath))
        {
            var files = Directory.GetFiles(filePath);
            foreach (string file in files) {
                File.Delete(file);
            }
            Directory.Delete(filePath);
        }
        CheckForSaves();
    }

    public void SelectedSlot(string filename)
    {
        if (loading) return;
        loading = true;
        loadingText.SetText();
        loadingText.GetComponent<TMP_Text>().enabled = true;
        filename = $"{Application.persistentDataPath}/{filename}";
        PlayerPrefs.SetString(LoadDataDirectoryKey, filename);
        if (Directory.Exists(filename))
        {       
            LoadPlayerMenuScene();
        }
        else
        {
            Debug.Log("Creating directory" + filename);
            Directory.CreateDirectory(filename);
            storyCanvas.enabled = true;
            loadSceneOperation = SceneManager.LoadSceneAsync(1);
            loadSceneOperation.allowSceneActivation = false;
        }
    }

    private void LoadPlayerMenuScene()
    {
        SceneManager.LoadScene(PlayerMenuSceneIndex);
    }

    public void StartNewGame()
    {
        if (loadSceneOperation != null)
        {
            loadSceneOperation.allowSceneActivation = true;
        }
    }

    public void AutomaticallyLoadSelectedSlot(bool load)
    {
        PlayerPrefs.SetInt(AutomaticallyLoadSlotKey, Convert.ToInt32(load));
    }
}
