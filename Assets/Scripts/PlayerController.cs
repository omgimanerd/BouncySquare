using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

  /**
   * This should match the order of the colors on the square.
   */

  public float controlSpeed;
  public float bounceVelocity;
  public float gravity;
  public float sidePadding;
  public int rotationRate;

  private float minX, maxX;

  private Color[] colors;
  private Rigidbody2D body;
  private int rotationChange;
  private bool lost;

  void Start() {
    GameManager manager = GameManager.GetInstance();
    minX = -manager.limits.x + sidePadding;
    maxX = manager.limits.x - sidePadding;
    colors = manager.colors;
    body = GetComponent<Rigidbody2D>();
    rotationChange = 0;
    lost = false;
  }

  void Update() {
    float horizontal = Input.GetAxis("Horizontal");
    Vector3 velocity = body.velocity;
    velocity.x = horizontal * controlSpeed;
    velocity.y -= gravity;
    body.velocity = velocity;
    ProcessUserInput();
    BoundUserPosition();
  }

  public void ProcessUserInput() {
    // TODO: change to tapping on screen region
    if (Input.GetMouseButtonDown(0)) {
      rotationChange += 90;
    } else if (Input.GetMouseButtonDown(1)) {
      rotationChange -= 90;
    }
    if (rotationChange != 0 && Mathf.Abs(rotationChange) <= rotationRate) {
      transform.Rotate(0, 0, rotationChange, Space.Self);
      rotationChange = 0;
    } else if (rotationChange > 0) {
      transform.Rotate(0, 0, rotationRate, Space.Self);
      rotationChange -= rotationRate;
    } else if (rotationChange < 0) {
      transform.Rotate(0, 0, -rotationRate, Space.Self);
      rotationChange += rotationRate;
    }
  }

  public void BoundUserPosition() {
    float x = Mathf.Clamp(transform.position.x, minX, maxX);
    Vector3 position = transform.position;
    position.x = x;
    transform.position = position;
  }

  void OnTriggerEnter2D(Collider2D collider) {
    if (collider.CompareTag("Platform") && body.velocity.y <= 0) {
      if (lost) {
        return;
      }
      PlatformController behavior =
        collider.gameObject.GetComponent<PlatformController>();
      if (MatchColor(behavior.color)) {
        body.velocity = new Vector3(0, bounceVelocity, 0);
      } else {
        body.velocity = new Vector3(0, bounceVelocity / 5, 0);
        lost = true;
      }
    }
  }

  public bool MatchColor(Color color) {
    float angle = transform.eulerAngles.z;
    while (angle < 0) {
      angle += 360;
    }
    return colors[Mathf.RoundToInt((angle % 360) / 90)].Equals(color);
  }
}
