using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
using Leap;
using System.Collections.Generic;

public class PlayerMagicAttacker : NetworkBehaviour
{
    delegate void MagicAction();

    PlayerMagicManager player_magic_manager_ = null;

    Dictionary<int, MagicAction> magic_action_list_ =
        new Dictionary<int, MagicAction>();

    GameObject hand_manager_ = null;

    void Start()
    {
        if (!isLocalPlayer) return;

        hand_manager_ = GameObject.Find("HandManager");

        magic_action_list_.Add(
            0,
            FindObjectOfType<Ike3KudamonKinesisu>().KudamonKinesis);

        magic_action_list_.Add(
            1,
            GetComponent<EggPlantAttacker>().StartEggPlantPanic);

        magic_action_list_.Add(
            2,
            FindObjectOfType<SmallFruit>().SmallFruitStart);

        magic_action_list_.Add(
            3,
            FindObjectOfType<Ike3TyphoonSetting>().TyphoonOn);

        magic_action_list_.Add(
            4,
            FindObjectOfType<PeachChange>().PeachChangeStart);


        player_magic_manager_ = GetComponent<PlayerMagicManager>();
    }

    void Update()
    {
        if (!isLocalPlayer) return;
        var magic_type = player_magic_manager_.MagicType;

        foreach (var hand in hand_manager_.GetComponentsInChildren<SkeletalHand>())
        {
            var light = hand.GetComponentInChildren<Light>();
            if (magic_type == -1)
            {
                light.intensity -= Time.deltaTime * 4;
                continue;
            }

            var pinch = hand.GetLeapHand().PinchStrength;
            light.intensity = pinch * 8.0f;
            
            if (1.0f <= pinch)
            {
                magic_action_list_[magic_type]();
                player_magic_manager_.MagicExecute();
                break;
            }
        }
    }
}
