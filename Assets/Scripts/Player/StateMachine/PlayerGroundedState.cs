using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGroundedState : PlayerBaseState
{
    public PlayerGroundedState(PlayerStateMachine currentContext , PlayerStateFactory PlayerStateFactory): base (currentContext, PlayerStateFactory){
        _isRootState=true;
        InitializeSubState();
    }

    public override void EnterState() {
        _ctx.PlayerInfo.JumpCounter = 0;
 
        _ctx.Physics.ChangeVerticalMovement(0); 
    }

    public override void UpdateState() {
        CheckSwitchState();
    }

    public override void ExitState() {
        
    }
    
    public override void CheckSwitchState() {

        if(!_ctx.Physics.IsGrounded()) {
            SwitchState(_factory.Fall());
        }

        if(_ctx.InputManager.JumpingWasPressedThisFrame) {
            SwitchState(_factory.Jump());
        }
    }
    
    public override void InitializeSubState() {
        if(_ctx.InputManager.CurrentMovementInput == 0) {
            SetSubState(_factory.Idle());
        }
        else {
            SetSubState(_factory.Walk());
        }
    }
}
