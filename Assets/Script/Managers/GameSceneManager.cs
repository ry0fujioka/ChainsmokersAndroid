using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameSceneManager : Singleton<GameSceneManager>
{
    private bool isLoadingScene = false;
    private void Start()
    {

        Time.timeScale = 1;
    }

    public void LoadScene(string sceneName)
    {
        if(!isLoadingScene)
            StartCoroutine(LoadAScene(sceneName));
    }

    public void ReloadScene()
    {
        if (!isLoadingScene)
            StartCoroutine(ReloadAScene());
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    IEnumerator LoadAScene(string sceneName)
    {
        isLoadingScene = true;
        yield return new WaitForSecondsRealtime(0.8f);
        isLoadingScene = false;
        SceneManager.LoadSceneAsync(sceneName);
    }

    IEnumerator ReloadAScene()
    {
        isLoadingScene = true;
        yield return new WaitForSecondsRealtime(0.8f);
        isLoadingScene = false;
        SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex);
    }
}
