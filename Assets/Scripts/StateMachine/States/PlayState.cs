

public class PlayState : BaseState
{
    private GameStateMachine gameStateMachine;
    public PlayState(GameStateMachine stateMachine) : base("PlayState", stateMachine)
    {
        gameStateMachine = (GameStateMachine)stateMachine;
    }
    public override void Enter()
    {
        UIManager.Instance.ShowLevel();
        base.Enter();
    }
    public override void UpdateLogic()
    {
        CarManager.Instance.GetCarMovement().UpdateTempVelocity();
        CarManager.Instance.GetCarMovement().RotateCarToTarget();
    }
    public override void UpdatePhysics()
    {
        CarManager.Instance.GetCarMovement().UpdateCarVelocity();

        for(int i= GameManager.Instance.movingObstacleCars.Count - 1;i>=0;i--)
        {
            GameManager.Instance.movingObstacleCars[i].SetObstacleCarVelocity();
        }
    }
    public override void Exit()
    {
    }
}
