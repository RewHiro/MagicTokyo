
using UnityEngine;
using System.Collections;
using Leap;

enum TitleState {
  Set,
  DemoPlay,
  Start,
}

public class SceneState : MonoBehaviour {

  TitleState state_;
  Controller controller = new Controller();
  GestureList gestures;

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
    controller.EnableGesture(Gesture.GestureType.TYPE_SWIPE);
  }

  void Start() {}

  void Update() {

    // デモプレイに移行
    if (canShiftDemoPlay()) {
      state_ = TitleState.DemoPlay;
    }
    
    // ゲーム本編に移行
    if (canShiftStart()) {
      state_ = TitleState.Start;
    }
  }

  bool canShiftDemoPlay() {
    if (!isRecognizedHand()) return false;

    Frame frame = controller.Frame();
    gestures = frame.Gestures();
    var finger_count = frame.Fingers.Count;

    for (int i = 0; i < finger_count; ++i) {
      if (gestures[i].Type == Gesture.GestureType.TYPE_SWIPE) {
        return true;
      } 
    }
    return false;
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
