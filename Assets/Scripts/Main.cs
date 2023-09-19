using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.IO;

public class Main : MonoBehaviour
{
    public Button _newGameBttn;
    public Button _slot1;
    public Button _slot2;
    public Button _slot3;

    public GameObject _newCreate;
    public Text[] _slotText;
    public Text _newPlayerName;

    bool[] _saveFile = new bool[3];

    private void Start()
    {
        for (int i = 0; i < 3; i++)
        {
            if (File.Exists(DataManager.Instance.path + $"{i}"))
            {
                _saveFile[i] = true;
                DataManager.Instance._nowSlot = i;
                DataManager.Instance.Load();
                _slotText[i].text = DataManager.Instance.nowPlayer.name;
            }

            else
                _slotText[i].text = "비어 있음";
        }

        DataManager.Instance.DataReset();
    }

    public void GameStart()
    {
        _slot1.gameObject.SetActive(true);
        _slot2.gameObject.SetActive(true);
        _slot3.gameObject.SetActive(true);
    }

    public void CreateCharacter()
    {
        _newCreate.SetActive(true);
    }

    public void Slot(int num)
    {
        DataManager.Instance._nowSlot = num;

        if (_saveFile[num])
        {
            DataManager.Instance.Load();
            GoGame();
        }

        else
            CreateCharacter();
    }

    public void GoGame()
    {
        if (!_saveFile[DataManager.Instance._nowSlot])
        {
            DataManager.Instance.nowPlayer.name = _newPlayerName.text;
            DataManager.Instance.Save();
        }

        SceneManager.LoadScene(1);
    }
}
