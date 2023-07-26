using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SaveDataManager : Singleton<SaveDataManager>
{
    public int GetStarData(string levelName)
    {
        //各レベルの星の獲得状況を獲得する。デフォルトの個数は0
        return PlayerPrefs.GetInt(levelName + "Star", 0);
    }

    
    public void SaveStarData(int currentStar)
    {
        //獲得した星のデータを"レベルの名前"+"Star"をキーとしてintで保存
        int savedStar = PlayerPrefs.GetInt(SceneManager.GetActiveScene().name+"Star", 0);
        if (currentStar > savedStar)
        {
            PlayerPrefs.SetInt(SceneManager.GetActiveScene().name + "Star", currentStar);
            PlayerPrefs.Save();
        }
    }

    
    public int GetLevelOpenData(string levelName)
    {
        //レベルが開放されてるかチェック
        //開放されていなければ0が
        //開放されていれば1が
        //もし存在しないレベルの場合-1が返ってくる
        return PlayerPrefs.GetInt(levelName + "Lock", -1);
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
        //音量のデータの保存
        volume = Mathf.Clamp(volume, 0, 1);
        PlayerPrefs.SetFloat("Volume", volume);
        PlayerPrefs.Save();
    }
}
