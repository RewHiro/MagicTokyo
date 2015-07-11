
using UnityEngine;
using System.Collections;


public class TransformController : MonoBehaviour {

  [SerializeField]
  float scale_ = 0.5f;

  [SerializeField]
  float back_speed_ = 1.0f;

  Vector3 neutral_ = Vector3.zero;
  public Vector3 Neutral { get { return neutral_; } }


  void Start() {
    transform.rotation = Camera.main.transform.rotation;
    transform.localScale = Vector3.one * scale_;

    neutral_ = transform.position;
  }

  void Update() {
    if (Vector3.Equals(transform.position, neutral_)) { return; }

    var dif = (transform.position - Neutral) * back_speed_;
    transform.position = transform.position - dif * Time.deltaTime;
  }
}
