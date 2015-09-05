using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class DorianBoomDamage : NetworkBehaviour
{

    [SerializeField
, TooltipAttribute("ここに「AttackDrian」prefabを入れてください\n(プログラマー用)")]
    GameObject drian_attack_obj_ = null;

    bool is_damage_ = false;
    public bool IsDamage { get { return is_damage_; } }

    bool is_guard_ = false;

    void Update()
    {
        if (!MyNetworkLobbyManager.s_singleton.IsTutorial)
        {
            if (!isLocalPlayer) return;
        }
        if (is_damage_)
        {
            if (is_guard_) return;

            GameObject game_object = Instantiate(drian_attack_obj_);
            game_object.GetComponent<Ike3AttackFruitMove>().UpDownChange(0, 0, 0, 1);
            game_object.transform.position = new Vector3(0, 5, 0);

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
