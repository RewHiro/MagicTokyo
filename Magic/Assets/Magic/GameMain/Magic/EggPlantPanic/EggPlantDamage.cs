using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class EggPlantDamage : NetworkBehaviour
{
    bool is_damage_ = false;
    public bool IsDamage { get { return is_damage_; } }

    bool is_guard_ = false;
    
    int egg_plant_num_ = 0;
    public int EggPlantNum { get { return egg_plant_num_; } }

    Particle particle_ = null;

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
            FindObjectOfType<FruitCreater>().EggPlantCreate(egg_plant_num_);
            is_guard_ = true;
            particle_.apply(Particle.State.Damage);
        }
        else
        {
            is_guard_ = false;
        }
    }

    [ClientRpc]
    public void RpcTellClientEggPlantDamage(bool is_damage,int egg_plant_num)
    {
        is_damage_ = is_damage;
        egg_plant_num_ = egg_plant_num;
    }
}
