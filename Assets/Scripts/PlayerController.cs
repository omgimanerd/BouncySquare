using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {
  
  public float controlSpeed;
  public float bounceVelocity;

  private Rigidbody2D body;

  void Start() {
    body = GetComponent<Rigidbody2D>();
  }

  void FixedUpdate() {
    float horizontal = Input.GetAxis("Horizontal");
    Vector3 velocity = body.velocity;
    velocity.x = horizontal * controlSpeed;
    body.velocity = velocity;
  }

  void OnCollisionStay2D(Collision2D collision) {
    if (collision.gameObject.CompareTag("Platform") && body.velocity.y <= 0) {
      body.velocity = new Vector3(0, bounceVelocity, 0);
    }
  }
}
