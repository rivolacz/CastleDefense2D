using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    public int PlaySceneIndex;
    private AsyncOperation loadSceneOperation;

    private void Start()
    {
        loadSceneOperation = SceneManager.LoadSceneAsync(PlaySceneIndex);
        loadSceneOperation.allowSceneActivation = false;
    }

    public void LoadMainMenu()
    {
        PlayerPrefs.DeleteKey("AutomaticallyLoadSlot");
        SceneManager.LoadScene(0);
    }

    public void LoadPlayerMenu()
    {
        SceneManager.LoadScene(1);
    }

    public void LoadGame()
    {
        loadSceneOperation.allowSceneActivation = true;
    }
}
