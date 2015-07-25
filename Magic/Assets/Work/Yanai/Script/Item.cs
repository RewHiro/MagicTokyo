
using UnityEngine;
using System.Collections;


public class Item : MonoBehaviour {

  enum State {
    Slide,
    Rotate,
    Pop,
  }

  [SerializeField]
  float SLIDE_SPEED = 0.1f;

  State state_;
  SceneState title_;
  Hashtable table;
  bool is_pop_;

  void Awake() {
    state_  = State.Slide;
    table   = new Hashtable();
    is_pop_ = false;
  }

  void Start() {
    title_ = gameObject.GetComponent<SceneState>();
  }

  public void Hide() {
    GameObject.Destroy(gameObject);
  }

  void Update() {
    switch (state_) {
      case State.Slide:
        Slide();
        break;
      case State.Rotate:
        Rotate();
        break;
      case State.Pop:
        Pop();
        break;
    }
  }

  void Slide() {
    gameObject.transform.Translate(SLIDE_SPEED, 0.0f, 0.0f);
    if (gameObject.transform.position.x > 0) {
      state_ = State.Rotate;
    }
  }

  void Rotate() {
    // 回転処理
    // TODO:もっといい処理を探す
    gameObject.transform.Rotate(0.0f, 0.0f, 1.0f);
    gameObject.transform.Translate(0.05f, 0.0f, 0.0f);

    //if (!gameObject.GetComponent<SceneState>().isStart()) return;
    if (!Input.GetKeyDown(KeyCode.Space)) return;
    state_ = State.Pop;
  }

  void Pop() {
    if (is_pop_) return;

    // はじける処理
    // TODO:もうすこし綺麗に弾けるようにする
    table.Add("x", Random.Range(0.0f, 1.0f) < 0.5f ? Random.Range(-15.0f, -10.0f) : Random.Range(10.0f, 15.0f));
    table.Add("y", Random.Range(0.0f, 1.0f) < 0.5f ? Random.Range(-12.0f, -8.0f) : Random.Range(8.0f, 12.0f));
    table.Add("time", 3.0f);
    table.Add("delay", 0.2f);
    iTween.MoveTo(gameObject, table);
    is_pop_ = true;
  }
}
