using UnityEngine;
using System.Collections;

public class PlatformBehavior : MonoBehaviour {

  public Color color;

  private SpriteRenderer spriteRenderer;

  void Start() {
    spriteRenderer = GetComponent<SpriteRenderer>();
  }

  void Update() {
    spriteRenderer.color = color;
  }

  public Color getColor() {
    return color;
  }

  public void setColor(Color color) {
    this.color = color;
    this.color.a = 255;
  }

  public bool matchColor(Color color) {
    return this.color.Equals(color);
  }
}
