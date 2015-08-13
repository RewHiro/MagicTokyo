using UnityEngine;
using System.Collections;

public class GripMagnet : MonoBehaviour
{

    delegate void MagicAction();

    PlayerMagicManager player_magic_manager_ = null;

    HandController hand_controller_ = null;

    KudamonGravity[] kudamon_gravity;

    int random_selection_kudamon_;

    bool is_magnet_;

    void Start()
    {
        is_magnet_ = false;
        hand_controller_ = FindObjectOfType<HandController>();
    }

    void Update()
    {

        foreach (var hand in FindObjectsOfType<SkeletalHand>())
        {
            var strength = hand.GetLeapHand().GrabStrength;
            hand.GetComponentInChildren<Light>().intensity = strength * 8.0f;


            if (1.0f <= strength)
            {
                kudamon_gravity = GetComponentsInChildren<KudamonGravity>();
                if(is_magnet_ == false)
                {
                    random_selection_kudamon_ = Random.Range(0, kudamon_gravity.Length - 1);
                    Debug.Log(random_selection_kudamon_);
                    is_magnet_ = true;
                }
                kudamon_gravity[random_selection_kudamon_].IsGravity(true);
                Vector3 grip_move = hand.GetPalmPosition() - kudamon_gravity[random_selection_kudamon_].transform.position;
                Vector3 kudamon_move_speed = grip_move.normalized;

                if (grip_move.x <= 0.5f && grip_move.y <= 0.5f && grip_move.z <= 0.5f)
                {
                    kudamon_gravity[random_selection_kudamon_].transform.position = hand.GetPalmPosition();
                }
                else
                {
                    kudamon_gravity[random_selection_kudamon_].transform.position += kudamon_move_speed / 5;
                }

                break;
            }
            else
            if(0.0f < strength && 1.0f > strength)
            {
                is_magnet_ = false;
                kudamon_gravity[random_selection_kudamon_].IsGravity(false);
            }
        }
    }
}
