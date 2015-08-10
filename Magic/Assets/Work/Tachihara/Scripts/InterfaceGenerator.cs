
using UnityEngine;


[System.Serializable]
class UI {
  public GameObject timePrefab;
  public GameObject itemPrefab;
  public GameObject gaugePrefab;
  public GameObject potPrefab;
}


public class InterfaceGenerator : MonoBehaviour {

  [SerializeField]
  UI ui_ = null;

  void Start() {
    var objects = new GameObject[4];

    objects[0] = Instantiate(ui_.timePrefab);
    objects[1] = Instantiate(ui_.itemPrefab);
    objects[2] = Instantiate(ui_.gaugePrefab);
    objects[3] = Instantiate(ui_.potPrefab);
    
    foreach (var obj in objects) {
      obj.transform.parent = gameObject.transform;
    }
  }
}
