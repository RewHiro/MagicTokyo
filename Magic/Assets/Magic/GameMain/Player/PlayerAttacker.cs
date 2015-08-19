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
    public bool IsRemoteDamage { get { return is_remote_damage_; } }

    bool is_guard_ = false;

    bool is_right_ = false;

    HandController hand_controller_ = null;
    //SkeletalHand hand_ = null;

    [SerializeField, Range(0.0f, 10.0f), TooltipAttribute("回した時間")]
    float TURN_SECOND = 1.0f;

    TuboInDestroy tubo_in_destory_ = null;

    const int POT_LIMIT_NUM = 10;

    Particle particle_ = null;

    void Start()
    {
        if (!isLocalPlayer) return;
        hand_controller_ = FindObjectOfType<HandController>();
        tubo_in_destory_ = FindObjectOfType<TuboInDestroy>();
        particle_ = FindObjectOfType<Particle>();
    }

    void Update()
    {
        if (!isLocalPlayer) return;
        GestureUpdate();
        AttackEffect();
    }

    void GestureUpdate()
    {
        foreach (var hand in hand_controller_.GetFrame().Hands)
        {
            var gesture_list = hand.Frame.Gestures();
            if (gesture_list[0].IsValid)
            {

                CircleGesture gesture = new CircleGesture(gesture_list[0]);
                if (gesture.DurationSeconds < TURN_SECOND) return;
                CmdTellServerAttack(true);
                is_attack_ = true;
                is_right_ = hand.IsRight;
                var apple_num = tubo_in_destory_.GetApumonCount();
                var lemon_num = tubo_in_destory_.GetLemonCount();
                CmdTellServerFruitNum(
                    apple_num,
                    lemon_num);
                apple_num_ = apple_num;
                lemon_num_ = lemon_num;
                particle_.apply(Particle.State.Attack);

            }
            else
            {
                CmdTellServerAttack(false);
                is_attack_ = false;
            }
        }
    }

    void AttackEffect()
    {
        if (is_remote_damage_)
        {
            if (is_guard_) return;
            is_guard_ = true;

            // 攻撃エフェクト
            if (POT_LIMIT_NUM <= tubo_in_destory_.GetKudamonCount() && 
                is_right_)
            {
                FindObjectOfType<FruitCreater>().PeachCreate(1);
            }
            tubo_in_destory_.ResetCount();
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
