
using UnityEngine;
using System.Collections.Generic;


[System.Serializable]
class UI {
  public GameObject timePrefab = null;
  public GameObject itemPrefab = null;
  public GameObject gaugePrefab = null;
  public GameObject potPrefab = null;
}


public class InterfaceGenerator : MonoBehaviour {

  [SerializeField]
  UI ui_ = null;

  void Start() {
    var objects = new List<GameObject>();

    objects.Add(Instantiate(ui_.timePrefab));
    objects.Add(Instantiate(ui_.itemPrefab));
    objects.Add(Instantiate(ui_.gaugePrefab));
    objects.Add(Instantiate(ui_.potPrefab));

    foreach (var obj in objects) {
      obj.transform.parent = gameObject.transform;
    }
  }
}
