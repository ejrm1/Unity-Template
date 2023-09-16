using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerYellowAbility : PlayerBaseAbility
{
    private float SpeedMultiplier = 2f;

    public PlayerYellowAbility(PlayerController playerController) : base(playerController)
    {
    }

    public override void ActivateAbility()
    {
        _player.PlayerInfo.IsAbilityActive = true;
        _player.PlayerInfo.MaxJumps = 2; 
       
        _player.PlayerInfo.MoveSpeed *= SpeedMultiplier;
    }

    public override void DeactivateAbility()
    {
        _player.PlayerInfo.IsAbilityActive = false;
        _player.PlayerInfo.MaxJumps = 1;
       
        _player.PlayerInfo.MoveSpeed /= SpeedMultiplier;
    }
}
