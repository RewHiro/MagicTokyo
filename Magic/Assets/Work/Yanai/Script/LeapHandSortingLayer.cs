
using UnityEngine;


public class LeapHandSortingLayer : MonoBehaviour {

  void Awake() {}

  void Start() {
    var hand = GetComponent<SkinnedMeshRenderer>();
    hand.sortingLayerName = "LeapHand";
    hand.sortingOrder = 1;
  }

  void Update() {}
}
