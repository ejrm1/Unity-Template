using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private PlayerInputManager _inputManager;
    private PlayerPhysics _physics;
    private PlayerStateMachine _stateMachine;
    private PlayerCDManager _cdManager;

    private PlayerInfo _playerInfo;

    private PlayerBaseAbility _ability;
    private Camera currentCamera;

    public PlayerBaseAbility Ability { get { return _ability;} set { _ability = value;}}
    public PlayerInfo PlayerInfo {get {return _playerInfo;}}
    public PlayerCDManager CDManager {get {return _cdManager;}}
    public PlayerInputManager InputManager {get {return _inputManager;}}
    public PlayerPhysics Physics {get {return _physics;}}
    

    void Awake() {
        
        _playerInfo = GetComponent<PlayerInfo>();


        switch
                (_playerInfo.CharacterRol)
        {
            case CharacterRol.Rojo:
                _ability = new PlayerRedAbility(this);
                break;
            case CharacterRol.Verde:
                _ability = new PlayerGreenAbility(this);
                break;
            case CharacterRol.Amarillo:
                _ability = new PlayerYellowAbility(this);
                break;
        }



        _cdManager =  GetComponent<PlayerCDManager>();
        _inputManager = GetComponent<PlayerInputManager>();
        _physics = GetComponent <PlayerPhysics>();
        _stateMachine = new PlayerStateMachine(this);
    }

    void Start() {
        _stateMachine.States= new PlayerStateFactory( _stateMachine);        
        _stateMachine.CurrentState= _stateMachine.States.Grounded();
        _stateMachine.CurrentState.EnterState();
    }

    void Update() {
        _stateMachine.CurrentState.UpdateStates();

       


    }

    public void Jump() { 
        _physics.Jump(); 
    }
}
