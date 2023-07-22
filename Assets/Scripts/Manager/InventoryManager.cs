using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour
{
    #region ╫л╠шео
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

    public List<Skin> _skins = new List<Skin>();

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

    private void Start()
    {
        SlotCount = 3;
    }

    public bool AddSkin(Skin skin)
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
