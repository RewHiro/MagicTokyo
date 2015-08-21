
using UnityEngine;


public class IconManager : MonoBehaviour {

  [SerializeField]
  Vector3 OWNER_POS = Vector3.zero;

  [SerializeField]
  Vector3 ENEMY_POS = Vector3.zero;

  TimeLimitter time_ = null;


  void Start() {
    time_ = FindObjectOfType<TimeLimitter>();

    PlayerSetting player = null;
    foreach (var obj in FindObjectsOfType<PlayerSetting>()) {
      if (!obj.isLocalPlayer) continue;
      player = obj;
    }

    var is_server = player.isServer;
    if (player == null) is_server = true;   // Debug

    var owner = GameObject.Find(is_server ? "Le-mon" : "Apumon").transform;
    var enemy = GameObject.Find(is_server ? "Apumon" : "Le-mon").transform;
    owner.localPosition = OWNER_POS;
    enemy.localPosition = ENEMY_POS;

    var p1 = GameObject.Find("Player_1");
    var p2 = GameObject.Find("Player_2");
    p1.GetComponent<GaugeSpriteSelecter>().Setup(is_server);
    p2.GetComponent<GaugeSpriteSelecter>().Setup(!is_server);
  }

  void Update() {
    if (time_.LimitCount > 10) return;

    var own_lose = FindObjectOfType<FruitsGaugeController>().IsOwnerLowRatio();
    var transform = GameObject.Find(own_lose ? "Le-mon" : "Apumon").transform;
    transform.Rotate(new Vector3(0, 0, 2.0f * (60 - time_.LimitCount)));
  }
}
