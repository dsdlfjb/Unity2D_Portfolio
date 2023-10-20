// 자석 아이템 구현
using System.Collections;
using UnityEngine;

public class MagnetItem : MonoBehaviour
{
    public float _moveSpeed;      // 아이템의 이동속도
    public float _magnetDistance;     // 자석 작용 거리
    public float _magnetDuration = 10f;      // 자석 아이템 효과 지속 시간

    Transform _player;      // 플레이어의 위치를 저장하는 변수

    public void UseItem()
    {
        StartCoroutine(ActivateMagnet());
    }

    IEnumerator ActivateMagnet()
    {
        _player = GameObject.FindGameObjectWithTag("Player").transform;

        float dist = Vector3.Distance(transform.position, _player.position);        // Player와 아이템의 거리를 계산

        // 거리가 magnetDistance 이내일 경우 아이템을 플레이어쪽으로 이동
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
