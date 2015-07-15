
using UnityEngine;
using System.Collections;


public class GaugeController : MonoBehaviour {

  Vector3 scale_ = Vector3.zero;

  GameObject player_ = null;
  GameObject y_bar_ = null;
  GameObject r_bar_ = null;

  void Start() {
    scale_ = transform.localScale;

    y_bar_ = GameObject.Find("YellowBar");
    r_bar_ = GameObject.Find("RedBar");
  }

  void Update() {
    var p1 = GameObject.Find("Player1");
    var p2 = GameObject.Find("Player2");

    if (p1 != null) { player_ = p1; }
    else if (p2 != null) { player_ = p2; }
    else { return; }

    var fruit = player_.GetComponent<FruitCounter>();
    var all_num = (float)(fruit.FruitNum + fruit.RemoteFruitNum);
    var owner = fruit.FruitNum / all_num;
    var enemy = fruit.RemoteFruitNum / all_num;
    
    
  }
}
