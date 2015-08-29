
using UnityEngine;


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

    tubo_ = FindObjectOfType<TuboInDestroy>();
    effect_ = GameObject.Find("PotIconEffect").GetComponent<SpriteRenderer>();
    effect_.color = ALPHA;
  }

  void Update() {
    if (IsCountMax()) {
      effect_.transform.Rotate(new Vector3(0, 0, -Time.deltaTime * 60));
    }

    // 計算前の値を取得、ナベの中身が増えてなければ処理をスキップ
    {
      var prev = current_count_;
      CountUp();
      if (prev == current_count_) { return; }
    }
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
  }

  bool IsCountMax() { return current_count_ == 10; }
}
