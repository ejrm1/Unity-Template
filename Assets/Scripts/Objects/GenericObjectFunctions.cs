using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenericObjectFunctions : MonoBehaviour
{
    [SerializeField]
    private float _timeToDestroy;
    int _currentWaypointIndex = 0;
    public bool circleWaypoints = false;
    public List<Transform> waypoints = new List<Transform>();
    public float movementSpeed = 1.0f;

    private bool _isMovingToNextWaypoint = false;
    private bool _isMovingToPreviousWaypoint = false;

    private int playerCountOnButton = 0;



    private void Awake()
    {
        GameObject waypointObj = new GameObject("Initial Waypoint");
        waypointObj.transform.position = transform.position;
        waypoints.Insert(0, waypointObj.transform);
    }

    public void SelfDestroy()
    {
        Destroy(gameObject, _timeToDestroy);
    }

    public void MoveToNextWaypoint()
    {
        if (_currentWaypointIndex < waypoints.Count - 1 || circleWaypoints)
        {
            StartCoroutine(MoveToNextWaypointCoroutine());
        }
    }

    public void MoveToPreviousWayPoint()
    {
        if (_currentWaypointIndex > 0 || circleWaypoints)
        {
            StartCoroutine(MoveToPreviousWaypointCoroutine());
        }
    }

    IEnumerator MoveToNextWaypointCoroutine()
    {

        _isMovingToNextWaypoint = true;
        _currentWaypointIndex++;

        if (_currentWaypointIndex >= waypoints.Count && circleWaypoints) {
            _currentWaypointIndex = 0;

        }
        while (Vector3.Distance(transform.position, waypoints[_currentWaypointIndex].position) > 0.001f)
        {
            if (_isMovingToPreviousWaypoint)
            {
                _isMovingToNextWaypoint = false;
                yield break;
            }

            transform.position = Vector3.MoveTowards(transform.position, waypoints[_currentWaypointIndex].position, movementSpeed * Time.deltaTime);
            yield return null;
        }

        _isMovingToNextWaypoint = false;
    }

    IEnumerator MoveToPreviousWaypointCoroutine()
    {
        _isMovingToPreviousWaypoint = true;
        _currentWaypointIndex--;

        if (_currentWaypointIndex < 0 && circleWaypoints)
            _currentWaypointIndex = waypoints.Count - 1;

        print(Vector3.Distance(transform.position, waypoints[_currentWaypointIndex].position));
        while (Vector3.Distance(transform.position, waypoints[_currentWaypointIndex].position) > 0.001f)
        {
            if (_isMovingToNextWaypoint)
            {
                _isMovingToPreviousWaypoint = false;
                yield break;
            }
      
            transform.position = Vector3.MoveTowards(transform.position, waypoints[_currentWaypointIndex].position, movementSpeed * Time.deltaTime);
            yield return null;
        }

        _isMovingToPreviousWaypoint = false;
    }


    public void ActivateCollider()
    {
        Collider2D collider = GetComponent<Collider2D>();

        if (collider != null)
        {
            collider.enabled = true;
        }
        else
        {
            Debug.LogWarning("No Collider2D found on this game object.");
        }
    }
    public void DeactivateCollider()
    {
        Collider2D collider = GetComponent<Collider2D>();

        if (collider != null && playerCountOnButton < 1)
        {
            collider.enabled = false;
        }
        else
        {
            Debug.LogWarning("No Collider2D found on this game object.");
        }
    }


    public void ButtonActivated() 
    {
        playerCountOnButton++;
        MoveToNextWaypoint();
    }

    public void ButtonDeactivated() 
    { 

        playerCountOnButton--;
        if (playerCountOnButton <= 0)
        {
            playerCountOnButton = 0; 
            MoveToPreviousWayPoint();
        }
    }

}
