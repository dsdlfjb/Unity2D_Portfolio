using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.IO;
using TMPro;

public class Main : MonoBehaviour
{
    public Button _newGameBttn;
    public Button _slot1;
    public Button _slot2;
    public Button _slot3;

    public GameObject _newCreate;
    public TextMeshProUGUI[] _slotText;
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
                _slotText[i].text = "Empty Slot";
        }

        DataManager.Instance.DataReset();
    }

    public void GameStart()
    {
        AudioManager.Instance.PlaySfx(AudioManager.ESfx.Select);

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
        AudioManager.Instance.PlaySfx(AudioManager.ESfx.Select);
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
        AudioManager.Instance.PlaySfx(AudioManager.ESfx.Select);

        if (!_saveFile[DataManager.Instance._nowSlot])
        {
            DataManager.Instance.nowPlayer.name = _newPlayerName.text;
            DataManager.Instance.Save();
        }

        SceneManager.LoadScene(1);
    }
}
