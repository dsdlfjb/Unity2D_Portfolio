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

    // 트리거 충돌체간 충돌이 끝난 시점에 호출
    void OnTriggerExit2D(Collider2D collision)
    {
        // Area 태그를 가진 오브젝트일 시 함수 return (함수 종료)
        if (!collision.CompareTag("Area"))
        {
            return;
        }

        // 플레이어 위치 초기화
        Vector3 playerPos = GameManager.Instance._player.transform.position;
        // 현재 오브젝트 (타일맵) 위치 초기화
        Vector3 myPos = transform.position;

        // 현재 오브젝트의 태그의 값으로 switch문을 실행합니다.
        switch (transform.tag)
        {
            // 현재 태그가 Ground일 때
            case "Ground":
                // 플레이어 위치와 현재 오브젝트 사이의 거리를 계산합니다.
                // Mathf 함수의 Abs 를 사용하여 playerPos와 myPos 사이의 거리를 절대값으로 반환합니다.
                float diffX = playerPos.x - myPos.x;
                float diffY = playerPos.y - myPos.y;

                // playerDir.x < 0 또는 playerDir.y < 0가 True일 때 -1을 False일 때 1을 반환하는 삼항연산자입니다.
                // 아래의 삼항연산자를 if, else문으로 나타내면
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
                // if, else문 사용 시 코드가 길어지기에 삼항연산자를 사용합니다.
                // 초기화 대상 = 조건 ? Ture일 때 결과 : False일 때 결과; 꼴로 사용합니다.
                float dirX = diffX < 0 ? -1 : 1;
                float dirY = diffY < 0 ? -1 : 1;
                diffX = Mathf.Abs(diffX);
                diffY = Mathf.Abs(diffY);

                // 플레이어가 x축 방향으로 움직이는 중 일 때 타일맵의 x길이 * 2 만큼(28)을 이동시킵니다.
                // 스테이지별로 다양한 타일맵의 크기가 존재할 수 있으니 길이 * 2 부분은 변수로 따로 빼두어 관리하는 것이 확장성 측면에서 유리할 수 있습니다.
                if (diffX >= diffY)
                {
                    transform.Translate(Vector3.right * dirX * 28);
                }
                // 플레이어가 y축 방향으로 움직이는 중 일 때 타일맵의 y길이 * 2 만큼(24)을 이동시킵니다.
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
