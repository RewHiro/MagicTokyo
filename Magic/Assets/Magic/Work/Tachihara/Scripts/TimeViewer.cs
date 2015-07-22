
using UnityEngine;
using UnityEngine.UI;


public class TimeViewer : MonoBehaviour {

  TimeLimitter TIME = null;
  Text canvas_ = null;

  void Start() {
    TIME = FindObjectOfType<TimeLimitter>();
  }

  void Update() {
    canvas_.text = TIME.LimitCount.ToString();
  }
}
