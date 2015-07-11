
using UnityEngine;
using System.Collections;


public class FruitFactory : MonoBehaviour {

  [SerializeField]
  GameObject apple_, lemon_, peach_;

  [SerializeField]
  int max_item_;

  [SerializeField]
  int instantiate_count_;

  int count_ = 0;
  int item_count_ = 0;

  void Start() {
    var item_object = GameObject.Instantiate(apple_.gameObject);
    item_object.transform.Translate(-5, -2, 0);
  }

  void Update() {
    if (max_item_ == item_count_) return;
    count_++;

    if (count_ < instantiate_count_) return;

    var item_object = GameObject.Instantiate(apple_.gameObject);
    item_object.transform.Translate(-5, -2, 0);

    item_count_++;
    count_ = 0;
  }
}
