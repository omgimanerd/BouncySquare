using UnityEngine;
using System.Collections;

public class PlatformController : MonoBehaviour {

  [SerializeField]
  private Color color_;

  private SpriteRenderer spriteRenderer;

  void Start() {
    spriteRenderer = GetComponent<SpriteRenderer>();
  }

  void Update() {
    spriteRenderer.color = color_;
  }

  public Color color {
    get {
      return color_;
    }
    set {
      color_ = value;
      color_.a = 1;
    }
  }

  public bool MatchColor(Color color) {
    return color_.Equals(color);
  }
}
