using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public abstract class EventCollision : MonoBehaviour
{
    [SerializeField]
    protected bool _isActive;
    [SerializeField]
    protected bool _isPressed;
    [SerializeField]
    protected bool _isInfiniteButton;
    [SerializeField]
    protected float _timeToDeactivate;
    [SerializeField]
    protected bool _specificPlayerButton;
    [SerializeField]
    protected CharacterRol _playerType;

    public UnityEvent OnCollisionActivated;
    public UnityEvent OnCollisionDeactivated;

    private Coroutine timerCoroutine;

    public bool IsPressed { get => _isPressed; set => _isPressed = value; }

    protected abstract bool ShouldActivate(Collision2D collision);
    protected abstract bool ShouldDeactivate(Collision2D collision);

    public virtual void OnCollisionEnter2D(Collision2D collision)
    {
        if (ShouldActivate(collision))
        {
            ActivateEvent();
        }
    }

    public virtual void OnCollisionExit2D(Collision2D collision)
    {
        if (ShouldDeactivate(collision))
        {
            DeactivateEvent();
        }
    }

    public abstract void ActivateEvent();
    public abstract void DeactivateEvent();

    protected void StartTimer(float time)
    {
        if (timerCoroutine != null)
        {
            StopCoroutine(timerCoroutine);
        }
        timerCoroutine = StartCoroutine(TimerCoroutine(time));
    }

    private IEnumerator TimerCoroutine(float time)
    {
        float elapsedTime = 0;
        while (elapsedTime < time)
        {
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        TimerFinished();
    }

    protected abstract void TimerFinished();
}
