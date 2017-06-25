using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

  /**
   * This should match the order of the colors on the square.
   */

  public float controlSpeed;
  public float bounceVelocity;
  public float gravity;
  public int rotationRate;

  private Color[] colors;
  private Rigidbody2D body;
  private int rotationChange;

  void Start() {
    colors = GameManager.GetInstance().GetColors();
    body = GetComponent<Rigidbody2D>();
    rotationChange = 0;
  }

  void Update() {
    float horizontal = Input.GetAxis("Horizontal");
    Vector3 velocity = body.velocity;
    velocity.x = horizontal * controlSpeed;
    velocity.y -= gravity;
    body.velocity = velocity;
   
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

  void OnTriggerEnter2D(Collider2D collider) {
    Debug.Log("collided with " + collider.tag + " " + body.velocity.y);
    if (collider.CompareTag("Platform") && body.velocity.y <= 0) {
      body.velocity = new Vector3(0, bounceVelocity, 0);
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
