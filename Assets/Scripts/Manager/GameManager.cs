using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    #region 싱글턴

    static GameManager _instance;
    public static GameManager Instance
    {
        get
        {
            if (_instance == null)
                _instance = FindObjectOfType<GameManager>();

            return _instance;
        }
    }
    #endregion

    [Header("[ 게임 컨트롤 ]")]
    public float _gameTime;
    public float _maxGameTime = 300f;
    public bool _isLive;

    [Header("[ 플레이어 정보 ]")]
    public int _level;
    public int _killCount;
    public float _curHp;
    public float _maxHP = 100;
    public int _exp;
    public int[] _nextExp = {  };

    [Header("[ 게임 오브젝트 ]")]
    public PlayerCtrl _player;
    public PoolManager _pool;
    public LevelUp _uiLevelUp;
    public Result _uiResult;
    public GameObject _enemyClear;

    int _coinScore = 0;
    int _cooperScore = 0;

    private void Start()
    {
        _curHp = _maxHP;
        _uiLevelUp.Select(0);
        _uiLevelUp.Hide_LevelUp();
        Resume();
    }

    private void Update()
    {
        if (!_isLive) return;

        _gameTime += Time.deltaTime;

        if(_gameTime > _maxGameTime)
        {
            _gameTime = _maxGameTime;
            GameVictory();
        }
    }

    // 경험치 증가 함수
    public void GetExp()
    {
        if (!_isLive) return;

        _exp++;

        if (_exp == _nextExp[Mathf.Min(_level, _nextExp.Length - 1)])
        {
            _level++;
            _exp = 0;
            _uiLevelUp.Show_LevelUp();
        }
    }
    public void AddCoin(int coinScore)
    {
        if (_isLive)
        {
            _coinScore += coinScore;
            UIManager.Instance.Update_CoinText(_coinScore);
        }
    }

    public void AddCooper(int cooper)
    {
        if (_isLive)
        {
            _cooperScore += cooper;
            UIManager.Instance.Update_CooperText(_cooperScore);
        }
    }

    public void Stop()
    {
        _isLive = false;
        Time.timeScale = 0;
    }

    public void Resume()
    {
        _isLive = true;
        Time.timeScale = 1;
    }

    public void GameStart()
    {
        _curHp = _maxHP;
        _uiLevelUp.Select(0);
        Resume();
    }

    public void ReStart()
    {
        SceneManager.LoadScene(1);
    }

    public void GameOver()
    {
        StartCoroutine(Coroutine_GameOver());
    }

    IEnumerator Coroutine_GameOver()
    {
        _isLive = false;

        yield return new WaitForSeconds(0.5f);

        _uiResult.gameObject.SetActive(true);
        _uiResult.Lose();
        Stop();
    }

    public void GameVictory()
    {
        StartCoroutine(Coroutine_GameVictory());

        _maxGameTime += 60f;
    }

    IEnumerator Coroutine_GameVictory()
    {
        _isLive = false;
        _enemyClear.SetActive(true);

        yield return new WaitForSeconds(0.5f);

        _uiResult.gameObject.SetActive(true);
        _uiResult.Win();
        Stop();
    }
}
