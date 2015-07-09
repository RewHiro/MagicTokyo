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

    int apple_num_ = 0;
    public int AppleNum { get { return apple_num_; } }
    int lemon_num_ = 0;
    public int LemonNum { get { return lemon_num_; } }

    bool is_remote_damage_ = false;
    bool is_guard_ = false;

    HandController hand_controller_ = null;

    [SerializeField, Range(0.0f, 10.0f), TooltipAttribute("回した時間")]
    float TURN_SECOND = 1.0f;

    void Start()
    {
        if (!isLocalPlayer) return;
        hand_controller_ = FindObjectOfType<HandController>();
    }

    void Update()
    {
        if (!isLocalPlayer) return;
        GestureUpdate();
        AttackEffect();
    }

    void GestureUpdate()
    {
        var gesture_list = hand_controller_.GetFrame().Gestures();
        if (gesture_list[0].IsValid)
        {
            CircleGesture gesture = new CircleGesture(gesture_list[0]);
            if (gesture.DurationSeconds < TURN_SECOND) return;
            CmdTellServerAttack(true);
            CmdTellServerFruitNum(1, 1);
        }
        else
        {
            CmdTellServerAttack(false);
        }
    }

    void AttackEffect()
    {
        if (is_remote_damage_)
        {
            if (is_guard_) return;
            is_guard_ = true;
            // 攻撃エフェクト
            FindObjectOfType<FruitCreater>().PeachCreate(1);
        }
        else
        {
            is_guard_ = false;
        }
    }

    [Command]
    void CmdTellServerAttack(bool is_attack)
    {
        is_attack_ = is_attack;
    }

    [Command]
    void CmdTellServerFruitNum(int apple_num,int lemon_num)
    {
        apple_num_ = apple_num;
        lemon_num_ = lemon_num;
    }

    [ClientRpc]
    public void RpcTellClientRemoteDamage(bool is_remote_damage)
    {
        is_remote_damage_ = is_remote_damage;
    }
}
