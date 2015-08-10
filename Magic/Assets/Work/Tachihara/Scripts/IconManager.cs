
using UnityEngine;
using UnityEngine.Networking;


public class IconManager : NetworkBehaviour {

  [SerializeField]
  Vector3 OWNER_POS = Vector3.zero;

  [SerializeField]
  Vector3 ENEMY_POS = Vector3.zero;

  [SerializeField, Tooltip("アイコンの回転速度：ピンチになったときの演出")]
  float ANGLE_SPEED = 1.0f;

  float apumon_angle_ = 0.0f;
  float le_mon_angle_ = 0.0f;

  TimeLimitter time_ = null;


  void Start() {
    time_ = FindObjectOfType<TimeLimitter>();

    PlayerSetting player = null;
    foreach (var obj in FindObjectsOfType<PlayerSetting>()) {
      if (!obj.isLocalPlayer) continue;
      player = obj;
    }

    var is_server = player.isServer;
    
    var owner = GameObject.Find(is_server ? "Le-mon" : "Apumon").transform;
    var enemy = GameObject.Find(is_server ? "Apumon" : "Le-mon").transform;
    owner.localPosition = OWNER_POS;
    enemy.localPosition = ENEMY_POS;

    var p1 = GameObject.Find("Player_1").GetComponent<SpriteRenderer>();
    var p2 = GameObject.Find("Player_2").GetComponent<SpriteRenderer>();
    p1.color = is_server ? Color.yellow : Color.red;
    p2.color = is_server ? Color.red : Color.yellow;
  }

  void Update() {
    if (time_.LimitCount > 10) return;
  }
}
