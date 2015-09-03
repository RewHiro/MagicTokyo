
using UnityEngine;
using System.Collections.Generic;


public class PotGaugeController : MonoBehaviour {

  Transform bar_ = null;
  float SCALE_X = 0.0f;
  float SCALE_Y = 0.0f;

  TuboInDestroy tubo_ = null;
  int current_count_ = 0;   // 現在のカウント
  int last_count_ = 0;      // 累積カウント

  SpriteRenderer effect_ = null;
  Color ALPHA = new Color(1, 1, 1, 0);


  void Start() {
    bar_ = GameObject.Find("Bar").transform;
    SCALE_X = bar_.transform.localScale.x;
    SCALE_Y = bar_.transform.localScale.y;

    bar_.localScale = Vector3.forward;

    tubo_ = FindObjectOfType<TuboInDestroy>();
    var pot_icon = GameObject.Find("PotIconEffect");
    effect_ = pot_icon.GetComponent<SpriteRenderer>();
    effect_.color = ALPHA;
  }

  void Update() {
    if (IsCountMax()) {
      var rotate_speed = -Time.deltaTime * 60.0f;
      effect_.transform.Rotate(Vector3.forward * rotate_speed);
    }

    if (IsNotCountChange()) { return; }
    if (current_count_ > 10) { current_count_ = 10; }

    var ratio = current_count_ * 0.1f;
    var scale = new Vector3(SCALE_X, SCALE_Y * ratio, 1.0f);
    bar_.localScale = scale;

    if (IsCountMax() && effect_.color != Color.white) {
      effect_.color = Color.white;
    }
  }

  public void GaugeReset() {
    // 正しく初期化できるようにカウントを再計算する
    CountUp();
    last_count_ += current_count_;

    // 色を戻す
    effect_.color = ALPHA;
  }

  void CountUp() {
    current_count_ = tubo_.GetKudamonCount() - last_count_;

    // log output
    var log_list = new List<string>();
    log_list.Add(string.Format("current = {0}", current_count_));
    log_list.Add(string.Format("pot     = {0}", tubo_.GetKudamonCount()));
    log_list.Add(string.Format("last    = {0}", last_count_));
    foreach (var log in log_list) { Debug.Log(log); }
  }

  bool IsCountMax() {
    return current_count_ >= 10;
  }

  // カウントが直前のフレームから変化してなければ true を返す
  bool IsNotCountChange() {
    var prev = current_count_;
    CountUp();
    return prev == current_count_;
  }
}
