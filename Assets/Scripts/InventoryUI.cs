using UnityEngine;
public class InventoryUI : MonoBehaviour
{
    [SerializeField] private GameObject _inventorySlotPrefab;
    [SerializeField] private Transform _contentTransform;

    private void Awake()
    {
        // �����ص״� Define ���ӽ����̽� �ȿ� �ִ� static class Constant����
        // Į�� ������ ����� �޾ƿ�
        // �̷����ϸ� ���� Į�� �þ�ų� �پ�� 'Define.Constant.SWORD_NUMBER'�� �����ϸ� �Ǳ� ������ ������ ����

        for (int i = 0; i < Define.Constant.SWORD_NUMBER; i++)
        {
            // Instantiate��� �Լ��� original�̶�� �Ķ���Ϳ� �� ���ӿ�����Ʈ�� �����ϰ� GameObject�� ���Ͻ�Ŵ
            // �̷����ϸ� ��Ÿ���߿�(�÷��� ���߿�) ��ũ��Ʈ�� ����ؼ� ���ӿ�����Ʈ�� ����
            // GetComponent�� InventorySlot�� ã�� SetData�� ȣ��
            Instantiate(_inventorySlotPrefab,_contentTransform).GetComponent<InventorySlot>().SetData(i);
        }

        this.gameObject.SetActive(false);
    }
}
