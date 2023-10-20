// �ڼ� ������ ����
using System.Collections;
using UnityEngine;

public class MagnetItem : MonoBehaviour
{
    public float _moveSpeed;      // �������� �̵��ӵ�
    public float _magnetDistance;     // �ڼ� �ۿ� �Ÿ�
    public float _magnetDuration = 10f;      // �ڼ� ������ ȿ�� ���� �ð�

    Transform _player;      // �÷��̾��� ��ġ�� �����ϴ� ����

    public void UseItem()
    {
        StartCoroutine(ActivateMagnet());
    }

    IEnumerator ActivateMagnet()
    {
        _player = GameObject.FindGameObjectWithTag("Player").transform;

        float dist = Vector3.Distance(transform.position, _player.position);        // Player�� �������� �Ÿ��� ���

        // �Ÿ��� magnetDistance �̳��� ��� �������� �÷��̾������� �̵�
        if (dist <= _magnetDistance)
        {
            transform.position = Vector3.MoveTowards(transform.position, _player.position, _moveSpeed * Time.deltaTime);
        }

        yield return new WaitForSeconds(_magnetDuration);
    }

    public void DestroyItem()
    {
        Destroy(this.gameObject, 5f);
    }
}
