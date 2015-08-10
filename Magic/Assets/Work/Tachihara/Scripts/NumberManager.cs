
using UnityEngine;


public class NumberManager : MonoBehaviour {

  [SerializeField]
  Sprite[] NUMBER = null;
  SpriteRenderer lower_ = null;
  SpriteRenderer upper_ = null;

  TimeLimitter TIME = null;
  int now_ = 0;
  int prev_ = 0;

  void Start() {
    TIME = FindObjectOfType<TimeLimitter>();

    lower_ = GameObject.Find("LowerCount").GetComponent<SpriteRenderer>();
    upper_ = GameObject.Find("UpperCount").GetComponent<SpriteRenderer>();
  }

  void Update() {
    now_ = TIME.LimitCount;
    if (now_ == prev_) return;

    lower_.sprite = NUMBER[now_ % 10];
    upper_.sprite = (now_ / 10) != 0 ? NUMBER[now_ / 10] : null;
    prev_ = now_;
  }
}
