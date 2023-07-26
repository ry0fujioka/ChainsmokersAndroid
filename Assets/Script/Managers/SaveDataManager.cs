using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SaveDataManager : Singleton<SaveDataManager>
{
    public int GetStarData(string levelName)
    {
        //�e���x���̐��̊l���󋵂��l������B�f�t�H���g�̌���0
        return PlayerPrefs.GetInt(levelName + "Star", 0);
    }

    
    public void SaveStarData(int currentStar)
    {
        //�l���������̃f�[�^��"���x���̖��O"+"Star"���L�[�Ƃ���int�ŕۑ�
        int savedStar = PlayerPrefs.GetInt(SceneManager.GetActiveScene().name+"Star", 0);
        if (currentStar > savedStar)
        {
            PlayerPrefs.SetInt(SceneManager.GetActiveScene().name + "Star", currentStar);
            PlayerPrefs.Save();
        }
    }

    
    public int GetLevelOpenData(string levelName)
    {
        //���x�����J������Ă邩�`�F�b�N
        //�J������Ă��Ȃ����0��
        //�J������Ă����1��
        //�������݂��Ȃ����x���̏ꍇ-1���Ԃ��Ă���
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
        //���ʂ̃f�[�^�̕ۑ�
        volume = Mathf.Clamp(volume, 0, 1);
        PlayerPrefs.SetFloat("Volume", volume);
        PlayerPrefs.Save();
    }
}
