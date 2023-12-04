using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class Platform : MonoBehaviour
{

   
    public List<Transform> points = new List<Transform>();
    public bool circleWaypoints = false;
    [SerializeField] float speed = 10f;
    int _currentWaypointIndex = 0;
    Vector2 startPosition;


    private bool _isMovingToNextWaypoint = false;
    private bool _isMovingToPreviousWaypoint = false;

    public float Speed { get; private set; }

    private void Awake()
    {
        GameObject pointsObj = new GameObject("Initial Waypoint");
        pointsObj.transform.position = transform.position;
        startPosition = pointsObj.transform.position;
        points.Insert(0, pointsObj.transform);
    }


    private void OnDrawGizmosSelected()
    {
        if (points == null || points.Count < 2)
        {
            return;
        }

        Gizmos.color = Color.blue;
        for (int p = 0; p < points.Count; p++)
        {
            Vector2 p1 = points[p].position;
            Vector2 p2 = points[(p + 1) % points.Count].position;

            Gizmos.DrawSphere(p1, 0.1f);
            Gizmos.DrawLine(p1,  p2);
        }
    }


    public void MoveToNextWaypoint()
    {
        if (_currentWaypointIndex < points.Count - 1 || circleWaypoints)
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

        if (_currentWaypointIndex >= points.Count && circleWaypoints)
        {
            _currentWaypointIndex = 0;

        }
        while (Vector3.Distance(transform.position, points[_currentWaypointIndex].position) > 0.001f)
        {
            if (_isMovingToPreviousWaypoint)
            {
                _isMovingToNextWaypoint = false;
                yield break;
            }

            transform.position = Vector3.MoveTowards(transform.position, points[_currentWaypointIndex].position, speed * Time.deltaTime);
            yield return null;
        }

        _isMovingToNextWaypoint = false;
    }

    IEnumerator MoveToPreviousWaypointCoroutine()
    {
        _isMovingToPreviousWaypoint = true;
        _currentWaypointIndex--;

        if (_currentWaypointIndex < 0 && circleWaypoints)
            _currentWaypointIndex = points.Count - 1;

        print(Vector3.Distance(transform.position, points[_currentWaypointIndex].position));
        while (Vector3.Distance(transform.position, points[_currentWaypointIndex].position) > 0.001f)
        {
            if (_isMovingToNextWaypoint)
            {
                _isMovingToPreviousWaypoint = false;
                yield break;
            }

            transform.position = Vector3.MoveTowards(transform.position, points[_currentWaypointIndex].position, speed * Time.deltaTime);
            yield return null;
        }

        _isMovingToPreviousWaypoint = false;
    }






}