using UnityEngine;
using System.Collections;
using Leap;

public class HandMagic : MonoBehaviour {

    float[] a ;

    Vector3 handCenter_;

    GameObject hand_manager;

    SkeletalHand[] skeletal_hand_;

	void Start () 
    {
        hand_manager = GameObject.Find("HandManager");
	}
	
	void Update () 
    {

        var hand_left_ = GameObject.Find("CleanRobotLeftHand(Clone)");
        var hand_right_ = GameObject.Find("CleanRobotRightHand(Clone)");
        //var rigid_hand_ = GameObject.Find("RigidHand(Clone)");
        hand_right_.transform.SetParent(hand_manager.transform);
        hand_left_.transform.SetParent(hand_manager.transform);
        //rigid_hand_.transform.SetParent(hand_manager.transform);

        skeletal_hand_ = GetComponentsInChildren<SkeletalHand>();

       // var hands = FindObjectsOfType<SkeletalHand>();
        if ((null == skeletal_hand_[0])&&(null == skeletal_hand_[1])) return;
//        foreach (var hand in skeletal_hand_)
        for (int i = 0; i < skeletal_hand_.Length; ++i)
        {
            a[i] =skeletal_hand_[i].GetLeapHand().GrabStrength;
            //handCenter_ = new Vector3(hand.GetLeapHand().PalmPosition.x, hand.GetLeapHand().PalmPosition.y, hand.GetLeapHand().PalmPosition.z);

            //var hand_left_ = GameObject.Find("CleanRobotLeftHand(Clone)").GetComponent<SkeletalHand>();
            //var hand_right_ = GameObject.Find("CleanRobotRightHand(Clone)").GetComponent<SkeletalHand>();
            //        Debug.Log(a);
        }
        Behaviour halo_left_ = (Behaviour)hand_left_.GetComponent("Halo");
        //            halo_left_.transform.position = hand_left_.transform.position;

        Behaviour halo_right_ = (Behaviour)hand_right_.GetComponent("Halo");



        if (a[0] >= 0.5 || a[1] >= 0.5)
        {
            halo_left_.enabled = true;
            halo_right_.enabled = true;
            Debug.Log(a);

        }
        else
        {
            halo_left_.enabled = false;
            halo_right_.enabled = false;
        }


      
	}
}
