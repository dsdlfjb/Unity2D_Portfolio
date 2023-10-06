using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Enum;

public class InventorySlot : MonoBehaviour
{
    [SerializeField] private Image _swordImage;

    // ���� ��ǥ�迡���� TextMeshPro�� ����ϰ�,
    // UI������ TextMeshProUGUI�� ����մϴ�.
    [SerializeField] private TextMeshProUGUI _equippedText;

    private int _index = -1;

    private void Awake()
    {
        // �� ���ǹ��� ���� ���������� Manager�� Null�̰ų�
        // ���α׷��� ����� ������ Manager�� ������� �ʵ��� �ϱ� ����
        // ���� ���α׷��� ����� �� Manager�� ���ؼ� �ٸ� �Ŵ����� �����Ϸ��Ѵٸ� NullReference ������ ��Ÿ��
        if (Managers.Instance != null)
        {
            // EventManager�� �Լ��� ���
            Managers.Event.RegistEvent(EEventKey.OnClickInventorySlot, OnClickedItemSlot);
            Managers.Event.RegistEvent(EEventKey.OnPurcharseSword, OnPurchasedSword);
        }
    }

    private void OnDestroy()
    {
        // �ݵ�� ����ߴ� �̺�Ʈ�� ��������
        if (Managers.Instance != null)
        {
            Managers.Event.RemoveEvent(EEventKey.OnClickInventorySlot, OnClickedItemSlot);
            Managers.Event.RemoveEvent(EEventKey.OnPurcharseSword, OnPurchasedSword);
        }
    }

    private void OnClickedItemSlot(object index)
    {
        // EventManager���� ����ߴ� �̺�Ʈ�� ȣ��Ǿ �̰����� ����

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
            // �������� �� UI�� ���ΰ�ħ�� ����
            // SetData()�Լ� ���� �ִ� ��� �� ������� �ʾƵ� �Ǵ� ��ɵ� ������,
            // �ڵ� �ۼ� �ð��� ���̱����� ��Ȱ����
            this.SetData(_index);
        }
    }


    public void SetData(int index)
    {
        _index = index;

        // �����ξ��� Manager.Resource���� ��������Ʈ�� �����µ� ��������Ʈ�� ��������
        _swordImage.sprite = Managers.Resouce.GetSprite(ESpriteKey.Sword_1 + index);

        _swordImage.color = Managers.Save.saveData.GetSwordData(_index)._isPurchased ? Color.white : Color.black;

        // ���� ������ Į�� Index�� �� ������ Index�� ��ġ�ϸ� �ؽ�Ʈ�� Ű�� �ƴϸ� ������ ��
        _equippedText.enabled = Managers.Save.saveData.EquippedSwordIndex == index;
    }

    public void Click()
    {
        // _index >= 0�� ����ó��
        // �̹� ������ Į�̶��
        if (_index >= 0 && Managers.Save.saveData.GetSwordData(_index)._isPurchased)
        {
            // ���� �������� Į�� ����Į�� �ٲٵ��� ��
            Managers.Save.saveData.EquippedSwordIndex = _index;
            
            // EventManager�� ��ϵ� �Լ����� ȣ���ϱ����ؼ� InvokeEvent�Լ��� ȣ��
            Managers.Event.InvokeEvent(EEventKey.OnClickInventorySlot, _index);

            // ����� �����͸� ����
            Managers.Save.SaveData();
        }
    }
}
