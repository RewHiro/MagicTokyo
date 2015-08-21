
using UnityEngine;
using Leap;

enum TitleState {
  Set,
  Start,
}

public class SceneState : MonoBehaviour {

  TitleState state_;
  Controller controller = new Controller();

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
    // ゲーム本編に移行
    if (canShiftStart()) {
            foreach (var player in FindObjectsOfType<LobbyPlayer>())
            {
                if (!player.isLocalPlayer) continue;
                player.ChangeReady();
            }
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
    var hand = FindObjectOfType<RigidHand>();

    if (hand == null) return false;

    return true;
  }

  public bool isStart() {
    return state_ == TitleState.Start;
  }
}