using UnityEngine;

public class InventoryUI : MonoBehaviour
{
    [SerializeField] private GameObject _inventorySlotPrefeb;
    [SerializeField] private Transform _contentTransform;

    private void Awake()
    {
        for (int i = 0; i < Define.Constant.SWORD_NUMBER; i++)
        {
            Instantiate(_inventorySlotPrefeb, _contentTransform).GetComponent<InventorySlot>().SetData(i);
        }

        //this.gameObject.SetActive(false);
    }
}
