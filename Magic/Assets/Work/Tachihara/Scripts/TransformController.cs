
using UnityEngine;


public class TransformController : MonoBehaviour {

  [SerializeField]
  float SCALE = 0.5f;

  [SerializeField]
  float BACK_SPEED = 1.0f;

  Vector3 NEUTRAL = Vector3.zero;
  public Vector3 Neutral { get { return NEUTRAL; } }


  void Start() {
    transform.rotation = Camera.main.transform.rotation;
    transform.localScale = Vector3.one * SCALE;

    NEUTRAL = transform.position + Vector3.up * 1.2f;
  }

  void FixedUpdate() {
    if (Vector3.Equals(transform.position, NEUTRAL)) { return; }

    if (transform.position.y < Neutral.y) { FixHeight(); }
    var dif = (transform.position - Neutral) * BACK_SPEED;
    transform.position = transform.position - dif * Time.deltaTime;
  }

  void FixHeight() {
    var v = new Vector3(0, Neutral.y - transform.position.y, 0);
    transform.position += v;
  }
}
