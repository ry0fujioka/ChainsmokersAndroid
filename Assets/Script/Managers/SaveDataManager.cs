using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SaveDataManager : Singleton<SaveDataManager>
{
    //PlayerPrefs
    //"(Scene name)"+"Star" returns collected stars of the level with the name
    //
    //"(Scene name)"+"Lock" returns 0 if it's locked, returns 1 if it's unlocked
    //
    //"Volume" returns volume setting

    public int GetStarData(string levelName)
    {
        return PlayerPrefs.GetInt(levelName + "Star", 0);
    }

    public void SaveStarData(int currentStar)
    {
        int savedStar = PlayerPrefs.GetInt(SceneManager.GetActiveScene().name+"Star", 0);
        if (currentStar > savedStar)
        {
            PlayerPrefs.SetInt(SceneManager.GetActiveScene().name + "Star", currentStar);
            PlayerPrefs.Save();
        }
    }

    public int GetLevelOpenData(string levelName)
    {
        return PlayerPrefs.GetInt(levelName + "Lock", -1);
    }

    public void SetLevelOpenData(string levelName)
    {
        PlayerPrefs.SetInt(levelName + "Lock", 0);
        PlayerPrefs.Save();
    }

    public void UnlockLevel(string unlockingLevelName)
    {
        PlayerPrefs.SetInt(unlockingLevelName + "Lock", 1);
        PlayerPrefs.Save();
    }

    public void LockLevel(string lockingLevelName)
    {
        PlayerPrefs.SetInt(lockingLevelName + "Lock", 0);
        PlayerPrefs.Save();
    }


    public void SetVolume(float volume)
    {
        volume = Mathf.Clamp(volume, 0, 1);
        PlayerPrefs.SetFloat("Volume", volume);
        PlayerPrefs.Save();
    }
}
