using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class InventoryManager : MonoBehaviour
{
    #region �̱���
    public static InventoryManager Instance;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }

        else if (Instance != this)
        {
            Destroy(gameObject);
        }
    }
    #endregion

    public delegate void OnSlotCountChange(int val);
    public OnSlotCountChange onSlotCountChange;

    public delegate void OnChangeSkin();
    public OnChangeSkin onChangeSkin;

    int _slotCount;
    public int SlotCount
    {
        get => _slotCount;
        set
        {
            _slotCount = value;
            onSlotCountChange.Invoke(_slotCount);
        }
    }

    public SkinList _skinList;
    public string path;
    public int nowNum;

    public List<Sword> _skins = new List<Sword>();

    private void Start()
    {
        SlotCount = 3;
    }

    public void EquipSkin(int index)
    {
        // �κ��丮���� ���� ���� �� ����
        if (index >= 0 && index < _skinList._swords.Count)
        {
            _skinList._swords[index]._isEquipped = true;
            SaveInventoryToJson();
        }
    }

    public void SaveInventoryToJson()
    {
        string data = JsonUtility.ToJson(_skinList);
        File.WriteAllText(path + nowNum.ToString(), data);
        // jsonData�� ���Ϸ� �����ϰų� �ٸ� ���� ����� ����մϴ�.
    }

    public bool AddSkin(Sword skin)
    {
        if (_skins.Count < SlotCount)
        {
            _skins.Add(skin);
            onChangeSkin.Invoke();
            return true;
        }
        return false;
    }
}
