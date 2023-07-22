using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryUI : MonoBehaviour
{
    public GameObject _inventoryPanel;
    bool _isActiveInven = false;
    InventoryManager _inven;

    public Slot[] _slots;
    public Transform _slotHolder;

    private void Start()
    {
        _inven = InventoryManager.Instance;
        _slots = _slotHolder.GetComponentsInChildren<Slot>();
        _inven.onSlotCountChange += SlotCountChange;
        _inventoryPanel.SetActive(_isActiveInven);
    }

    private void SlotCountChange(int val)
    {
        for (int i = 0; i < _slots.Length; i++)
        {
            if (i < _inven.SlotCount)
                _slots[i].GetComponent<Button>().interactable = true;

            else
                _slots[i].GetComponent<Button>().interactable = false;
        }
    }

    public void AddSlot()
    {
        _inven.SlotCount++;
    }
}
