﻿using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
using Leap;

[NetworkSettings(channel = 0, sendInterval = 0.001f)]
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

    [SerializeField
    , TooltipAttribute("ここに「AttackApple」prefabを入れてください\n(プログラマー用)")]
    GameObject apple_attack_obj_ = null;
    [SerializeField
    , TooltipAttribute("ここに「AttackLemon」prefabを入れてください\n(プログラマー用)")]
    GameObject lemon_attack_obj_ = null;
    [SerializeField
    , TooltipAttribute("ここに「Pot」prefabを入れてください\n(プログラマー用)")]
    GameObject pot_obj_ = null;

    void Start()
    {
        if (!MyNetworkLobbyManager.s_singleton.IsTutorial)
        {
            if (!isLocalPlayer) return;
        }
        hand_controller_ = FindObjectOfType<HandController>();
        tubo_in_destory_ = FindObjectOfType<TuboInDestroy>();
    }

    void Update()
    {
        if (!MyNetworkLobbyManager.s_singleton.IsTutorial)
        {
            if (!isLocalPlayer) return;
            if (GetComponent<GameEndDirector>().IsStart) return;
            GestureUpdate();
            AttackEffect();
        }
        else
        {
            TutorialGestureUpdate();
        }

    }

    void GestureUpdate()
    {
        is_attack_ = false;
        if (!MyNetworkLobbyManager.s_singleton.IsTutorial)
        {
            CmdTellServerAttack(false);
        }
        foreach (var hand in hand_controller_.GetFrame().Hands)
        {
            if (!hand.IsRight) continue;
            var gesture_list = hand.Frame.Gestures();

            foreach (Gesture gesture in gesture_list)
            {
                var circle_gesture = new CircleGesture(gesture);

                if (circle_gesture.IsValid)
                {

                    if (circle_gesture.DurationSeconds < TURN_SECOND) return;
                    CmdTellServerAttack(true);
                    is_attack_ = true;
                    var apple_num = tubo_in_destory_.GetApumonCount();
                    var lemon_num = tubo_in_destory_.GetLemonCount();
                    CmdTellServerFruitNum(
                        apple_num,
                        lemon_num);
                    apple_num_ = apple_num;
                    lemon_num_ = lemon_num;
                    break;
                }
            }
        }
    }

    void AttackEffect()
    {
        if (is_remote_damage_)
        {
            if (tubo_in_destory_.GetKudamonCount() <= 0)
            {
                is_guard_ = false;
            }

            if (is_guard_) return;

            is_guard_ = true;

            if (MyNetworkLobbyManager.s_singleton.Is1P)
            {
                AudioManager.Instance.PlaySe(20);
            }
            else
            {
                AudioManager.Instance.PlaySe(16);
            }

            bool flag_ap = apple_attack_obj_ != null;
            bool flag_le = lemon_attack_obj_ != null;
            bool flag_po = pot_obj_ != null;

            bool flag = flag_ap && flag_le && flag_po;
            if (flag)
            {
                for (int num = 0; num < apple_num_; num++)
                {
                    GameObject game_object = Instantiate(apple_attack_obj_);
                    GameObject pot_obj = GameObject.Find(pot_obj_.name);
                    game_object.transform.position = pot_obj.transform.position;
                    game_object.name = apple_attack_obj_.name;
                }
                for (int num = 0; num < lemon_num_; num++)
                {
                    GameObject game_object = Instantiate(lemon_attack_obj_);
                    GameObject pot_obj = GameObject.Find(pot_obj_.name);
                    game_object.transform.position = pot_obj.transform.position;
                    game_object.name = lemon_attack_obj_.name;
                }
            }
            else
            {
                Debug.Log("AttackApple(lemon) または Pot が入っていません");
            }

            // 攻撃エフェクト
            if (POT_LIMIT_NUM <= tubo_in_destory_.GetKudamonCount())
            {
                FindObjectOfType<FruitCreater>().PeachCreate(1);
                GetComponent<EggPlantAttacker>().AttackEggPlant();
            }
            tubo_in_destory_.ResetCount();
            FindObjectOfType<PotGaugeController>().GaugeReset();
        }
        else
        {
            is_guard_ = false;
        }
    }

    void TutorialGestureUpdate()
    {
        foreach (var hand in hand_controller_.GetFrame().Hands)
        {
            if (!hand.IsRight) continue;
            var gesture_list = hand.Frame.Gestures();

            foreach (Gesture gesture in gesture_list)
            {
                var circle_gesture = new CircleGesture(gesture);

                if (circle_gesture.IsValid)
                {

                    if (circle_gesture.DurationSeconds < TURN_SECOND) return;
                    var apple_num = tubo_in_destory_.GetApumonCount();
                    var lemon_num = tubo_in_destory_.GetLemonCount();

                    if (MyNetworkLobbyManager.s_singleton.Is1P)
                    {
                        AudioManager.Instance.PlaySe(20);
                    }
                    else
                    {
                        AudioManager.Instance.PlaySe(16);
                    }

                    bool flag_ap = apple_attack_obj_ != null;
                    bool flag_le = lemon_attack_obj_ != null;
                    bool flag_po = pot_obj_ != null;

                    bool flag = flag_ap && flag_le && flag_po;
                    if (flag)
                    {
                        for (int num = 0; num < apple_num; num++)
                        {
                            GameObject game_object = Instantiate(apple_attack_obj_);
                            GameObject pot_obj = GameObject.Find(pot_obj_.name);
                            game_object.transform.position = pot_obj.transform.position;
                            game_object.name = apple_attack_obj_.name;
                        }
                        for (int num = 0; num < lemon_num; num++)
                        {
                            GameObject game_object = Instantiate(lemon_attack_obj_);
                            GameObject pot_obj = GameObject.Find(pot_obj_.name);
                            game_object.transform.position = pot_obj.transform.position;
                            game_object.name = lemon_attack_obj_.name;
                        }
                    }
                    else
                    {
                        Debug.Log("AttackApple(lemon) または Pot が入っていません");
                    }

                    // 攻撃エフェクト
                    if (POT_LIMIT_NUM <= tubo_in_destory_.GetKudamonCount())
                    {
                        FindObjectOfType<FruitCreater>().PeachCreate(1);
                    }
                    tubo_in_destory_.ResetCount();
                    FindObjectOfType<PotGaugeController>().GaugeReset();

                    break;
                }
            }
        }
    }

    [Command]
    void CmdTellServerAttack(bool is_attack)
    {
        is_attack_ = is_attack;
    }

    [Command]
    void CmdTellServerFruitNum(int apple_num, int lemon_num)
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
