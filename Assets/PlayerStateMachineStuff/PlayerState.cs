public class PlayerState
{

    protected Player player;
    protected PlayerData playerData;
    protected PlayerStateMachine playerStateMachine;


    public PlayerState(Player player, PlayerData playerData, PlayerStateMachine playerStateMachine)
    {
        this.player = player;
        this.playerData = playerData;
        this.playerStateMachine = playerStateMachine;
    }

    public virtual void Enter()
    {
        Checks();
    }

    public virtual void Update()
    {
        Checks();

    }

    public virtual void FixedUpdate() { }

    public virtual void Exit() { }

    public virtual void Checks()
    {

    }

}
