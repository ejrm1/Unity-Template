using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJumpState : PlayerBaseState
{
    private bool _jumpFinish = false;

    public PlayerJumpState(PlayerStateMachine currentContext, PlayerStateFactory playerStateFactory)
        : base(currentContext, playerStateFactory)
    {
        _isRootState = true;
        InitializeSubState();
    }

    public override void EnterState()
    {

        if (_ctx.PlayerInfo.JumpCounter < _ctx.PlayerInfo.MaxJumps)
        {
            _ctx.PlayerInfo.JumpCounter++;
           
            _ctx.Physics.Jump();
        }
    }

    public override void UpdateState()
    {
        if (_ctx.Physics.GetVerticalVelocity() < 0.01)
        {
            _jumpFinish = true;
        }

        CheckSwitchState();
    }

    public override void ExitState()
    {
        // Logic for ExitState (currently empty)
    }

    public override void CheckSwitchState()
    {
        if (_jumpFinish)
        {
            Debug.Log("Falling");
            SwitchState(_factory.Fall());
        }
        else if (_ctx.PlayerInfo.JumpCounter < _ctx.PlayerInfo.MaxJumps && _ctx.InputManager.JumpingWasPressedThisFrame)
        {

            SwitchState(_factory.Jump());
        }
    }

    public override void InitializeSubState()
    {
        if (_ctx.InputManager.CurrentMovementInput == 0)
        {
            SetSubState(_factory.Idle());
        }
        else
        {
            SetSubState(_factory.Walk());
        }
    }
}
