
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class PlayerPhysics: MonoBehaviour {

  [SerializeField] private float groundCheckDistance = 0.4f;
  [SerializeField] private LayerMask groundLayers;
  private PlayerInfo _playerInfo;
  private int _maxDepth = 5;
  float skinWidth = 0.001f;
  float maxSlopeAngle = 60f;

  public Vector2 movementInput;

  private Rigidbody2D _rb;
  private BoxCollider2D _playerBoxCollider;

  private void Awake() {
    _playerBoxCollider = GetComponent <BoxCollider2D> ();

    _rb = GetComponent < Rigidbody2D > ();
    _playerInfo = GetComponent < PlayerInfo > ();
    _rb.constraints = RigidbodyConstraints2D.FreezeRotation;

  }

  public void Move(float horizontalInput) {
    movementInput = new Vector2(horizontalInput, 0);

  }
  public void Jump() {

    float jumpVelocity = Mathf.Sqrt(2 * _playerInfo.JumpHeight * Mathf.Abs(Physics2D.gravity.y));
    _rb.velocity = new Vector2(_rb.velocity.x, jumpVelocity);

  }

  public bool IsGrounded() {
    RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, groundCheckDistance, groundLayers);
    return hit.collider != null;

  }

  public void ChangeGravityScale(float gravityScale) {
    _rb.gravityScale = gravityScale;
  }

  public void ChangeVerticalMovement(float verticalMovement) {
    _rb.velocity = new Vector2(_rb.velocity.x, verticalMovement);
  }

  public void MovePlayer(float horizontalInput) {
    Move(horizontalInput);
  }

  public void ChangeHorizontalMovement(float horizontalMovement) {
    _rb.velocity = new Vector2(horizontalMovement, _rb.velocity.y);
  }

  public float GetVerticalVelocity() {
    return _rb.velocity.y;
  }

  private Vector2 CollideAndSlide(Vector2 velocity, Vector2 pos, int depth, bool gravityPass) {
    if (depth > _maxDepth) {
      Debug.Log("Max depth reached");
      return Vector2.zero;
    }

    float distance = velocity.magnitude + skinWidth;
    Vector2 boxSize = _playerBoxCollider.bounds.extents;
    Vector2 castDirection = velocity.normalized;
    RaycastHit2D hit;

        hit = Physics2D.BoxCast(pos, _playerBoxCollider.bounds.extents , 0f, velocity.normalized, distance, groundLayers);
        // Debug Visualization
        // Draw the starting box in red
        Debug.DrawLine(pos + new Vector2(-boxSize.x, -boxSize.y), pos + new Vector2(boxSize.x, -boxSize.y), Color.red);
        Debug.DrawLine(pos + new Vector2(boxSize.x, -boxSize.y), pos + new Vector2(boxSize.x, boxSize.y), Color.red);
        Debug.DrawLine(pos + new Vector2(boxSize.x, boxSize.y), pos + new Vector2(-boxSize.x, boxSize.y), Color.red);
        Debug.DrawLine(pos + new Vector2(-boxSize.x, boxSize.y), pos + new Vector2(-boxSize.x, -boxSize.y), Color.red);

        // Draw the end box in blue, indicating the end position of the box if there were no collisions
        Vector2 endPos = pos + castDirection * distance;
        Debug.DrawLine(endPos + new Vector2(-boxSize.x, -boxSize.y), endPos + new Vector2(boxSize.x, -boxSize.y), Color.blue);
        Debug.DrawLine(endPos + new Vector2(boxSize.x, -boxSize.y), endPos + new Vector2(boxSize.x, boxSize.y), Color.blue);
        Debug.DrawLine(endPos + new Vector2(boxSize.x, boxSize.y), endPos + new Vector2(-boxSize.x, boxSize.y), Color.blue);
        Debug.DrawLine(endPos + new Vector2(-boxSize.x, boxSize.y), endPos + new Vector2(-boxSize.x, -boxSize.y), Color.blue);

        if (hit) {
      Vector2 snapToSurface = velocity.normalized * (hit.distance - skinWidth);
      Vector2 leftover = velocity - snapToSurface;
      float angle = Vector2.Angle(Vector2.up, hit.normal);
      if (snapToSurface.magnitude <= skinWidth) {
               
        snapToSurface = Vector2.zero;
      }
      if (angle <= maxSlopeAngle) {

        if (gravityPass) {
          return snapToSurface;
                }
                leftover = new Vector2(hit.normal.y, -hit.normal.x) * leftover.magnitude;

        return CollideAndSlide(leftover, pos + snapToSurface, depth + 1, gravityPass) + snapToSurface;
      }

    }

    return velocity;
  }





  private void FixedUpdate() {

    float horizontalInput = movementInput.x;

    Bounds bounds = _playerBoxCollider.bounds;
    bounds.Expand(skinWidth * -2);

    Vector2 gravity = Physics2D.gravity;

    Vector2 moveAmount = new Vector2(horizontalInput * _playerInfo.MoveSpeed, _rb.velocity.y)*Time.fixedDeltaTime;

    // Adjust the velocity with the collide and slide logic
    moveAmount = CollideAndSlide(moveAmount, transform.position, 0, false);

    // Set the rigidbody's velocity to the result of the CollideAndSlide
    _rb.gameObject.transform.position = _rb.gameObject.transform.position + (Vector3) moveAmount;

    movementInput = Vector2.zero;

  }

}