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

    Particle particle_ = null;

    void Start()
    {
        if (!isLocalPlayer) return;
        particle_ = FindObjectOfType<Particle>();
    }

    void Update()
    {
        if (!isLocalPlayer) return;
        if (!is_remote_damage_) return;
        CmdTellServerAttack(false, 0);
        is_attack_ = false;
    }

    public void StartEggPlantPanic()
    {
        CmdTellServerAttack(true, 5);
        is_attack_ = true;
        egg_plant_num_ = 5;
        particle_.apply(Particle.State.Attack);
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
