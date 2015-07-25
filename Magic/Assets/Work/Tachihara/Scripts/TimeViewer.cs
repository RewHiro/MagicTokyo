
using UnityEngine;
using UnityEngine.UI;


public class TimeViewer : MonoBehaviour {

  TimeLimitter TIME = null;
  Text canvas_ = null;

  void Start() {
    TIME = FindObjectOfType<TimeLimitter>();
    canvas_ = GetComponent<Text>();
  }

  void Update() {
    canvas_.text = TIME.LimitCount.ToString();
  }
}
