using UnityEngine;
using System.Collections;
using Leap;

public class HandMagic : MonoBehaviour
{

    float[] hand_grab_angle_;

    Vector3 handCenter_;

    GameObject hand_manager_;

    SkeletalHand[] skeletal_hand_;

    Light light_left_;
    Light light_right_;

    [SerializeField]
    GameObject hand_pos_;

    void Start()
    {
        hand_manager_ = GameObject.Find("HandManager");
        hand_grab_angle_ = new float[2];
    
    }

    void Update()
    {
        //if (null == hand) return;
        var left_hand_light = GameObject.Find("HandManager/CleanRobotLeftHand(Clone)/" + hand_pos_.name);
        var left_hand_ = GameObject.Find("CleanRobotLeftHand(Clone)");
        left_hand_.transform.SetParent(hand_manager_.transform);

        var right_hand_light = GameObject.Find("HandManager/CleanRobotRightHand(Clone)/" + hand_pos_.name);
        var right_hand_ = GameObject.Find("CleanRobotRightHand(Clone)");
        right_hand_.transform.SetParent(hand_manager_.transform);


        skeletal_hand_ = GetComponentsInChildren<SkeletalHand>();
        for (int i = 0; i < skeletal_hand_.Length; ++i)
        {
            hand_grab_angle_[i] = skeletal_hand_[i].GetLeapHand().GrabStrength;

        }

        light_left_ = left_hand_light.GetComponent<Light>();

        light_right_ = right_hand_light.GetComponent<Light>();



        if (hand_grab_angle_[0] >= 0.5)
        {
            light_left_.enabled = true;

        }
        else
        {
            light_left_.enabled = false;

        }

        if (hand_grab_angle_[1] >= 0.5)
        {
            light_right_.enabled = true;

        }
        else
        {
            light_right_.enabled = false;
        }


    }
}
