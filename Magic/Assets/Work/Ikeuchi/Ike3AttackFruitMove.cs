using UnityEngine;
using System.Collections;

public class Ike3AttackFruitMove : MonoBehaviour {

    private Vector3 move_value_ = Vector3.zero;

    // 上←→下の動きを切り替える関数
    void UpDownChange()
    {
        move_value_ *= -1;
    }

    // Use this for initialization
    void Start () {
        const float LIMIT_VALUE = 0.15f;
        float up_down_value = 0.15f;
        move_value_ = new Vector3(Random.Range(-LIMIT_VALUE, LIMIT_VALUE),
                                  up_down_value,
                                  Random.Range(-LIMIT_VALUE, LIMIT_VALUE));
    }
	
	// Update is called once per frame
	void Update () {
        transform.position += move_value_;

        if (move_value_.x > 0) { move_value_.x -= 0.01f; }
        if (move_value_.x < 0) { move_value_.x += 0.01f; }
        if (move_value_.z > 0) { move_value_.z -= 0.01f; }
        if (move_value_.z < 0) { move_value_.z += 0.01f; }

        if (transform.position.y > 10) { Destroy(gameObject); }
    }
}
