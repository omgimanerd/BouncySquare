using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

  private static GameManager instance;

  public Color[] colors;
  /*
   * The absolute value coordinates of any visible entity will never
   * be greater than the respective axes on the limits. In essence,
   * the x param in limits is the halfwidth of the world, and the y
   * param in limits is the halfheight of the world.
   */
  [HideInInspector]
  public Vector3 limits;

  void Awake() {
    if (instance == null) {
      instance = this;
      Camera camera = Camera.main;
      for (int i = 0; i < colors.Length; ++i) {
        colors[i].a = 1;
      }
      limits = camera.ScreenToWorldPoint(
        new Vector3(Screen.width, Screen.height, 0));
    }
  }

  public static GameManager GetInstance() {
    return instance;
  }

  public void RestartGame() {
    StartCoroutine(RestartGameCoroutine());
  }

  private IEnumerator RestartGameCoroutine() {
    yield return new WaitForSeconds(2f);
    SceneManager.LoadScene("Gameplay");
  }
}
