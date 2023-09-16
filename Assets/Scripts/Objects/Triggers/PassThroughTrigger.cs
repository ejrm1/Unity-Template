using System.Collections;
using System.Collections.Generic;
using UnityEngine;



[RequireComponent(typeof(Collider2D))]
public class PassThroughTrigger : EventTrigger
{  
    private PlayerController playerController;


    public PlayerController PlayerController { get => playerController; set => playerController = value; }
    protected override bool ShouldActivate(Collider2D collision)
    {

        
        if(_isActive == false) return false;
        
        if(_specificPlayerButton == true)
        {
            var player = collision.gameObject.GetComponent<PlayerController>();
            if(player == null || player.PlayerInfo.CharacterRol != _playerType)
            {
                return false;
            }
        }

        playerController = collision.gameObject.GetComponent<PlayerController>();
        return playerController != null;
    }

    protected override bool ShouldDeactivate(Collider2D collision)
    {
        if(_isActive == false) return false;
        
        playerController = collision.gameObject.GetComponent<PlayerController>();
        return playerController != null;
    }

    public override void ActivateEvent()
    {
        
        if (playerController)
        {
            if( _isInfiniteButton == true)
            {
                _isActive = false;
            }

            OnTriggerActivated.Invoke();
            IsPressed = true;
        }
    }

    public override void DeactivateEvent()
    {
        if (playerController)
        {
            StartTimer(_timeToDeactivate);
            IsPressed = false;
        }
    }
    protected override void TimerFinished()
    {
        
        OnTriggerDeactivated.Invoke();     
    }


}
