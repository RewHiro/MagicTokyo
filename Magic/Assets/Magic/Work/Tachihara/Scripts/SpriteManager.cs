﻿
using UnityEngine;
using UnityEngine.UI;


public class SpriteManager : MonoBehaviour {

  [SerializeField, TooltipAttribute("スロット演出時間の長さ（単位：秒）")]
  float ROULETTE_TIME = 1.0f;
  int roulette_counter_ = 0;
  int blink_timer_ = 0;

  [SerializeField, Range(1, 5), TooltipAttribute("画像切り替えの速度（1:早い, 5:遅い）")]
  int ROULETTE_SPEED = 2;
  int roulette_reel_ = 0;
  public int MagicSlot { get { return roulette_reel_; } }

  [SerializeField, TooltipAttribute("スロットに使う魔法アイコンの一覧")]
  Sprite[] ICON = null;

  public bool SlotTrigger { get; private set; }

  SpriteRenderer sprite_ = null;


  void Start() {
    SlotTrigger = false;
    sprite_ = gameObject.GetComponent<SpriteRenderer>();
  }

  void Update() {
    /*// Debug Command.
    if (Input.GetKeyDown(KeyCode.A)) {
      Debug.Log("slot trigger on.");
      SlotTrigger = true;
    }

    if (Input.GetKeyDown(KeyCode.S)) {
      Debug.Log("magic action execute.");
      MagicAction();
    }
    */

    if (blink_timer_ > 0) { SlotBlink(); }
    if (!SlotTrigger) { return; }

    ++roulette_counter_;
    roulette_reel_ = (roulette_counter_ / ROULETTE_SPEED) % ICON.Length;

    if (roulette_counter_ > ROULETTE_TIME * 60.0f) {
      roulette_counter_ = 0;
      roulette_reel_ = Random.Range(0, ICON.Length - 1);
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
}
