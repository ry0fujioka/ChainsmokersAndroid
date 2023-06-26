using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : Singleton<UIManager>
{
    [SerializeField]
    private GameObject pauseandClearPanel;
    [SerializeField, Header("Pause UI")]
    private GameObject pausePanel;
    [SerializeField, Header("Coin UI")]
    private Image coinImage;
    [SerializeField, Header("Fuel UI")]
    private Image fuelImage;
    [SerializeField, Header("Star UI")]
    private Image zeroStar;
    [SerializeField]
    private Image oneStar;
    [SerializeField]
    private Image twoStar;
    [SerializeField]
    private Image threeStar;
    [SerializeField, Header("Clear UI")]
    private GameObject clearPanel;
    [SerializeField]
    private string nextLevel;
    private bool isGoal;
    private int starCount;

    private void OnEnable()
    {
        LevelManager.Instance.onRespawn += OnRespawn;
        Goal.Instance.onGoal += OnGoal;
    }

    private void OnDisable()
    {
        //if (LevelManager.Instance != null)
        //{
        //    LevelManager.Instance.onRespawn -= OnRespawn;
        //}
        //if (Goal.Instance != null)
        //{
        //    Goal.Instance.onGoal -= OnGoal;
        //}
    }

    // Start is called before the first frame update
    void Start()
    {
        OnRespawn();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    #region PauseMenu
    private void OpenPauseMenu()
    {
        pauseandClearPanel.SetActive(true);
        clearPanel.SetActive(false);
        pausePanel.SetActive(true);
        ChangeStars();
        Time.timeScale = 0;
    }

    private void ClosePauseMenu()
    {
        pauseandClearPanel.SetActive(false);
        pausePanel.SetActive(false);
        Time.timeScale = 1;
    }

    public void OnPauseButtonPressed()
    {
        OpenPauseMenu();
    }

    public void OnResumeButtonPressed()
    {
        ClosePauseMenu();
    }

    public void OnBackGroundButtonPressed()
    {
        if (pausePanel.activeInHierarchy)
        {
            SoundManager.Instance.PlaySound(SoundManager.Instance.BackClip);
            ClosePauseMenu();
        }
    }
    public void OnOptionButtonPressed()
    {

    }
    #endregion

    #region CoinUI
    public void OnCoinCollected()
    {
        coinImage.enabled = true;
    }
    #endregion

    #region FuelUI
    public void ChangeFuelAmount(float amount)
    {
        fuelImage.fillAmount = amount;
    }
    #endregion

    #region ClearPanel
    public void OnGoal()
    {
        isGoal = true;
        OpenClearMenu();
        SaveDataManager.Instance.SaveStarData(starCount);
        SaveDataManager.Instance.UnlockLevel(nextLevel);
        Debug.Log("Goal");
    }

    private void OpenClearMenu()
    {
        pauseandClearPanel.SetActive(true);
        pausePanel.SetActive(false);
        clearPanel.SetActive(true);
        ChangeStars();
        Time.timeScale = 0;
    }

    private void CloseClearMenu()
    {
        pauseandClearPanel.SetActive(false);
        pausePanel.SetActive(false);
        Time.timeScale = 1;
    }
    #endregion

    public void ChangeStars()
    {
        starCount = 0;
        if (fuelImage.fillAmount >= 0.5f)
            starCount++;
        if (coinImage.isActiveAndEnabled)
            starCount++;
        if (isGoal)
            starCount++;
        switch(starCount)
        {
            case (0):
                {
                    zeroStar.enabled = true;
                    oneStar.enabled = false;
                    twoStar.enabled = false;
                    threeStar.enabled = false;
                    break;
                }
            case (1):
                {
                    zeroStar.enabled = false;
                    oneStar.enabled = true;
                    twoStar.enabled = false;
                    threeStar.enabled = false;
                    break;
                }
            case (2):
                {
                    zeroStar.enabled = false;
                    oneStar.enabled = false;
                    twoStar.enabled = true;
                    threeStar.enabled = false;
                    break;
                }
            case (3):
                {
                    zeroStar.enabled = false;
                    oneStar.enabled = false;
                    twoStar.enabled = false;
                    threeStar.enabled = true;
                    break;
                }
        }
    }

    public void OnRespawn()
    {
        isGoal = false;
        coinImage.enabled = false;
        fuelImage.fillAmount = 1;
    }
}
