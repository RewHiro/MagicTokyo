
using UnityEngine;


public class PotGaugeController : MonoBehaviour {

  Transform bar_ = null;
  float SCALE_Y = 0.0f;

  TuboInDestroy tubo_ = null;


  void Start() {
    bar_ = GameObject.Find("Bar").transform;
    SCALE_Y = bar_.transform.localScale.y;

    tubo_ = FindObjectOfType<TuboInDestroy>();
  }

  void Update() {
    var ratio = tubo_.GetKudamonCount() * 0.1f;
    var scale = new Vector3(1.0f, SCALE_Y * ratio, 1.0f);
    bar_.localScale = scale;
  }
}
