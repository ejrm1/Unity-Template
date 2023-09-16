using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateMachine 
{
    private PlayerController playerController;

    public PlayerStateMachine(PlayerController playerController)
    {
        this.playerController = playerController;
    }
    PlayerBaseState currentState;
    PlayerStateFactory states;
    public PlayerBaseState CurrentState{get{ return currentState;}  set { currentState =value;}}
    public PlayerStateFactory States{get{ return states;}  set { states =value;}}

    public PlayerPhysics Physics
    {
        get
        {
            return playerController.Physics;
        }
    }
    public PlayerInputManager InputManager
    {
        get
        {
            return playerController.InputManager;
        }
    }

    public PlayerCDManager CDManager
    {
        get
        {
            return playerController.CDManager;
        }
    }
    public PlayerInfo PlayerInfo
    {
        get
        {
            return playerController.PlayerInfo;
        }
    }   
    public PlayerBaseAbility Ability
    {
        get
        {
            return playerController.Ability;
        }
    }
}
