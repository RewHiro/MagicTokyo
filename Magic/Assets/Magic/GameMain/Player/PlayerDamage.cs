using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

[NetworkSettings(channel = 0, sendInterval = 0.001f)]
public class PlayerDamage : NetworkBehaviour
{
    [SyncVar]
    bool is_damage_ = false;
    public bool IsDamage { get { return is_damage_; } }

    bool is_guard_ = false;

    int apple_num_ = 0;
    int lemon_num_ = 0;

    Particle particle_ = null;

    [SerializeField
, TooltipAttribute("ここに「AttackApple」prefabを入れてください\n(プログラマー用)")]
    GameObject apple_attack_obj_ = null;
    [SerializeField
    , TooltipAttribute("ここに「AttackLemon」prefabを入れてください\n(プログラマー用)")]
    GameObject lemon_attack_obj_ = null;

    void Start()
    {
        if (!isLocalPlayer) return;
        particle_ = FindObjectOfType<Particle>();
    }

    void Update()
    {
        if (!isLocalPlayer) return;
        if (is_damage_)
        {
            if (is_guard_) return;

            for (int num = 0; num < apple_num_; num++)
            {
                GameObject game_object = Instantiate(apple_attack_obj_);
                game_object.GetComponent<Ike3AttackFruitMove>().UpDownChange(1);
                game_object.transform.position = new Vector3(0, 5, 0);
            }

            for (int num = 0; num < lemon_num_; num++)
            {
                GameObject game_object = Instantiate(lemon_attack_obj_);
                game_object.GetComponent<Ike3AttackFruitMove>().UpDownChange(0, 1);
                game_object.transform.position = new Vector3(0, 5, 0);
            }
            is_guard_ = true;

        }
        else
        {
            is_guard_ = false;
        }
    }

    [ClientRpc]
    public void RpcTellClientDamage(bool is_damage, int apple_num, int lemon_num)
    {
        is_damage_ = is_damage;
        apple_num_ = apple_num;
        lemon_num_ = lemon_num;
    }
}
