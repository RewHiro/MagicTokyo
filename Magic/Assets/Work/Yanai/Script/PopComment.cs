
using UnityEngine;
using UnityEngine.UI;


public class PopComment : MonoBehaviour {

  float alpha_ = 0.0f;
  public float Alpha {
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
        foreach (var player in FindObjectsOfType<LobbyPlayer>())
        {
            if (!player.isLocalPlayer) continue;
            if (!player.IsReady) return;
            if (alpha_ > 1.0f) return;
            Alpha += Time.deltaTime;
        }
  }
}
