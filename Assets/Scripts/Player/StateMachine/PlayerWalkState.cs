using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWalkState : PlayerBaseState
{

    public PlayerWalkState(PlayerStateMachine currentContext , PlayerStateFactory PlayerStateFactory): base (currentContext, PlayerStateFactory){
    }
    public override void EnterState() {
        
    }

    public override void UpdateState() {
        _ctx.Physics.Move(_ctx.InputManager.CurrentMovementInput * _ctx.PlayerInfo.MoveSpeed);

        if( _ctx.InputManager.CurrentMovementInput > 0.01f){
            _ctx.PlayerInfo.LastDirection = 0;
        }
        else if(_ctx.InputManager.CurrentMovementInput < -0.01f){
            _ctx.PlayerInfo.LastDirection = 1;
        }

        CheckSwitchState();
    }

    public override void ExitState() {}
    
    public override void CheckSwitchState() {

        if(_ctx.InputManager.CurrentMovementInput == 0){
            SwitchState(_factory.Idle());
        }

        if (_ctx.InputManager.ShootingInput) {
            SwitchState(_factory.Shooting());
        }

        if (_ctx.InputManager.AbilityWasPressedThisFrame)
        {
            SwitchState(_factory.Ability());
        }
    }
    
    public override void InitializeSubState() {}
}
