using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Enum;

public class InventorySlot : MonoBehaviour
{
    [SerializeField] private Image _swordImage;

    // 월드 좌표계에서는 TextMeshPro를 사용하고,
    // UI에서는 TextMeshProUGUI를 사용합니다.
    [SerializeField] private TextMeshProUGUI _equippedText;

    private int _index = -1;

    private void Awake()
    {
        // 이 조건문은 현재 오류로인해 Manager가 Null이거나
        // 프로그램이 종료될 때에는 Manager를 사용하지 않도록 하기 위함
        // 만약 프로그램이 종료될 때 Manager를 통해서 다른 매니저에 접근하려한다면 NullReference 오류가 나타남
        if (Managers.Instance != null)
        {
            // EventManager에 함수를 등록
            Managers.Event.RegistEvent(EEventKey.OnClickInventorySlot, OnClickedItemSlot);
            Managers.Event.RegistEvent(EEventKey.OnPurcharseSword, OnPurchasedSword);
        }
    }

    private void OnDestroy()
    {
        // 반드시 등록했던 이벤트는 지워야함
        if (Managers.Instance != null)
        {
            Managers.Event.RemoveEvent(EEventKey.OnClickInventorySlot, OnClickedItemSlot);
            Managers.Event.RemoveEvent(EEventKey.OnPurcharseSword, OnPurchasedSword);
        }
    }

    private void OnClickedItemSlot(object index)
    {
        // EventManager에서 등록했던 이벤트가 호출되어서 이곳으로 들어옴

        if (_index >= 0 && Managers.Save.saveData.GetSwordData(_index)._isPurchased && _index == (int)index)
        {
            _equippedText.enabled = true;
        }

        else
        {
            _equippedText.enabled = false;
        }
    }

    private void OnPurchasedSword(object index)
    {
        if((int)index == _index)
        {
            // 구입했을 때 UI를 새로고침을 해줌
            // SetData()함수 내에 있는 기능 중 사용하지 않아도 되는 기능도 있지만,
            // 코드 작성 시간을 줄이기위해 재활용함
            this.SetData(_index);
        }
    }


    public void SetData(int index)
    {
        _index = index;

        // 만들어두었던 Manager.Resource에서 스프라이트를 가져온뒤 스프라이트를 세팅해줌
        _swordImage.sprite = Managers.Resouce.GetSprite(ESpriteKey.Sword_1 + index);

        _swordImage.color = Managers.Save.saveData.GetSwordData(_index)._isPurchased ? Color.white : Color.black;

        // 현재 장착된 칼의 Index와 이 슬롯의 Index가 일치하면 텍스트를 키고 아니면 끄도록 함
        _equippedText.enabled = Managers.Save.saveData.EquippedSwordIndex == index;
    }

    public void Click()
    {
        // _index >= 0은 예외처리
        // 이미 구입한 칼이라면
        if (_index >= 0 && Managers.Save.saveData.GetSwordData(_index)._isPurchased)
        {
            // 현재 장착중인 칼을 지금칼로 바꾸도록 함
            Managers.Save.saveData.EquippedSwordIndex = _index;
            
            // EventManager에 등록된 함수들을 호출하기위해서 InvokeEvent함수를 호출
            Managers.Event.InvokeEvent(EEventKey.OnClickInventorySlot, _index);

            // 변경된 데이터를 저장
            Managers.Save.SaveData();
        }
    }
}
