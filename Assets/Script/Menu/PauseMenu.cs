using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    public GameObject pausePanel;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OpenPauseMenu()
    {
        pausePanel.SetActive(true);
        Time.timeScale = 0;
    }

    public void ClosePauseMenu()
    {
        pausePanel.SetActive(false);
        Time.timeScale = 1;
    }

    public void OnResumeButtonPressed()
    {

    }

    public void OnLevelSelectionButtonPressed()
    {

    }

    public void OnOptionButtonPressed()
    {

    }

    public void OnQuitButtonPressed()
    {

    }
}
