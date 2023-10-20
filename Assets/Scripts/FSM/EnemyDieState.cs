using UnityEngine;

public class EnemyDieState : EnemyState
{
    public override void Enter(Enemy target)
    {
        // 적 사망 시퀀스
        Debug.Log("적이 처치되었습니다.");

        target.Dead();
    }

    public override void Execute(Enemy target) { }

    public override void Exit(Enemy target) { }
}