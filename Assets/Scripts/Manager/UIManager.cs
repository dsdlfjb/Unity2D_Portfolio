using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    #region 싱글턴
    static UIManager _instance;
    public static UIManager Instance
    {
        get
        {
            if (_instance == null)
                _instance = FindObjectOfType<UIManager>();

            return _instance;
        }
    }
    #endregion

    public float _currTime;

    public TMP_Text _timeText;
    public TMP_Text _waveText;
    public TMP_Text _killCountText;
    public TMP_Text _coinText;
    //public TMP_Text _cooperText;
    public TMP_Text _lvText;

    public Slider _expBar;
    public Slider _hpBar;

    private void Start()
    {
        _hpBar.value = 100;
        _expBar.value = 0;
    }

    // Update is called once per frame
    void Update()
    {
        Timer();
        Level_UP();
        Kill_Count();
        HP_Slider();
    }

    void HP_Slider()
    {
        float curHp = GameManager.Instance._curHp;
        float maxHP = GameManager.Instance._maxHP;
        _hpBar.value = (curHp / maxHP) * 100;
    }

    void Timer()
    {
        _currTime += Time.deltaTime;
        int min = Mathf.FloorToInt(_currTime / 60);
        int sec = Mathf.FloorToInt(_currTime % 60);
        _timeText.text = string.Format("{00:} : {01:F0}", min, sec);
    }

    public void EXP_UP()     // 경험치가 다 차면 레벨업과 최대 경험치 증가
    {
        float curExp = GameManager.Instance._exp;
        float maxExp = GameManager.Instance._nextExp[Mathf.Min(GameManager.Instance._level, GameManager.Instance._nextExp.Length - 1)];
        _expBar.value = curExp / maxExp;
    }

    public void Level_UP()
    {
        _lvText.text = string.Format("LV.{0:F0}", GameManager.Instance._level);
    }

    public void Kill_Count()
    {
        _killCountText.text = string.Format("{0:F0}", GameManager.Instance._killCount);
    }

    public void Update_CoinText(int newCoin)
    {
        _coinText.text = newCoin.ToString();
    }

    /*
    public void Update_CooperText(int newCooper)
    {
        _cooperText.text = newCooper.ToString();
    }
    */
}
