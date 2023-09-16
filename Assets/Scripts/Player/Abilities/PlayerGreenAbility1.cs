using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGreenAbility : PlayerBaseAbility
{
    public PlayerGreenAbility(PlayerController playerController) : base(playerController)
    {
    }

    public override void ActivateAbility()
    {
        _player.PlayerInfo.IsAbilityActive = true;

    }

    public override void DeactivateAbility()
    {
        _player.PlayerInfo.IsAbilityActive = false;

    }
}



