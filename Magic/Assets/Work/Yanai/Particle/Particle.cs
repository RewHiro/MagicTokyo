using UnityEngine;
using System.Collections;

public class Particle : MonoBehaviour {

  public enum State {
    Attack, Damage
  }

  private bool is_active_ = false;
  bool isActive {
    get { return is_active_; }
    set {
      is_active_ = value;
      Count = 0;
      GetComponent<ParticleSystem>().emissionRate = EmmisionRate;
    }
  }

  [SerializeField]
  private int Max_Time = 40;

  private int count_;
  int Count {
    get { return count_; }
    set { count_ = value; }
  }

  [SerializeField]
  private float active_rate_ = 30;
  float EmmisionRate {
    get { return is_active_ ? active_rate_ : 0; }
  }

  // エフェクトが消えるまでのタイム
  [SerializeField]
  private float Atk_Lifetime = 5;

  [SerializeField]
  private float Dmg_Lifetime = 1;

  // 壺の位置を指定
  [SerializeField]
  private Vector3 origin_pos_;

  // 降ってくる位置を指定
  [SerializeField]
  private Vector3 fall_pos_;


  void Awake() {
    GetComponent<ParticleSystem>().Play();
    GetComponent<ParticleSystem>().emissionRate = EmmisionRate;
  }

  void Start() {}

  void Update() {
    if (!isActive) return;

    Count++;
    if (Count > Max_Time) {
      isActive = false;
    }
  }

  public void apply(State state) {
    isActive = true;
    switch (state) {
      case State.Attack: {
        gameObject.transform.position = origin_pos_;
        gameObject.transform.eulerAngles = new Vector3(270, 0, 0);
        GetComponent<ParticleSystem>().startLifetime = Atk_Lifetime;
      } break;

      case State.Damage: {
        gameObject.transform.position = fall_pos_;
        gameObject.transform.eulerAngles = new Vector3(90, 0, 0);
        GetComponent<ParticleSystem>().startLifetime = Dmg_Lifetime;
      } break;
    }
  }
}
