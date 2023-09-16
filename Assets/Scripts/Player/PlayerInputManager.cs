using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(PlayerInput))]
public class PlayerInputManager : MonoBehaviour
{
    private PlayerInput _playerInput;

    private InputAction _jumpAction;
    private InputAction _movement;
    private InputAction _shootAction;
    private InputAction _abilityAction;

    private Vector2 _currentMovementInput;
    private bool _jumpingInput;
    private bool _jumpingWasPressed;
    private bool _shootingInput;
    private bool _shootingWasPressed;
    private bool _abilityInput;
    private bool _abilityWasPressed;
    private Animator _animator;

    public bool AbilityInput { get { return _abilityInput; } }
    public bool AbilityWasPressedThisFrame { get { return _abilityWasPressed; } }
    public bool JumpingInput { get { return _jumpingInput; } }
    public bool JumpingWasPressedThisFrame { get { return _jumpingWasPressed; } }
    public float CurrentMovementInput { get { return _currentMovementInput.x; } }
    public bool ShootingInput { get { return _shootingInput; } }
    public bool ShootingWasPressedThisFrame { get { return _shootingWasPressed; } }

    private void Awake(){
        _playerInput = GetComponent<PlayerInput>();

        _jumpAction = _playerInput.actions["Jump"];    
        _movement = _playerInput.actions["Move"]; 
        _shootAction = _playerInput.actions["Shoot"];
        _abilityAction = _playerInput.actions["Ability"];
        _animator = GetComponent<Animator>();
    }

    private void OnEnable() {
        _movement.performed += Move;
        
        _movement.canceled += context => {
            

            _currentMovementInput = Vector2.zero;
        };

        _abilityAction.performed += Ability;
        _abilityAction.canceled += context => {
          

            _abilityWasPressed = false;
        };

        _shootAction.performed += Shoot;
        _shootAction.canceled += context => {
            

            _shootingInput = false;
        };

        _jumpAction.performed += Jump;

        _jumpAction.canceled += context => {
            

            _jumpingWasPressed = false;
        };
    }

    private void OnDisable() {
        _movement.performed -= Move;
        _movement.canceled -= context => {
       

            _currentMovementInput = Vector2.zero;
        };

        _shootAction.performed -= Shoot;
        _shootAction.canceled -= context => {
          

            _shootingInput = false;
        };

        _jumpAction.performed -= Jump;
        _jumpAction.canceled -= context => {
            

            _jumpingWasPressed = false;
        };

        _abilityAction.performed -= Ability;
        _abilityAction.canceled -= context => {
           

            _abilityWasPressed = false;
        };
    }

    private void Update()
    {
        

        //_jumpingWasPressed = false;
        _shootingWasPressed = false;
        _abilityWasPressed = false;

        Vector2 moveInput = _movement.ReadValue<Vector2>();


        bool jumpInput = _jumpAction.ReadValue<float>() > 0.5f;


        bool shootAction = _shootAction.ReadValue<float>() > 0.5f;
 
    }

    private void Jump(InputAction.CallbackContext context)
    {
       

        _jumpingInput = context.ReadValueAsButton();
        if (_jumpingInput) {
            _jumpingWasPressed = true;
        }
    }

    private void Move(InputAction.CallbackContext context)
    {
       
        
        _currentMovementInput = context.ReadValue<Vector2>();
    }

    private void Shoot(InputAction.CallbackContext context)
    {
       

        _shootingInput = context.ReadValueAsButton();
        if (_shootingInput) {
            _shootingWasPressed = true;
        }
    }

    private void Ability(InputAction.CallbackContext context)
    {
      

        _abilityInput = context.ReadValueAsButton();
        if (_abilityInput) {
            _abilityWasPressed = true;
        }
    }
}
