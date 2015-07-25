using UnityEngine;
using System.Collections;

public class Ike3TyhoonEffectHalo : MonoBehaviour {

    private Vector3 drain_pos_;
    public Vector3 DrainPos { 
        set 
        { 
            drain_pos_ = value;
            move_value_ = drain_pos_ - transform.position;
            move_value_.Normalize();
            //Debug.Log(move_value_);
        }
    }

    private Vector3 move_value_ = Vector3.zero;
    private float move_speed_ = 0;
	
	// Update is called once per frame
	void Update () {
        MoveHalo();
	}

    void MoveHalo()
    {
        move_speed_ += 0.01f;
        transform.position += move_value_ * move_speed_;

        if (drain_pos_.y > transform.position.y)
        {
            Destroy(gameObject);
        }
    }
}
