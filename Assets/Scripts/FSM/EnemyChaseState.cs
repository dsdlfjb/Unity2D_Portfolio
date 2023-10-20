using UnityEngine;

public class EnemyChaseState : EnemyState
{
    float time = 0;

    public override void Enter(Enemy target) { }

    public override void Execute(Enemy target)
    {
        time += Time.deltaTime;


        // 방향 벡터
        var dir = (target._playerTrnsf.position - target.transform.position).normalized;

        // 플레이어 사이와의 거리
        var distance = (target._playerTrnsf.position - target.transform.position).magnitude;

        target.MoveToPlayer(dir);

        // 플레이어와의 거리가 1보다 작다면 AttackState로 이동
        if (distance < 1f && time >= target._attackDelay)
        {
            time = 0;
            target.ChangeState(EEnemyState.Attack);
        }
    }

    public override void Exit(Enemy target) { }
}