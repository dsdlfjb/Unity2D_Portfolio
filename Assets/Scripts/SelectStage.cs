using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SelectStage : MonoBehaviour
{
    int _levelat;   // ���� �������� ��ȣ, ������ �������� ��ȣ
    public GameObject _stageNumObj;

    private void Start()
    {
        Button[] stages = _stageNumObj.GetComponentsInChildren<Button>();

        _levelat = PlayerPrefs.GetInt("levelReached");
        // Debug.Log(_levelat);
        for (int i = _levelat + 1; i < stages.Length; i++)
        {
            stages[i].interactable = false;
        }
    }

    public void Move_To_Stage1()
    {
        SceneManager.LoadScene(2);
    }

    public void Move_To_Stage2()
    {
        SceneManager.LoadScene(3);
    }

    public void Move_To_Stage3()
    {
        SceneManager.LoadScene(4);
    }

    public void Move_To_Stage4()
    {
        SceneManager.LoadScene(5);
    }

    public void Move_To_Stage5()
    {
        SceneManager.LoadScene(6);
    }
}