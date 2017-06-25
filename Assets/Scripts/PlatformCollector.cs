using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformCollector : MonoBehaviour {
  
  public Rigidbody2D platformPrefab;

  private Color[] platformColors;
  private float platformDistance;
  private float minPlatformX, maxPlatformX;

  void Start() {
    Camera camera = Camera.main;
    Vector3 limits = camera.ScreenToWorldPoint(
      new Vector3(Screen.width, Screen.height * 2, 0));
    platformColors = GameManager.GetInstance().GetColors();
    platformDistance = limits.y / 4;
    minPlatformX = -limits.x + 1;
    maxPlatformX = limits.x - 1;
  }

  void OnTriggerEnter2D(Collider2D collider) {
    if (collider.CompareTag("Platform")) {
      SpawnNewPlatform();
      Destroy(collider.gameObject);
    }
  }

  void SpawnNewPlatform() {
    GameObject[] platforms = GameObject.FindGameObjectsWithTag("Platform");
    float highestY = 0;
    for (int i = 0; i < platforms.Length; ++i) {
      highestY = Mathf.Max(highestY, platforms[i].transform.position.y);
    }
    Vector3 newPlatformVector = new Vector3(
      Random.Range(minPlatformX, maxPlatformX), highestY + platformDistance, 0);
    Rigidbody2D newPlatform = (Rigidbody2D) Instantiate(
      platformPrefab, newPlatformVector, Quaternion.identity);
    Color newColor = platformColors[Random.Range(0, platformColors.Length)];
    newPlatform.GetComponent<PlatformBehavior>().setColor(newColor);
  }
}
