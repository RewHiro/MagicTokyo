
using UnityEngine;
using System.Collections;


public class TimeViewer : MonoBehaviour {

  TimeLimitter TIME = null;
  string time_str_ = null;

  void Start() {
    TIME = FindObjectOfType<TimeLimitter>();
  }

  void Update() {
    time_str_ = TIME.LimitCount.ToString();
  }
}
