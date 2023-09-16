using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(PassThroughTrigger))]
public class Spring : MonoBehaviour
{

    public float springForce = 20f;
    public float springCooldown = 0.5f;
    private float _springCooldownTimer = 0f;
    private bool _isSpringing = false;
    private PassThroughTrigger _passThroughTrigger;


    private void Awake()
    {
        _passThroughTrigger = GetComponent<PassThroughTrigger>();
    }

    public void SpringPlayer()
    {
        if (_isSpringing == false)
        {
            _isSpringing = true;
            _passThroughTrigger.PlayerController.Physics.ChangeVerticalMovement(springForce);
            _springCooldownTimer = springCooldown;
            StartCoroutine(ResetSpring());
           
        }
    }

    private IEnumerator ResetSpring()
    {
        yield return new WaitForSeconds(springCooldown);
        _isSpringing = false;
    }


}
