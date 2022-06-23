

public class FinishState : BaseState
{

    private GameStateMachine gameStateMachine;

    public FinishState(GameStateMachine stateMachine) : base("FinishState", stateMachine)
    {
        gameStateMachine = (GameStateMachine)stateMachine;
    }
    public override void Enter()
    {
        base.Enter();

        CarManager.Instance.GetCarMovement().ResetCarVelocity();
    }
    public override void Exit()
    {
    }
}
