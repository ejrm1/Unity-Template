using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PlayerBaseAbility : MonoBehaviour
{
    protected PlayerController _player;

    public PlayerBaseAbility(PlayerController playerController)
    {
        this._player = playerController;
    }

    public abstract void ActivateAbility();

    public abstract void DeactivateAbility();

}
