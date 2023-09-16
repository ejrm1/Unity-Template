using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public abstract class EventTrigger : MonoBehaviour
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
    
    public UnityEvent OnTriggerActivated;
    public UnityEvent OnTriggerDeactivated;

    private Coroutine timerCoroutine;


    public bool IsPressed { get => _isPressed; set => _isPressed = value; }


    protected abstract bool ShouldActivate(Collider2D collider);
    protected abstract bool ShouldDeactivate(Collider2D collider);

    public virtual void OnTriggerEnter2D(Collider2D collision)
    {
        
        if (ShouldActivate(collision))
        {
            ActivateEvent();
        }
    }

    public virtual void OnTriggerExit2D(Collider2D collision)
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
