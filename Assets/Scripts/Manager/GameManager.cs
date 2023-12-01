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

    public InventoryManager _inventoryManager;
    public ShopManager _shopManager;
    AudioManager _audio;

    private void Awake()
    {
        // JSON 파일로부터 데이터 로드
        string jsonData = "";   // JSON 파일에서 데이터를 읽어와서 jsonData에 저장

        // jsonData를 파싱하여 InventoryManager와 ShopManager의 weaponList에 할당
        //_inventoryManager._skinList = JsonUtility.FromJson<SkinList>(jsonData);
        //_shopManager._skinList = _inventoryManager._skinList;

        _audio = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }

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

        // 예전 코드 =============================
        // 시간 버티면 클리어헀던 코드
        //_gameTime += Time.deltaTime;
        //
        //if(_gameTime > _maxGameTime)
        //{
        //    _gameTime = _maxGameTime;
        //    GameVictory();
        //}
        //
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
        _gameTime = 0;
    }

    public void GameOver()
    {
        StartCoroutine(Coroutine_GameOver());
    }

    IEnumerator Coroutine_GameOver()
    {
        _isLive = false;
        _audio.PlaySFX(_audio._lose);
        yield return new WaitForSeconds(0.5f);

        _uiResult.gameObject.SetActive(true);
        _uiResult.Lose();
        Stop();

        _gameTime = 0;
    }

    public void GameVictory()
    {
        StartCoroutine(Coroutine_GameVictory());
    }

    IEnumerator Coroutine_GameVictory()
    {
        _isLive = false;
        _enemyClear.SetActive(true);
        _audio.PlaySFX(_audio._win);
        yield return new WaitForSeconds(0.5f);

        _uiResult.gameObject.SetActive(true);
        _uiResult.Win();
        Stop();
    }
}
