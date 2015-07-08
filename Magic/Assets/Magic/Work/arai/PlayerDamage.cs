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

    void Update()
    {
        if (!isLocalPlayer) return;
        if (is_damage_)
        {
            if (is_guard_) return;
            FindObjectOfType<FruitCreater>().AppleCreate(apple_num_);
            FindObjectOfType<FruitCreater>().LemonCreate(lemon_num_);
            is_guard_ = true;
        }
        else
        {
            is_guard_ = false;
        }
    }

    [ClientRpc]
    public void RpcTellClientDamage(bool is_damage)
    {
        is_damage_ = is_damage;
    }

    [ClientRpc]
    public void RpcTellClientFruitNum(int apple_num,int lemon_num)
    {
        apple_num_ = apple_num;
        lemon_num_ = lemon_num;
    }
}
