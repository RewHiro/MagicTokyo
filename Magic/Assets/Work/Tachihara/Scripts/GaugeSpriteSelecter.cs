
using UnityEngine;


public class GaugeSpriteSelecter : MonoBehaviour {

  [SerializeField]
  Sprite red_ = null;

  [SerializeField]
  Sprite yellow_ = null;


  public void Setup(bool is_server) {
    var renderer = gameObject.GetComponent<SpriteRenderer>();
    renderer.sprite = is_server ? yellow_ : red_;
  }
}
