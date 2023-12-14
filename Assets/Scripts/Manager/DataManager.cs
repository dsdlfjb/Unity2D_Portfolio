using UnityEngine;
using System.IO;

[System.Serializable]
public class PlayerData
{
    // 플레이어 이름, 스킨 타입, 스킨 이름, 보유 코인
    public string name;
    public int coin = 1000;
    public int maxStage = 0;
    public int upgradeSkill = 0;
}

public class DataManager : MonoBehaviour
{
    #region 싱글턴
    public static DataManager Instance;

    void Awake()
    {
        // 인스턴스가 비어있다면
        if(Instance == null)
        {
            // 자기자신으로 채우고, 씬 이동시에도 삭제되지 않도록
            Instance =  this;
            DontDestroyOnLoad(gameObject);
        }
        // 인스턴스가 비어있지 않지만 자기자신과 다르다면 = 새로 생긴 객체
        else if(Instance != this)
        {
            // 자기자신 삭제
            Destroy(gameObject);
        }

        path = Application.persistentDataPath + "/save";
    }
    #endregion

    public int _nowSlot;
    public string _swordSkinName;
    public string _bulletSkinName;

    public string path;

    public PlayerData nowPlayer= new PlayerData();

    // 코인 증가
    public void AddCoin(int coinScore)
    {
        // 현재, 게임매니저가 없는 씬이 있어서 게임매니저를 찾을 수 있을 때만 작동하도록
        if (GameManager.Instance && GameManager.Instance._isLive)
        {
            // 데이터매니저에 있는 코인스코어 증가
            DataManager.Instance.nowPlayer.coin += coinScore;
            UIManager.Instance.Update_CoinText(DataManager.Instance.nowPlayer.coin);
        }
    }

    // 코인 감소
    public void RemoveCoin(int coinScore)
    {
        // 데이터매니저에 있는 코인스코어 감소
        DataManager.Instance.nowPlayer.coin -= coinScore;

        UIManager.Instance.Update_CoinText(DataManager.Instance.nowPlayer.coin);
    }

    /*
    // 구리코인 증가
    public void AddCooper(int cooper)
    {
        // 현재, 게임매니저가 없는 씬이 있어서 게임매니저를 찾을 수 있을 때만 작동하도록
        if (GameManager.Instance && GameManager.Instance._isLive)
        {
            // 데이터매니저에 있는 구리코인스코어 증가
            DataManager.Instance.nowPlayer.cooper += cooper;
            UIManager.Instance.Update_CooperText(DataManager.Instance.nowPlayer.cooper);
        }
    }

    // 구리코인 감소
    public void RemoveCooper(int cooper)
    {
        // 데이터매니저에 있는 구리코인스코어 감소
        DataManager.Instance.nowPlayer.cooper -= cooper;
        //UIManager.Instance.Update_CooperText(DataManager.Instance.nowPlayer.cooper);
        // 현재는 상점 화면에서 유아이매니저에 접근할 수 없으므로 잠깐 주석처리
    }
    */

    // 저장 기능
    public void Save()
    {
        string data = JsonUtility.ToJson(nowPlayer, true);
        File.WriteAllText(path + _nowSlot.ToString(), data);
    }

    // 불러오기 기능
    public void Load()
    {
        string data = File.ReadAllText(path + _nowSlot.ToString());
        nowPlayer = JsonUtility.FromJson<PlayerData>(data);
    }

    public void DataReset()
    {
        _nowSlot = -1;
        nowPlayer = new PlayerData();
    }
}
