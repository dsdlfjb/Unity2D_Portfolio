using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Main : MonoBehaviour
{
    public Button _newGameBttn;

    public void NewGame()
    {
        SceneManager.LoadScene(2);
    }


}
