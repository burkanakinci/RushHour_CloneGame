using UnityEngine;

public class TapToPlayState : BaseState
{

    private GameStateMachine gameStateMachine;

    public TapToPlayState(GameStateMachine stateMachine) : base("TapToPlayState", stateMachine)
    {
        gameStateMachine = (GameStateMachine)stateMachine;
    }
    public override void Enter()
    {
        CarManager.Instance.ResetCarValues();

            base.Enter();
    }
    public override void UpdateLogic()
    {
        if (Input.GetMouseButtonDown(0))
        {
            CarManager.Instance.GetCarMovement().UpdateTempVelocity();
            CarManager.Instance.GetCarMovement().RotateCarToTarget();

            GameManager.Instance.GetGameStateMachine().ChangeState(GameManager.Instance.GetGameStateMachine().playState);
        }
    }
    public override void Exit()
    {
        UIManager.Instance.ResetUI();
    }
}
