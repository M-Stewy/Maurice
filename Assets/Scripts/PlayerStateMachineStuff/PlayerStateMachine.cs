/// <summary>
/// Made by Stewy
/// 
/// This handles the switching of the different states for the player
/// It is able to do this as all the states inherit from the PlayerState class 
///   so they all can be used in this scripts currentState variable
/// </summary>
public class PlayerStateMachine
{
    public PlayerState currentState { get; private set; }

    public void Initialize(PlayerState startingState)
    {
        currentState = startingState;
        currentState.Enter();
    }

    public void ChangeState(PlayerState newState)
    {
        currentState.Exit();
        currentState = newState;
        currentState.Enter();
    }

}
