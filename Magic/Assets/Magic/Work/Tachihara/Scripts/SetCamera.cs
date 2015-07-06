
using UnityEngine;


public class SetCamera : MonoBehaviour {

  void Start() {
    var canvas = gameObject.GetComponent<Canvas>();
    canvas.worldCamera = Camera.main;
    transform.rotation = canvas.worldCamera.transform.rotation;
  }
}
