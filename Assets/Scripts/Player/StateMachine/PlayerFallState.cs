    using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFallState : PlayerBaseState
{
    public PlayerFallState(PlayerStateMachine currentContext , PlayerStateFactory PlayerStateFactory): base (currentContext, PlayerStateFactory){
        _isRootState=true;
        InitializeSubState();
    }
    public override void EnterState() {
        _ctx.Physics.ChangeGravityScale(1f);

    }

    public override void UpdateState() {

        CheckSwitchState();

    }

    public override void ExitState() {



    }
    
    public override void CheckSwitchState() {
        if(_ctx.Physics.IsGrounded()){
            Debug.Log("grounded");
            SwitchState(_factory.Grounded());
        }


    }
    
    public override void InitializeSubState() {

        if(_ctx.InputManager.CurrentMovementInput == 0){

            SetSubState(_factory.Idle());

        }
        else{

            SetSubState(_factory.Walk());

        }
    }




}
