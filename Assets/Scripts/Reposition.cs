using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reposition : MonoBehaviour
{
    Collider2D _coll;

    private void Awake()
    {
        _coll = GetComponent<Collider2D>();
    }

    // Ʈ���� �浹ü�� �浹�� ���� ������ ȣ��
    void OnTriggerExit2D(Collider2D collision)
    {
        // Area �±׸� ���� ������Ʈ�� �� �Լ� return (�Լ� ����)
        if (!collision.CompareTag("Area"))
        {
            return;
        }

        // �÷��̾� ��ġ �ʱ�ȭ
        Vector3 playerPos = GameManager.Instance._player.transform.position;
        // ���� ������Ʈ (Ÿ�ϸ�) ��ġ �ʱ�ȭ
        Vector3 myPos = transform.position;

        // ���� ������Ʈ�� �±��� ������ switch���� �����մϴ�.
        switch (transform.tag)
        {
            // ���� �±װ� Ground�� ��
            case "Ground":
                // �÷��̾� ��ġ�� ���� ������Ʈ ������ �Ÿ��� ����մϴ�.
                // Mathf �Լ��� Abs �� ����Ͽ� playerPos�� myPos ������ �Ÿ��� ���밪���� ��ȯ�մϴ�.
                float diffX = playerPos.x - myPos.x;
                float diffY = playerPos.y - myPos.y;

                // playerDir.x < 0 �Ǵ� playerDir.y < 0�� True�� �� -1�� False�� �� 1�� ��ȯ�ϴ� ���׿������Դϴ�.
                // �Ʒ��� ���׿����ڸ� if, else������ ��Ÿ����
                //
                // float dirX = 0f;
                // if (playerDir.x < 0)
                // {
                //    dirX = -1;
                // }
                // else
                // {
                //    dirX = 1;
                // }
                //
                // if, else�� ��� �� �ڵ尡 ������⿡ ���׿����ڸ� ����մϴ�.
                // �ʱ�ȭ ��� = ���� ? Ture�� �� ��� : False�� �� ���; �÷� ����մϴ�.
                float dirX = diffX < 0 ? -1 : 1;
                float dirY = diffY < 0 ? -1 : 1;
                diffX = Mathf.Abs(diffX);
                diffY = Mathf.Abs(diffY);

                // �÷��̾ x�� �������� �����̴� �� �� �� Ÿ�ϸ��� x���� * 2 ��ŭ(28)�� �̵���ŵ�ϴ�.
                // ������������ �پ��� Ÿ�ϸ��� ũ�Ⱑ ������ �� ������ ���� * 2 �κ��� ������ ���� ���ξ� �����ϴ� ���� Ȯ�强 ���鿡�� ������ �� �ֽ��ϴ�.
                if (diffX >= diffY)
                {
                    transform.Translate(Vector3.right * dirX * 28);
                }
                // �÷��̾ y�� �������� �����̴� �� �� �� Ÿ�ϸ��� y���� * 2 ��ŭ(24)�� �̵���ŵ�ϴ�.
                else if (diffX < diffY)
                {
                    transform.Translate(Vector3.up * dirY * 24);
                }

                Vector3 pos = transform.position;
                pos.z = 1;
                transform.position = pos;
                break;

            case "Enemy":
                if (_coll.enabled)
                {
                    Vector3 dist = playerPos - myPos;
                    Vector3 rnd = new Vector3(Random.Range(-3, 3), Random.Range(-3, 3), 0);
                    transform.Translate(rnd + dist * 2);
                }
                break;
        }
    }
}
