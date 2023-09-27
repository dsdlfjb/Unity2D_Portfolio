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
            // ������ �ؽ�Ʈ�� Ŵ
            _equippedText.enabled = true;
        }

        else
        {
            // �ƴ϶�� �ؽ�Ʈ�� ��
            _equippedText.enabled = false;
        }
            
    }

    private void OnPurchasedSword(object index)
    {
        if((int)index == _index)
        {
            // �������� �� UI�� ���ΰ�ħ ����
            // SetData() �Լ� ���� �ִ� ��� �� ������� �ʾƵ� �Ǵ� ��ɵ� ������,
            // �ڵ� �ۼ� �ð��� ���̱� ���� ��Ȱ����
            this.SetData(_index);
        }
    }

    public void SetData(int index)
    {
        _index = index;

        _swordImage.sprite = Managers.Resource.GetSprite(Enum.ESpriteKey.Sword_1 + index);

        _swordImage.color = Managers.Save._saveData.GetSwordData(_index)._isPurchased ? Color.white : Color.black;
        // ���� ������ Į�� Index�� �� ������ index�� ��ġ�ϸ� �ؽ�Ʈ�� Ű�� �ƴϸ� ����
        _equippedText.enabled = Managers.Save._saveData._equippedSwordIndex == index;

    }

    public void Click()
    {
        if (_index >= 0 && Managers.Save._saveData.GetSwordData(_index)._isPurchased)
        {
            // ���� �������� Į�� ����Į�� �ٲٵ��� ��
            Managers.Save._saveData._equippedSwordIndex = _index;

            // EventManager�� ��ϵ� �Լ����� ȣ���ϱ� ���ؼ� InvokeEvent �Լ��� ȣ����
            Managers.Event.InvokeEvnet(Enum.EEventKey.OnClickInventorySlot, _index);

            // ����� �����͸� ����
            Managers.Save.Save();
        }
    }
}
