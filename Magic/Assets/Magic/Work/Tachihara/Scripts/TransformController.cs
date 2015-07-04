
using UnityEngine;
using System.Collections;


public class TransformController : MonoBehaviour {

  [SerializeField]
  float scale_ = 0.5f;

  Vector3 neutral_ = Vector3.zero;

  void Start() {
    transform.rotation = Camera.main.transform.rotation;
    transform.localScale = Vector3.one * scale_;

    neutral_ = transform.position;
  }

  void Update() {
    // TODO: 手で動かしたら元に戻す処理
  }
}
