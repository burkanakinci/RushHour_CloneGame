
public class GameStateMachine : StateMachine
{
    public TapToPlayState tapToPlayState { get; private set; }
    public PlayState playState { get; private set; }
    public FinishState finishState { get; private set; }
    public FailState failState { get; private set; }

    private void Awake()
    {
        tapToPlayState = new TapToPlayState(this);
        playState = new PlayState(this);
        finishState = new FinishState(this);
        failState = new FailState(this);
    }

    public void ChangeInitialState()
    {
        ChangeState(GetInitialState());
    }

    protected override BaseState GetInitialState()
    {
        return tapToPlayState;
    }
}
