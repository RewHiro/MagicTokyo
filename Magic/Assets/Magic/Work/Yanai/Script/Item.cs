
using UnityEngine;
using System.Collections;


public class Item : MonoBehaviour {

  enum State {
    Slide,
    Rotate,
    Pop,
  }

  State state_;

  void Awake() {
    state_ = State.Slide;
  }

  void Start() {}

  public void Hide() {
    GameObject.Destroy(gameObject);
  }

  void Update() {
    switch (state_) {
      case State.Slide:
        Slide();
        break;
      case State.Rotate:
        Rotate();
        break;
      case State.Pop:
        Pop();
        break;
    }
  }

  void Slide() {
    gameObject.transform.Translate(0.1f, 0.0f, 0.0f);
  }

  void Rotate() {
    
  }

  void Pop() {
    
  }
}
