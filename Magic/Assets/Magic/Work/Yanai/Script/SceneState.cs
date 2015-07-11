
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

  void Update() {}
}
