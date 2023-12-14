// ������ �����ϰų� �ε��ϴ� ��ũ��Ʈ
using UnityEngine;
using System.IO;

public class SaveManager : MonoBehaviour
{
    // ������Ƽ�� �����Ͽ� �ܺο��� ������ ���������� ������ �� �� ����
    public SaveData _saveData { get; private set; } = null;

    private void Awake()
    {
        _saveData = new SaveData();
        Save();
    }

    public void Save()
    {
        string path = Application.persistentDataPath + "/save.json";
        string json = JsonUtility.ToJson(_saveData, true);
        File.WriteAllText(path, json);
    }

    public void Load()
    {
        string path = Application.persistentDataPath + "/save.json";

        // �ش� ��ο� ������ �����ϴ��� Ȯ��
        if (File.Exists(path))
        {
            string data = File.ReadAllText(path);
            _saveData = JsonUtility.FromJson<SaveData>(data);
        }

        else
            _saveData = new SaveData();
    }

    #region Data Classes
    [System.Serializable]
    public class SaveData
    {
        public int _equippedSwordIndex;
        public SwordData[] _swordDatas;
        public int _upgradedSkillLevel;
        public UpgradeSkillData[] _upgradeSkillDatas;

        public SaveData()
        {
            this.Initialize();
        }

        public void Initialize()
        {
            _equippedSwordIndex = 0;
            _upgradedSkillLevel = 1;

            // �������� �ִ� ���� ��� - Scripts/Enumerations.cs
            _swordDatas = new SwordData[Define.Constant.SWORD_NUMBER];
            _upgradeSkillDatas = new UpgradeSkillData[6];

            for (int i = 0; i < _swordDatas.Length; i++)
                _swordDatas[i] = new SwordData(1000 * i);

            for (int i = 0; i < _upgradeSkillDatas.Length; i++)
                _upgradeSkillDatas[i] = new UpgradeSkillData(1000 * i);

            // �⺻ ����� �����ϰ� �־���ϹǷ� true�� �������
            _swordDatas[0]._isPurchased = true;
        }

        public SwordData GetSwordData(int index)
        {
            if (index >= 0 && index < _swordDatas.Length)
                return _swordDatas[index];

            else return null;
        }

        public UpgradeSkillData GetUpgradeSkillData(int index)
        {
            if (index >= 0 && index < _upgradeSkillDatas.Length)
                return _upgradeSkillDatas[index];
            else return null;

        }
    }

    [System.Serializable]
    public class SwordData
    {
        public int _price;
        public bool _isPurchased;

        public SwordData(int price)
        {
            this._price = price;
            _isPurchased = false;
        }
    }

    public class UpgradeSkillData
    {
        public int _price;
        public bool _isPurchased;

        public UpgradeSkillData(int price)
        {
            this._price = price;
            _isPurchased = false;
        }
    }
    #endregion
}
