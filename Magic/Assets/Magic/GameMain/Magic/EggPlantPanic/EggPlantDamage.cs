using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class EggPlantDamage : NetworkBehaviour
{
    [SerializeField
, TooltipAttribute("ここに「AttackJamamon」prefabを入れてください\n(プログラマー用)")]
    GameObject jamamon_attack_obj_ = null;

    bool is_damage_ = false;
    public bool IsDamage { get { return is_damage_; } }

    bool is_guard_ = false;

    int egg_plant_num_ = 0;
    public int EggPlantNum { get { return egg_plant_num_; } }

    Particle particle_ = null;

    void Start()
    {
        if (!MyNetworkLobbyManager.s_singleton.IsTutorial)
        {
            if (!isLocalPlayer) return;
        }
        particle_ = FindObjectOfType<Particle>();
    }

    void Update()
    {
        if (!MyNetworkLobbyManager.s_singleton.IsTutorial)
        {
            if (!isLocalPlayer) return;
        }
        if (is_damage_)
        {
            if (is_guard_) return;
            for (var i = 0; i < egg_plant_num_; ++i)
            {
                GameObject game_object = Instantiate(jamamon_attack_obj_);
                game_object.GetComponent<Ike3AttackFruitMove>().UpDownChange(0, 0, 1);
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
    public void RpcTellClientEggPlantDamage(bool is_damage, int egg_plant_num)
    {
        is_damage_ = is_damage;
        egg_plant_num_ = egg_plant_num;
    }
}
