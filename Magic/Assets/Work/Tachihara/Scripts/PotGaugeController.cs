
using UnityEngine;


public class PotGaugeController : MonoBehaviour {

  Transform bar_ = null;
  float SCALE_Y = 0.0f;

  TuboInDestroy tubo_ = null;
  int current_count_ = 0;       // 現在のカウント
  int accumlation_count_ = 0;   // 累積カウント

  void Start() {
    bar_ = GameObject.Find("Bar").transform;
    SCALE_Y = bar_.transform.localScale.y;

    tubo_ = FindObjectOfType<TuboInDestroy>();
  }

  void Update() {
    current_count_ = tubo_.GetKudamonCount() - accumlation_count_;
    var ratio = current_count_ * 0.1f;
    var scale = new Vector3(1.0f, SCALE_Y * ratio, 1.0f);
    bar_.localScale = scale;
  }

  public void GaugeReset() {
    accumlation_count_ += current_count_;
  }
}
