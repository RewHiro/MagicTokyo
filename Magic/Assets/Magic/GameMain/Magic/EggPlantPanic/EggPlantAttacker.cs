using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class EggPlantAttacker : NetworkBehaviour
{
    bool is_attack_ = false;
    public bool IsAttack { get { return is_attack_; } }

    int egg_plant_num_ = 0;
    public int EggPlantNum { get { return egg_plant_num_; } }

    const float MAGIC_TIME = 0.5f;
    public float GetMagicTime { get { return MAGIC_TIME; } }

    bool is_remote_damage_ = false;

    [SerializeField
    , TooltipAttribute("ここに「AttackJamamon」prefabを入れてください\n(プログラマー用)")]
    GameObject jamamon_attack_obj_ = null;
    [SerializeField
    , TooltipAttribute("ここに「Pot」prefabを入れてください\n(プログラマー用)")]
    GameObject pot_obj_ = null;

    void Update()
    {
        if (!isLocalPlayer) return;
        if (!is_remote_damage_) return;
        CmdTellServerAttack(false, 0);
        is_attack_ = false;
    }

    public void StartEggPlantPanic()
    {
        if (!isLocalPlayer) return;
        CmdTellServerAttack(true, 5);
        is_attack_ = true;
        egg_plant_num_ = 5;

        bool flag_jat = jamamon_attack_obj_ != null;
        bool flag_po = pot_obj_ != null;

        bool flag = flag_jat && flag_po;
        if (flag)
        {
            for (int num = 0; num < 5; num++)
            {
                GameObject game_object = Instantiate(jamamon_attack_obj_);
                GameObject pot_obj = GameObject.Find(pot_obj_.name);
                game_object.transform.SetParent(pot_obj.transform);
                game_object.name = jamamon_attack_obj_.name;
            }
        }
        else
        {
            Debug.Log("AttackJamamon または Pot が入っていません");
        }
    }

    [Command]
    void CmdTellServerAttack(bool is_attack, int egg_plant_num)
    {
        is_attack_ = is_attack;
        egg_plant_num_ = egg_plant_num;
    }

    [ClientRpc]
    public void RpcTellClientRemoteDamage(bool is_remote_damage)
    {
        is_remote_damage_ = is_remote_damage;
    }
}
