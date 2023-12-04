using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public enum CharacterRol
{
    Rojo,
    Verde,
    Amarillo,
}

public class PlayerInfo : MonoBehaviour
{

    public CharacterRol _characterRol;
    private bool _isAlive = true;
    private byte _LastDirection;
    private bool _isAbilityActive;
    private int _maxJumps = 1;
    private int _jumpCounter = 0;
    [SerializeField] private float moveAcceleration = 10f;
    [SerializeField] private float _jumpHeight = 1.5f;
    [SerializeField] private float _moveSpeed = 5f;
  
    private Transform _spawnpoint;

    private void Awake()
    {
        GameObject waypointObj = new GameObject("Initial Spawn");
        waypointObj.transform.position = transform.position;
        _spawnpoint = waypointObj.transform;

    }

    public float MoveAcceleration { get => moveAcceleration; set => moveAcceleration = value; }
    public float MoveSpeed { get => _moveSpeed; set => _moveSpeed = value; }
    public float JumpHeight { get => _jumpHeight; set => _jumpHeight = value; }
    public Transform Spawnpoint { get => _spawnpoint; set => _spawnpoint = value; }
    public int MaxJumps { get => _maxJumps; set => _maxJumps = value; }
    public int JumpCounter { get => _jumpCounter; set => _jumpCounter = value; }
    public byte LastDirection { get => _LastDirection; set => _LastDirection = value; }
    public CharacterRol CharacterRol { get => _characterRol; set => _characterRol = value; }
    public bool IsAbilityActive { get => _isAbilityActive; set => _isAbilityActive = value; }


    public void Respawn()
    {  

        this.transform.position = _spawnpoint.position;
        _isAlive = true;
    }

}
