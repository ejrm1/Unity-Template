using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerIdleState : PlayerBaseState
{
    public PlayerIdleState(PlayerStateMachine currentContext , PlayerStateFactory PlayerStateFactory): base (currentContext, PlayerStateFactory){
    }

    public override void EnterState() {
        _ctx.Physics.ChangeHorizontalMovement(0);
    }

    public override void UpdateState() {
        CheckSwitchState();
    }

    public override void ExitState() {}

    public override void CheckSwitchState()
    {
        if (_ctx.InputManager.CurrentMovementInput != 0)
        {
            SwitchState(_factory.Walk());
        }

        if (_ctx.InputManager.ShootingInput)
        {
            SwitchState(_factory.Shooting());
        }

        if (_ctx.InputManager.AbilityWasPressedThisFrame)
        {
            SwitchState(_factory.Ability());
        }
    }
    
    public override void InitializeSubState() {}
}
