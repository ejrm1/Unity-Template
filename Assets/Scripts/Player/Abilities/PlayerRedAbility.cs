using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRedAbility : PlayerBaseAbility
{
    public PlayerRedAbility(PlayerController playerController) : base(playerController)
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



