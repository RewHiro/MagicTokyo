
using UnityEngine;
using System.Collections;


public class SpriteManager : MonoBehaviour {

  [SerializeField]
  float shuffle_time_ = 1.0f;

  [SerializeField, Range(1, 10)]
  int shuffle_speed_ = 1;

  [SerializeField]
  Sprite[] icon_ = null;
  Sprite current_ = null;

  GameObject player_ = null;


  void Start() {
    //player_ = FindObjectOfType<Player>();
  }

  void Update() {
  }

  public void SetMagic(int magic_type) {
  }
}
