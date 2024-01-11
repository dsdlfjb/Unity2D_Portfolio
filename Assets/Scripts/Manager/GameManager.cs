using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    #region ?±Í???
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

    [Header("[ Í≤åÏûÑ Ïª®Ìä∏Î°?]")]
    public float _gameTime;
    public float _maxGameTime = 300f;
    public bool _isLive;

    [Header("[ ?åÎ†à?¥Ïñ¥ ?ïÎ≥¥ ]")]
    public int _level;
    public int _killCount;
    public float _curHp;
    public float _maxHP = 100;
    public int _exp;
    public int[] _nextExp = {  };

    [Header("[ Í≤åÏûÑ ?§Î∏å?ùÌä∏ ]")]
    public PlayerCtrl _player;
    public PoolManager _pool;
    public LevelUp _uiLevelUp;
    public Result _uiResult;
    public GameObject _enemyClear;

    public InventoryManager _inventoryManager;
    public ShopManager _shopManager;

    private void Awake()
    {
        // JSON ?åÏùºÎ°úÎ????∞Ïù¥??Î°úÎìú
        string jsonData = "";   // JSON ?åÏùº?êÏÑú ?∞Ïù¥?∞Î? ?ΩÏñ¥?Ä??jsonData???Ä??
        // jsonDataÎ•??åÏã±?òÏó¨ InventoryManager?Ä ShopManager??weaponList???†Îãπ
        //_inventoryManager._skinList = JsonUtility.FromJson<SkinList>(jsonData);
        //_shopManager._skinList = _inventoryManager._skinList;
    }

    private void Start()
    {
        _curHp = _maxHP;
        _uiLevelUp.Select(0);
        _uiLevelUp.Hide_LevelUp();
        Resume();

        GameStart();
    }

    private void Update()
    {
        if (!_isLive) return;

        _gameTime += Time.deltaTime;
    }

    // Í≤ΩÌóòÏπ?Ï¶ùÍ? ?®Ïàò
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

        AudioManager.Instance.PlayBgm(true);
        AudioManager.Instance.PlaySfx(AudioManager.ESfx.Select);
    }

    public void ReStart()
    {
        SceneManager.LoadScene(1);
        _gameTime = 0;
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

        _gameTime = 0;

        AudioManager.Instance.PlayBgm(false);
        AudioManager.Instance.PlaySfx(AudioManager.ESfx.Lose);
    }

    public void GameVictory()
    {
        StartCoroutine(Coroutine_GameVictory());
    }

    IEnumerator Coroutine_GameVictory()
    {
        _isLive = false;
        _enemyClear.SetActive(true);
        yield return new WaitForSeconds(0.5f);

        _uiResult.gameObject.SetActive(true);
        _uiResult.Win();
        Stop();

        AudioManager.Instance.PlayBgm(false);
        AudioManager.Instance.PlaySfx(AudioManager.ESfx.Win);
    }
}
