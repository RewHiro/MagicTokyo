
using UnityEngine;


public class FruitsGaugeController : MonoBehaviour {

  PlayerSetting player_ = null;
  FruitCounter fruit_ = null;

  GameObject l_bar_ = null;
  GameObject r_bar_ = null;

  float owner_ = 0.0f;
  float enemy_ = 0.0f;


  void Start() {
    l_bar_ = GameObject.Find("Left");
    r_bar_ = GameObject.Find("Right");

    foreach (var player in FindObjectsOfType<PlayerSetting>()) {
            if (!MyNetworkLobbyManager.s_singleton.IsTutorial)
            {
                if (!player.isLocalPlayer) continue;
            }
            player_ = player;
    }

    var l_render = l_bar_.GetComponent<SpriteRenderer>();
    var r_render = r_bar_.GetComponent<SpriteRenderer>();

    l_render.color = player_.isServer ? Color.yellow : Color.red;
    r_render.color = player_.isServer ? Color.red : Color.yellow;
  }

  void Update() {
    if (!IsRefFruitCounter()) { return; }

    owner_ = CalculateGaugeRatio(fruit_.RemoteFruitNum, fruit_.FruitNum);
    enemy_ = CalculateGaugeRatio(fruit_.FruitNum, fruit_.RemoteFruitNum);

    FixGaugeLength(l_bar_.transform, owner_);
    FixGaugeLength(r_bar_.transform, enemy_);
  }

  bool IsRefFruitCounter() {
    if (fruit_ == null) { fruit_ = player_.GetComponent<FruitCounter>(); }
    return fruit_ != null;
  }

  void FixGaugeLength(Transform gauge, float scaleRatio) {
    if (scaleRatio > 2.0f) { scaleRatio = 2.0f; }
    var gaugeScale = new Vector3(3.5f * scaleRatio, 1.0f, 1.0f);
    gauge.localScale = gaugeScale;
  }

  float CalculateGaugeRatio(float ownerNum, float enemyNum) {
    return (ownerNum != 0.0f && enemyNum != 0.0f) ? ownerNum / enemyNum : 0.0f;
  }
}
