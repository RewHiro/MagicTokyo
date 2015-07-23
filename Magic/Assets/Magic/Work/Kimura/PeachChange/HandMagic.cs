using UnityEngine;
using System.Collections;
using Leap;

public class HandMagic : MonoBehaviour
{

    float[] hand_grab_angle_;

    Vector3 handCenter_;

    GameObject hand_manager;

    SkeletalHand[] skeletal_hand_;

    [SerializeField]
    GameObject hand_pos_;

    void Start()
    {
        hand_manager = GameObject.Find("HandManager");
        hand_grab_angle_ = new float[2];
    }

    void Update()
    {

        var left_hand_light = GameObject.Find("HandManager/CleanRobotLeftHand(Clone)/" + hand_pos_.name);
        var left_hand_ = GameObject.Find("CleanRobotLeftHand(Clone)");
        left_hand_.transform.SetParent(hand_manager.transform);

        var right_hand_light = GameObject.Find("HandManager/CleanRobotRightHand(Clone)/" + hand_pos_.name);
        var right_hand_ = GameObject.Find("CleanRobotRightHand(Clone)");
        right_hand_.transform.SetParent(hand_manager.transform);


        skeletal_hand_ = GetComponentsInChildren<SkeletalHand>();
        for (int i = 0; i < skeletal_hand_.Length; ++i)
        {
            hand_grab_angle_[i] = skeletal_hand_[i].GetLeapHand().GrabStrength;

        }
        Behaviour halo_left_ = (Behaviour)left_hand_light.GetComponent("Halo");

        Behaviour halo_right_ = (Behaviour)right_hand_light.GetComponent("Halo");


        if (hand_grab_angle_[0] >= 0.5 )
        {
            halo_left_.enabled = true;
           
        }
        else
        {
            halo_left_.enabled = false;
           
        }

        if( hand_grab_angle_[1] >= 0.5)
        {   
            halo_right_.enabled = true;
            Debug.Log(hand_grab_angle_);

        }
        else
        {
            halo_right_.enabled = false;
        }
        

    }
}
