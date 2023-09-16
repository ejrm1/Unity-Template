using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAbilityState : PlayerBaseState
{

    public PlayerAbilityState(PlayerStateMachine currentContext, PlayerStateFactory PlayerStateFactory) : base(currentContext, PlayerStateFactory)
    {
    }

    public override void EnterState()
    {
        if (_ctx.PlayerInfo.IsAbilityActive)
        {
            _ctx.Ability.DeactivateAbility();

        }
        else if (!_ctx.PlayerInfo.IsAbilityActive)
        {
            _ctx.Ability.ActivateAbility();
        }

    }

    public override void UpdateState()
    {
        CheckSwitchState();

    }

    public override void ExitState() { }
    public override void CheckSwitchState()
    {
        if (_ctx.InputManager.CurrentMovementInput != 0)
        {
            SwitchState(_factory.Walk());
        }
        if (_ctx.InputManager.CurrentMovementInput == 0)
        {

            SwitchState(_factory.Idle());
        }
        if (_ctx.InputManager.ShootingInput)
        {
            SwitchState(_factory.Shooting());
        }
    }

    public override void InitializeSubState() { }
}
