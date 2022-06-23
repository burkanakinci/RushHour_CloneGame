
public class BaseState
{
    public string name;
    protected StateMachine stateMachine;


    public BaseState(string name, StateMachine stateMachine)
    {
        this.name = name;
        this.stateMachine = stateMachine;
    }

    public virtual void Enter()
    {
        UIManager.Instance.ShowUI();
    }
    public virtual void UpdateLogic()
    {
        return;
    }
    public virtual void UpdatePhysics()
    {
        return;
    }
    public virtual void Exit()
    {
        return;
    }
}
