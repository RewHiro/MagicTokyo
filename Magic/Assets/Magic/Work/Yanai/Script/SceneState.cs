
using UnityEngine;
using System.Collections;
using Leap;

enum TitleState {
  Set,
  DemoPlay,
  Start,
}

public class SceneState : MonoBehaviour {

  private TitleState state_;

  [SerializeField]
  int VALID_MAX = 60;
  public int ValidMax {
    get { return VALID_MAX; }
  }

  [SerializeField]
  int valid_count_ = 0;
  public int ValidCount {
    get { return valid_count_; }
    private set { valid_count_ = value; }
  }

  void Awake() {
    state_ = TitleState.Set;
  }

  void Start() {}

  void Update() {

    // デモプレイに移行
    if (canShiftStart()) {
      state_ = TitleState.Start;
    }
  }

  bool canShiftStart() {
    if (!isRecognizedHand()) return false;
    if (ValidCount < ValidMax) {
      ValidCount++;
      return false;
    }
    else {
      return true;
    }
  }

  bool isRecognizedHand() {
    var hand = GetComponent<RigidHand>();

    if (hand == null) return false;

    return true;
  }

  public bool isStart() {
    return state_ == TitleState.Start;
  }

  public bool isDemoPlay() {
    return state_ == TitleState.DemoPlay;
  }
}
