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

    [SerializeField, Range(0.0f, 10.0f), TooltipAttribute("回した時間")]
    float TURN_SECOND = 1.0f;

    bool is_guard_ = false;

    void Start()
    {
        if (!MyNetworkLobbyManager.s_singleton.IsTutorial)
        {
            if (!isLocalPlayer) return;
        }

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
        if (!MyNetworkLobbyManager.s_singleton.IsTutorial)
        {
            if (!isLocalPlayer) return;
            if (GetComponent<GameEndDirector>().IsStart) return;
        }




        foreach (var hand in hand_controller_.GetFrame().Hands)
        {
            if (!hand.IsLeft) continue;

            var gesture_list = hand.Frame.Gestures();

            foreach (Gesture gesture in gesture_list)
            {
                var magic_type = player_magic_manager_.MagicType;
                if (magic_type == -1) break;
                var circle_gesture = new CircleGesture(gesture);

                if (circle_gesture.IsValid)
                {
                    if (!hand.IsLeft) continue;
                    if (gesture.DurationSeconds < TURN_SECOND) continue;
                    if (!player_magic_manager_.MagicExecute()) continue;
                    magic_action_list_[magic_type]();

                    AudioManager.Instance.PlaySe(11);
                    break;
                }
            }
            break;
        }
    }
}
