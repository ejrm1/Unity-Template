using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShootingState : PlayerBaseState
{
    public PlayerShootingState(PlayerStateMachine currentContext , PlayerStateFactory PlayerStateFactory): base (currentContext, PlayerStateFactory){
    }

    public override void EnterState() {
        if(!_ctx.CDManager.IsAbilityInCooldown("Shooting")){
        
            _ctx.CDManager.StartAbilityCooldown("Shooting");
        }
    }

    public override void UpdateState() {
        CheckSwitchState();
    }

    public override void ExitState() {}
    
    public override void CheckSwitchState() {
        if(_ctx.InputManager.CurrentMovementInput != 0){
            SwitchState(_factory.Walk());
        } else {
            SwitchState(_factory.Idle());
        }
    }
    
    public override void InitializeSubState() {}
}
