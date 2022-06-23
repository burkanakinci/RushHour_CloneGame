
public class FailState : BaseState
{

    private GameStateMachine gameStateMachine;

    public FailState(GameStateMachine stateMachine) : base("FailState", stateMachine)
    {
        gameStateMachine = (GameStateMachine)stateMachine;
    }
    public override void Enter()
    {
        base.Enter();

    }

    public override void UpdatePhysics()
    {
        for (int i = GameManager.Instance.movingObstacleCars.Count - 1; i >= 0; i--)
        {
            GameManager.Instance.movingObstacleCars[i].ResetObstacleCarVelocity();
            CarManager.Instance.GetCarMovement().ResetCarVelocity();
        }
    }
    public override void Exit()
    {
    }
}
