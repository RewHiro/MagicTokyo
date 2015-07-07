using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

[NetworkSettings(channel = 0, sendInterval = 0.001f)]
public class PlayerDamage : NetworkBehaviour
{
    [SyncVar]
    bool is_damage_ = false;
    bool is_guard_ = false;

    void Update()
    {
        if (!isLocalPlayer) return;
        if (is_damage_)
        {
            if (is_guard_) return;
            FindObjectOfType<FruitCreater>().AppleCreate(5);
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
}
