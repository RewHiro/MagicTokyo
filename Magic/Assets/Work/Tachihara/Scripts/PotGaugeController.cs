
using UnityEngine;


public class PotGaugeController : MonoBehaviour {

  Transform bar_ = null;
  float SCALE_X = 0.0f;
  float SCALE_Y = 0.0f;

  TuboInDestroy tubo_ = null;
  int current_count_ = 0;       // 現在のカウント
  int accumlation_count_ = 0;   // 累積カウント

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
    current_count_ = tubo_.GetKudamonCount() - accumlation_count_;

    if (current_count_ > 10) { current_count_ = 10; }
    var ratio = current_count_ * 0.1f;
    var scale = new Vector3(SCALE_X, SCALE_Y * ratio, 1.0f);
    bar_.localScale = scale;

    var maximum = (current_count_ == 10);

    var set_color = (maximum ? Color.white : ALPHA);
    if (effect_.color != set_color) { effect_.color = set_color; }

    if (!maximum) { return; }
    effect_.transform.Rotate(new Vector3(0, 0, -Time.deltaTime * 60));
  }

  public void GaugeReset() {
    accumlation_count_ += current_count_;
  }
}
