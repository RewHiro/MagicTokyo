using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class DorianBoomDamage : NetworkBehaviour
{

    bool is_damage_ = false;
    public bool IsDamage { get { return is_damage_; } }

    bool is_guard_ = false;

    void Update()
    {
        if (!isLocalPlayer) return;
        if (is_damage_)
        {
            if (is_guard_) return;
            FindObjectOfType<FruitCreater>().DorianCreate(1);
            is_guard_ = true;
        }
        else
        {
            is_guard_ = false;
        }
    }

    [ClientRpc]
    public void RpcTellClientDorianBoomDamage(bool is_damage)
    {
        is_damage_ = is_damage;
    }
}
