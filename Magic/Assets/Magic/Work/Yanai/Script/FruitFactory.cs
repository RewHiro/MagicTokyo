
using UnityEngine;
using System.Collections;


public class FruitFactory : MonoBehaviour {

  [SerializeField]
  Item apple_, lemon_, peach_;

  [SerializeField]
  int max_item_;

  [SerializeField]
  int instantiate_count_;

  int count_ = 0;

  void Start() {
    var item_object = GameObject.Instantiate(apple_.gameObject);
    item_object.transform.Translate(0, 0, 0);
  }

  void Update() {

  }
}
