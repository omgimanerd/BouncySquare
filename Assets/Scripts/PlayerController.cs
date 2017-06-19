using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

  /**
   * This should match the order of the colors on the square.
   */
  public Color[] colors;

  public float controlSpeed;
  public float bounceVelocity;

  private Rigidbody2D body;

  void Start() {
    body = GetComponent<Rigidbody2D>();
  }

  void Update() {
    float horizontal = Input.GetAxis("Horizontal");
    Vector3 velocity = body.velocity;
    velocity.x = horizontal * controlSpeed;
    body.velocity = velocity;

    Debug.Log(Input.touchCount);
    // Touch t = Input.GetTouch();
    // Debug.Log(t.position);
  }

  void OnCollisionEnter2D(Collision2D collision) {
    if (collision.gameObject.CompareTag("Platform") && body.velocity.y <= 0) {
      body.velocity = new Vector3(0, bounceVelocity, 0);
    }
  }

  public bool matchColor(Color color) {
    float angle = transform.rotation.z;
    while (angle < 0) {
      angle += 360;
    }
    angle %= 360;
    return colors[Mathf.FloorToInt(angle / 90)].Equals(color);
  }
}
