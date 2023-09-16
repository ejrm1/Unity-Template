

public class PlayerStateFactory {
    PlayerStateMachine _context;

    public PlayerStateFactory (PlayerStateMachine currentContext){
        _context = currentContext;
    }

    public PlayerBaseState Idle (){
        return new PlayerIdleState(_context, this);
    }

    public PlayerBaseState Jump (){
        return new PlayerJumpState(_context, this);
    }

    public PlayerBaseState Walk (){
        return new PlayerWalkState(_context, this);
    }

    public PlayerBaseState Grounded(){
        return new PlayerGroundedState(_context, this);
    }

    public PlayerBaseState Fall(){
        return new PlayerFallState(_context, this);
    }
    public PlayerBaseState Shooting(){
        return new PlayerShootingState(_context, this);
    }
    public PlayerBaseState Ability()
    {
        return new PlayerAbilityState(_context, this);
    }
    




}

