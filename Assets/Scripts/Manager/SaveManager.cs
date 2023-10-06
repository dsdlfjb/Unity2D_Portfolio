using UnityEngine;
using System.IO;

// 세이브파일이 너무 커진다면, 열거형으로 구분해서 저장해도됨
public class SaveManager : MonoBehaviour
{
    // 프로퍼티로 선언하여 외부에서 접근은 가능하지만 수정은 불가능
    public SaveData saveData { get; private set; } = null;

    private void Awake()
    {
        saveData = new SaveData();
        LoadData();
    }

    public void SaveData()
    {
        string path = Application.persistentDataPath + "/save.json";
        string json = JsonUtility.ToJson(saveData);
        File.WriteAllText(path, json);
    }

    public void LoadData()
    {
        string path = Application.persistentDataPath + "/save.json";

        // 해당 경로에 파일이 있는지 확인
        if (File.Exists(path))
        {
            string data = File.ReadAllText(path);
            saveData = JsonUtility.FromJson<SaveData>(data);
        }
        else
        {
            saveData = new SaveData();
        }
    }
}

#region Data Classes
// [] 안에 있는 클래스 또는 변수 등의 위에 있는 이것을 어트리뷰트라고 함
// [System.Serializable] 어트리뷰트는 해당 클래스를 직렬화 시켜서 저장 또는 서버와의 통신에 용이하도록 하기 위해서임

[System.Serializable]
public class SaveData
{
    public int EquippedSwordIndex;
    public SwordData[] SwordDatas;

    public SaveData()
    {
        this.Initialize();
    }

    public void Initialize()
    {
        EquippedSwordIndex = 0;

        SwordDatas = new SwordData[Define.Constant.SWORD_NUMBER];

        // 가격은 임시로 세팅
        for (int i = 0; i < SwordDatas.Length; i++)
            SwordDatas[i] = new SwordData(1000 * i);

        // 기본 무기는 소지하고 있어야하기 때문에
        // true로 만들어줌
        SwordDatas[0]._isPurchased = true;
    }

    // 외부 클래스에서 사용하기 편하도록 함수를 작성
    // 이런 편의기능을 작성하지 않으면 외부에서 코드가 너무 길어지거나,
    // 참조하기 위한 변수들이 많이 생겨날 수 있음
    public SwordData GetSwordData(int index)
    {
        if(index >= 0 && index < SwordDatas.Length)
        {
            return SwordDatas[index];
        }
        else
        {
            return null;
        }
    }
}

[System.Serializable]
public class SwordData
{
    public int _price;
    public bool _isPurchased;

    // 생성자를 통해서 new할 때 바로 초기화하도록
    public SwordData(int price)
    {
        this._price = price;
        _isPurchased = false;
    }
}

#endregion