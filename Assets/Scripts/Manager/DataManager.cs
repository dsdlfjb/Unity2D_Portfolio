using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataManager : MonoBehaviour
{
    #region 싱글턴
    public static DataManager _instance;

    void Awake()
    {
        // 인스턴스가 비어있다면
        if(_instance == null)
        {
            // 자기자신으로 채우고, 씬 이동시에도 삭제되지 않도록
            _instance =  this;
            DontDestroyOnLoad(gameObject);
        }
        // 인스턴스가 비어있지 않지만 자기자신과 다르다면 = 새로 생긴 객체
        else if(_instance != this)
        {
            // 자기자신 삭제
            Destroy(gameObject);
        }
    }
    #endregion

    public int _coinScore = 0;
    public int _cooperScore = 0;
    public string _swordSkinName;
    public string _bulletSkinName;

    // 코인 증가
    public void AddCoin(int coinScore)
    {
        // 현재, 게임매니저가 없는 씬이 있어서 게임매니저를 찾을 수 있을 때만 작동하도록
        if (GameManager.Instance && GameManager.Instance._isLive)
        {
            // 데이터매니저에 있는 코인스코어 증가
            DataManager._instance._coinScore += coinScore;
            ShopManager.Instance.Update_CoinText(DataManager._instance._coinScore);
        }
    }

    // 코인 감소
    public void RemoveCoin(int coinScore)
    {
        // 데이터매니저에 있는 코인스코어 감소
        DataManager._instance._coinScore -= coinScore;
        ShopManager.Instance.Update_CoinText(DataManager._instance._coinScore);
        // 현재는 상점 화면에서 유아이매니저에 접근할 수 없으므로 잠깐 주석처리
    }

    // 구리코인 증가
    public void AddCooper(int cooper)
    {
        // 현재, 게임매니저가 없는 씬이 있어서 게임매니저를 찾을 수 있을 때만 작동하도록
        if (GameManager.Instance && GameManager.Instance._isLive)
        {
            // 데이터매니저에 있는 구리코인스코어 증가
            DataManager._instance._cooperScore += cooper;
            ShopManager.Instance.Update_CooperText(DataManager._instance._cooperScore);
        }
    }

    // 구리코인 감소
    public void RemoveCooper(int cooper)
    {
        // 데이터매니저에 있는 구리코인스코어 감소
        DataManager._instance._cooperScore -= cooper;
        ShopManager.Instance.Update_CooperText(DataManager._instance._cooperScore);
        // 현재는 상점 화면에서 유아이매니저에 접근할 수 없으므로 잠깐 주석처리
    }

}
