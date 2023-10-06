using UnityEngine;
using System.IO;

// ���̺������� �ʹ� Ŀ���ٸ�, ���������� �����ؼ� �����ص���
public class SaveManager : MonoBehaviour
{
    // ������Ƽ�� �����Ͽ� �ܺο��� ������ ���������� ������ �Ұ���
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

        // �ش� ��ο� ������ �ִ��� Ȯ��
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
// [] �ȿ� �ִ� Ŭ���� �Ǵ� ���� ���� ���� �ִ� �̰��� ��Ʈ����Ʈ��� ��
// [System.Serializable] ��Ʈ����Ʈ�� �ش� Ŭ������ ����ȭ ���Ѽ� ���� �Ǵ� �������� ��ſ� �����ϵ��� �ϱ� ���ؼ���

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

        // ������ �ӽ÷� ����
        for (int i = 0; i < SwordDatas.Length; i++)
            SwordDatas[i] = new SwordData(1000 * i);

        // �⺻ ����� �����ϰ� �־���ϱ� ������
        // true�� �������
        SwordDatas[0]._isPurchased = true;
    }

    // �ܺ� Ŭ�������� ����ϱ� ���ϵ��� �Լ��� �ۼ�
    // �̷� ���Ǳ���� �ۼ����� ������ �ܺο��� �ڵ尡 �ʹ� ������ų�,
    // �����ϱ� ���� �������� ���� ���ܳ� �� ����
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

    // �����ڸ� ���ؼ� new�� �� �ٷ� �ʱ�ȭ�ϵ���
    public SwordData(int price)
    {
        this._price = price;
        _isPurchased = false;
    }
}

#endregion