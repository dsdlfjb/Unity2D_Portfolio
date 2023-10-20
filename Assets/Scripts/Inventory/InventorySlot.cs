using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InventorySlot : MonoBehaviour
{
    [SerializeField] private Image _swordImage;
    [SerializeField] private TextMeshProUGUI _equippedText;

    private int _index = -1;

    private void Awake()
    {
        if (Managers.Instance != null)
        {
            Managers.Event.RegistEvent(Enum.EEventKey.OnClickInventorySlot, OnClickedItemSlot);
            Managers.Event.RegistEvent(Enum.EEventKey.OnPurchaseSword, OnPurchasedSword);
        }
    }

    private void OnDestroy()
    {
        if (Managers.Instance != null)
        {
            Managers.Event.RemoveEvent(Enum.EEventKey.OnClickInventorySlot, OnClickedItemSlot);
            Managers.Event.RemoveEvent(Enum.EEventKey.OnPurchaseSword, OnPurchasedSword);
        }
    }

    private void OnClickedItemSlot(object index)
    {
        if (_index >= 0 && Managers.Save._saveData.GetSwordData(_index)._isPurchased && _index == (int)index)
        {
            // 장착중 텍스트를 킴
            _equippedText.enabled = true;
        }

        else
        {
            // 아니라면 텍스트를 끔
            _equippedText.enabled = false;
        }
            
    }

    private void OnPurchasedSword(object index)
    {
        if((int)index == _index)
        {
            // 구입했을 때 UI를 새로고침 해줌
            // SetData() 함수 내에 있는 기능 중 사용하지 않아도 되는 기능도 있지만,
            // 코드 작성 시간을 줄이기 위해 재활용함
            this.SetData(_index);
        }
    }

    public void SetData(int index)
    {
        _index = index;

        _swordImage.sprite = Managers.Resource.GetSprite(Enum.ESpriteKey.Sword_1 + index);

        _swordImage.color = Managers.Save._saveData.GetSwordData(_index)._isPurchased ? Color.white : Color.black;
        // 현재 장착된 칼의 Index와 이 슬롯의 index가 일치하면 텍스트를 키고 아니면 꺼짐
        _equippedText.enabled = Managers.Save._saveData._equippedSwordIndex == index;

    }

    public void Click()
    {
        if (_index >= 0 && Managers.Save._saveData.GetSwordData(_index)._isPurchased)
        {
            // 현재 장착중인 칼을 지금칼로 바꾸도록 함
            Managers.Save._saveData._equippedSwordIndex = _index;

            // EventManager에 등록된 함수들을 호출하기 위해서 InvokeEvent 함수를 호출함
            Managers.Event.InvokeEvent(Enum.EEventKey.OnClickInventorySlot, _index);

            // 변경된 데이터를 저장
            Managers.Save.Save();
        }
    }
}
