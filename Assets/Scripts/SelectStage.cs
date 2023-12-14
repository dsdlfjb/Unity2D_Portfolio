using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SelectStage : MonoBehaviour
{
    int _levelat;   // 현재 스테이지 번호, 오픈한 스테이지 번호
    public GameObject _stageNumObj;

    private void Start()
    {
        Button[] stages = _stageNumObj.GetComponentsInChildren<Button>();

        //_levelat = PlayerPrefs.GetInt("levelReached");
        _levelat = DataManager.Instance.nowPlayer.maxStage;
        // Debug.Log(_levelat);
        for (int i = _levelat + 1; i < stages.Length; i++)
        {
            stages[i].interactable = false;
        }
    }

    public void Move_To_Stage1()
    {
        AudioManager.Instance.PlaySfx(AudioManager.ESfx.Select);
        Managers.Instance.CurrentStageLevel = 1;
        SceneManager.LoadScene(2);
    }

    public void Move_To_Stage2()
    {
        AudioManager.Instance.PlaySfx(AudioManager.ESfx.Select);
        Managers.Instance.CurrentStageLevel = 2;
        SceneManager.LoadScene(3);
    }

    public void Move_To_Stage3()
    {
        AudioManager.Instance.PlaySfx(AudioManager.ESfx.Select);
        Managers.Instance.CurrentStageLevel = 3;
        SceneManager.LoadScene(4);
    }

    public void Move_To_Stage4()
    {
        AudioManager.Instance.PlaySfx(AudioManager.ESfx.Select);
        Managers.Instance.CurrentStageLevel = 4;
        SceneManager.LoadScene(5);
    }

    public void Move_To_Stage5()
    {
        AudioManager.Instance.PlaySfx(AudioManager.ESfx.Select);
        Managers.Instance.CurrentStageLevel = 5;
        SceneManager.LoadScene(6);
    }

    public void Move_To_Main()
    {
        AudioManager.Instance.PlaySfx(AudioManager.ESfx.Select);
        SceneManager.LoadScene(0);
    }
}
