using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class PassThroughCollision : EventCollision
{
    private PlayerController playerController;

    public PlayerController PlayerController { get => playerController; set => playerController = value; }

    protected override bool ShouldActivate(Collision2D collision)
    {
        if (_isActive == false) return false;

        if (_specificPlayerButton == true)
        {
            var player = collision.gameObject.GetComponent<PlayerController>();
            if (player == null || player.PlayerInfo.CharacterRol != _playerType)
            {
                return false;
            }
        }

        playerController = collision.gameObject.GetComponent<PlayerController>();
        return playerController != null;
    }

    protected override bool ShouldDeactivate(Collision2D collision)
    {
        if (_isActive == false) return false;

        playerController = collision.gameObject.GetComponent<PlayerController>();
        return playerController != null;
    }

    public override void ActivateEvent()
    {
        if (playerController)
        {
            if (_isInfiniteButton == true)
            {
                _isActive = false;
            }

            OnCollisionActivated.Invoke();
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
        OnCollisionDeactivated.Invoke();
    }
}
