using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour
{
    #region ╫л╠шео
    static InventoryManager _instance;
    public static InventoryManager Instance
    {
        get
        {
            if (_instance == null)
                _instance = FindObjectOfType<InventoryManager>();
            return _instance;
        }
    }
    #endregion


}
