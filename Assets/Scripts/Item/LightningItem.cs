// ����Ʈ�� ��ų������ ����
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightningItem : MonoBehaviour, IFieldDropItem
{
    public void UseItem()
    {
        // ���� �������� �Ծ��� ���� ������ �����մϴ�.
        // �ֺ� ���鿡�� ���� ȿ���� �����ϰ� ������ ó���մϴ�.
        // ��: ���� ȿ�� ���� �� �� ó�� ������ �����ϼ���.
    }

    public void DestroyItem()
    {
        Destroy(this.gameObject, 5f);
    }
}
