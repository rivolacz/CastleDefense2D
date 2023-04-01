using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    public int PlaySceneIndex;
    private AsyncOperation loadSceneOperation;

    private void Awake()
    {
        loadSceneOperation = SceneManager.LoadSceneAsync(PlaySceneIndex);
        loadSceneOperation.allowSceneActivation = false;
    }

    public void LoadMainMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void LoadGame()
    {
        loadSceneOperation.allowSceneActivation = true;
    }
}
