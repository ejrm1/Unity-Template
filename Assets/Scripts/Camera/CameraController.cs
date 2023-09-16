using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform player1;
    public Transform player2;
    public float minZoom = 5f;
    public float maxZoom = 15f;
    public float zoomLimiter = 1.5f;

    private Camera cam;

    private void Start()
    {
        cam = GetComponentInChildren<Camera>();
    }

    private void Update()
    {
        Move();
        Zoom();
    }

    void Move()
    {
        Vector3 midPoint = (player1.position + player2.position) / 2f;
        transform.position = new Vector3(midPoint.x, midPoint.y, transform.position.z);
    }

    void Zoom()
    {
        float newZoom = Mathf.Lerp(minZoom, maxZoom, GetDistance() / zoomLimiter);
        cam.orthographicSize = Mathf.Lerp(cam.orthographicSize, newZoom, Time.deltaTime);
    }

    float GetDistance()
    {
        return Vector2.Distance(player1.position, player2.position);
    }
}
