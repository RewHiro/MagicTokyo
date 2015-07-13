
using UnityEngine;
using System.Collections;


enum TitleState {
  Set,
  Ready,
  Start,
}

public class SceneState : MonoBehaviour {

  TitleState state_;

  void Awake() {
    state_ = TitleState.Set;
  }

  void Start() {}

  void Update() {
    if (!Input.GetKeyDown(KeyCode.Space)) return;
    state_ = TitleState.Start;
  }

  public bool isStart() {
    return state_ == TitleState.Start ? true : false;
  }
}
