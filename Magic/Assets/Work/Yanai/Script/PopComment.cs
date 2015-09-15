
using UnityEngine;
using UnityEngine.UI;


public class PopComment : MonoBehaviour {

  float alpha_ = 0.0f;
  float Alpha {
    set {
      alpha_ = value;
      raw_image_.color = new Color(1, 1, 1, alpha_);
    }
        get
        {
            return alpha_;
        }
  }

  RawImage raw_image_ = null;

  [SerializeField]
  GameObject scene_state_;

	void Start () {
    raw_image_ = GetComponent<RawImage>();
    raw_image_.color = new Color(1, 1, 1, alpha_);
  }
  
	void Update () {
    if (scene_state_.GetComponent<SceneState>().isReady()) {
      if (alpha_ < 1.0f) {
                Alpha += Time.deltaTime;
      }
    }
  }
}
