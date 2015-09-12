
using UnityEngine;
using UnityEngine.UI;


public class ItemSpriteManager : MonoBehaviour {

  [SerializeField, Tooltip("スロット演出時間の長さ（単位：秒）")]
  float ROULETTE_TIME = 1.0f;
  int roulette_counter_ = 0;
  int blink_timer_ = 0;

  [SerializeField, Range(1, 5), Tooltip("画像切り替えの速度（1:早い, 5:遅い）")]
  int ROULETTE_SPEED = 2;
  int roulette_reel_ = 0;

  [SerializeField, Tooltip("スロットに使う魔法アイコンの一覧")]
  Sprite[] ICON = null;
  public int IconSize { get { return ICON.Length; } }

  public bool SlotTrigger { get; private set; }

  SpriteRenderer sprite_ = null;


  void Start() {
    SlotTrigger = false;
    sprite_ = gameObject.GetComponent<SpriteRenderer>();
  }

  void Update() {
    // Debug Command
    if (Input.GetKeyDown(KeyCode.K)) {
      SlotTrigger = true;
    }
    if (Input.GetKeyDown(KeyCode.J)) {
      MagicAction();
    }

    if (IsSlotBlink()) { SlotBlink(); }
    if (!SlotTrigger) { return; }

    ++roulette_counter_;
    roulette_reel_ = (roulette_counter_ / ROULETTE_SPEED) % ICON.Length;

    if (roulette_counter_ > ROULETTE_TIME * 60.0f) {
      roulette_counter_ = 0;

      var magic = FindObjectOfType<PlayerMagicManager>();
      roulette_reel_ = magic.MagicType;

      // Debug Output
      Debug.Log("magic type = " + magic.MagicType);
      //roulette_reel_ = Random.Range(0, ICON.Length - 1);

      blink_timer_ = 60;
      SlotTrigger = false;
    }

    sprite_.sprite = ICON[roulette_reel_];
  }

  void SlotBlink() {
    var alpha = 1 - (blink_timer_ / 2) % 2;
    --blink_timer_;
    sprite_.color = new Color(1.0f, 1.0f, 1.0f, alpha);
  }

  public void SlotTriggerEnter() {
    SlotTrigger = true;
  }

  public void MagicAction() {
    sprite_.sprite = null;
  }

  // スロットの回転、点滅中か
  public bool IsSlotBlink() {
    return blink_timer_ > 0;
  }
}
