using System.Threading;

// 모든 FSM 상태객체는 이 인터페이스를 상속받습니다.
public interface IState<T>
{
    // 상태로 진입했을때
    public void Enter(T target);
    // 상태 진행
    public void Execute(T target);
    // 상태에서 빠져나갈때
    public void Exit(T target);
}
