using UnityEngine;
using System.Collections;

public class GripMagnet : MonoBehaviour {

    delegate void MagicAction();

    PlayerMagicManager player_magic_manager_ = null;

    HandController hand_controller_ = null;

    void Start()
    {

        hand_controller_ = FindObjectOfType<HandController>();

    }

    void Update()
    {
        var magic_type = player_magic_manager_.MagicType;

        foreach (var hand in FindObjectsOfType<SkeletalHand>())
        {
            var strength = hand.GetLeapHand().GrabStrength;
            hand.GetComponentInChildren<Light>().intensity = strength * 8.0f;

            if (1.0f <= strength)
            {
                break;
            }
        }
    }
}
