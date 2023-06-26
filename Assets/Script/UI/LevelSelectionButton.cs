using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LevelSelectionButton : MonoBehaviour
{
    public string levelToLoad;
    [SerializeField]
    private bool isFirstLevel = false;
    [SerializeField]
    private Image zeroStar;
    [SerializeField]
    private Image oneStar;
    [SerializeField]
    private Image twoStar;
    [SerializeField]
    private Image threeStar;
    [SerializeField]
    private Image lockImage;
    [SerializeField]
    private GameObject buttonContainer;
    [SerializeField]
    private TMP_Text text;
    private Button thisButton;

    // Start is called before the first frame update
    void Start()
    {
        thisButton = GetComponent<Button>();
        text.text = levelToLoad;
        CheckButton();
    }

    public void OnClick()
    {
        GameSceneManager.Instance.LoadScene(levelToLoad);
    }

    public void CheckButton()
    {
        if(SaveDataManager.Instance.GetLevelOpenData(levelToLoad) == 1)//if it's open
        {
            EnableButton();
        }
        else if(isFirstLevel)//if it's not open but set as first level
        {
            EnableButton();
        }
        else//if it's locked
        {
            DisableButton();
        }
    }

    private void EnableButton()
    {
        if (isFirstLevel)
            SaveDataManager.Instance.UnlockLevel(levelToLoad);
        thisButton.enabled = true;
        buttonContainer.SetActive(true);
        ChangeStars();
        lockImage.enabled = false;
    }

    private void DisableButton()
    {
        SaveDataManager.Instance.LockLevel(levelToLoad);
        thisButton.enabled = false;
        buttonContainer.SetActive(false);
        lockImage.enabled = true;
    }
    public void ChangeStars()
    {
        int starCount = SaveDataManager.Instance.GetStarData(levelToLoad);
        switch (starCount)
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
}
