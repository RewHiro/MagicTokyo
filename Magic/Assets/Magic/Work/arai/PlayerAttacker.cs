using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
using Leap;

[NetworkSettings(channel=0,sendInterval = 0.001f)]
public class PlayerAttacker : NetworkBehaviour
{
    [SyncVar]
    bool is_attack_ = false;
    public bool IsAttack { get { return is_attack_; } }

    bool is_gesture_ = false;

    HandController hand_controller_ = null;

    void Start()
    {
        if (!isLocalPlayer) return;
        hand_controller_ = FindObjectOfType<HandController>();
    }

    void GestureUpdate()
    {
        var gesture_list = hand_controller_.GetFrame().Gestures();
        if (gesture_list[0].IsValid)
        {
            CircleGesture gesture = new CircleGesture(gesture_list[0]);
            if (gesture.DurationSeconds < 1.0f) return;
            if (is_gesture_) return;
            is_gesture_ = true;
            CmdTellServerAttack(true);
        }
        else
        {
            CmdTellServerAttack(false);
            is_gesture_ = false;
        }
    }

    void Update()
    {
        if (!isLocalPlayer) return;
        GestureUpdate();
    }

    [Command]
    void  CmdTellServerAttack(bool is_attack)
    {
        is_attack_ = is_attack;
    }
}
