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

    HandController hand_controller_ = null;

    bool is_guard_ = false;

    void Start()
    {
        if (!isLocalPlayer) return;

        hand_manager_ = GameObject.Find("HandManager");
        hand_controller_ = FindObjectOfType<HandController>();

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
        if (GetComponent<GameEndDirector>().IsStart) return;
        var magic_type = player_magic_manager_.MagicType;

        foreach (var hand in hand_controller_.GetFrame().Hands)
        {
            if (magic_type == -1)
            {
                continue;
            }

            var gesture_list = hand.Frame.Gestures();
            if (gesture_list[0].IsValid)
            {
                if (!hand.IsLeft) continue;
                if (is_guard_) break;
                CircleGesture gesture = new CircleGesture(gesture_list[0]);
                if (gesture.DurationSeconds < 0.5f) return;
                magic_action_list_[magic_type]();
                player_magic_manager_.MagicExecute();
                is_guard_ = true;
                AudioManager.Instance.PlaySe(11);
            }
            else
            {
                is_guard_ = false;
            }
        }
    }
}
