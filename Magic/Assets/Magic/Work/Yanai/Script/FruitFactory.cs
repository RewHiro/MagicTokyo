
using UnityEngine;
using System.Collections;


public class FruitFactory : MonoBehaviour {

  [SerializeField]
  GameObject apple_, lemon_, peach_;

  [SerializeField]
  int MAX_ITEM = 10;

  [SerializeField]
  int INSTANTIATE_COUNT = 60;

  int count_ = 0;
  int item_count_ = 0;

  void Start() {
    var item_object = GameObject.Instantiate(apple_.gameObject);
    item_object.transform.Translate(-5, -2, 0);
  }

  void Update() {
    if (MAX_ITEM == item_count_) return;
    count_++;

    if (count_ < INSTANTIATE_COUNT) return;

    var item_object = GameObject.Instantiate(apple_.gameObject);
    item_object.transform.Translate(-5, -2, 0);

    item_count_++;
    count_ = 0;
  }
}
