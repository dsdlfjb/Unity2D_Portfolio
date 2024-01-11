// 파일을 저장하거나 로드하는 스크립트
using UnityEngine;
using System.IO;

public class SaveManager : MonoBehaviour
{
    // 프로퍼티로 선언하여 외부에서 접근은 가능하지만 수정은 할 수 없음
    public SaveData _saveData { get; private set; } = null;

    private void Awake()
    {
        string path = Application.persistentDataPath + "/save.json";

        if(!string.IsNullOrEmpty(path)) //파일이 존재할 경우
        {
            Load();
        }
        else //파일이 없을 경우 새로 시작하기
        {
            _saveData = new SaveData();
            Save();
        }
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

        // 해당 경로에 파일이 존재하는지 확인
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

            // 열거형이 있는 파일 경로 - Scripts/Enumerations.cs
            _swordDatas = new SwordData[Define.Constant.SWORD_NUMBER];
            _upgradeSkillDatas = new UpgradeSkillData[6];

            for (int i = 0; i < _swordDatas.Length; i++)
                _swordDatas[i] = new SwordData(1000 * i);

            for (int i = 0; i < _upgradeSkillDatas.Length; i++)
                _upgradeSkillDatas[i] = new UpgradeSkillData(1000 * i);

            // 기본 무기는 소지하고 있어야하므로 true로 만들어줌
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
