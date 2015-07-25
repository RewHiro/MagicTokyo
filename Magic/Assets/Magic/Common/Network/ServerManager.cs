using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class ServerManager : NetworkBehaviour
{

    GameObject local_player_ = null;
    GameObject remote_player_ = null;
    float count_ = 60;

    const int READY_PLAYER_NUM = 2;

    [SerializeField,Range(1,120),TooltipAttribute("制限時間")]
    int TIME_LIMIT_SECOND = 2;

    [SerializeField, Range(1, 10), TooltipAttribute("レモネードが出すアプモンの倍率")]
    int LOCAL_APPLE_MULTIPLE = 1;

    [SerializeField, Range(1, 10), TooltipAttribute("レモネードが出すレーモンの倍率")]
    int LOCAL_LEMON_MULTIPLE = 2;

    [SerializeField, Range(1, 10), TooltipAttribute("アプルが出すアプモンの倍率")]
    int REMOTE_APPLE_MULTIPLE = 2;

    [SerializeField, Range(1, 10), TooltipAttribute("アプルが出すレーモンの倍率")]
    int REMOTE_LEMON_MULTIPLE = 1;


    void Start()
    {
        if (!isServer) return;
        count_ = TIME_LIMIT_SECOND;
    }

    void Update()
    {
        if (!isServer) return;
        PlayerFind();

        if (local_player_ == null) return;
        ClientUpdate();

        if (!local_player_.GetComponent<GameStartDirector>().IsStart) return;
        if (!remote_player_.GetComponent<GameStartDirector>().IsStart) return;

        if (local_player_.GetComponent<GameEndDirector>().IsStart) return;
        var delta_time = Time.deltaTime;
        count_ += -delta_time;
        
        if (0 != (int)count_) return;
        local_player_.GetComponent<GameEndDirector>().RpcTellClientStart();
        remote_player_.GetComponent<GameEndDirector>().RpcTellClientStart();
    }

    void ClientUpdate()
    {
        var local_player_damage
            = local_player_.GetComponent<PlayerDamage>();
        var remote_player_damage
            = remote_player_.GetComponent<PlayerDamage>();

        var local_player_attacker
            = local_player_.GetComponent<PlayerAttacker>();
        var remote_player_attacker
            = remote_player_.GetComponent<PlayerAttacker>();

        var local_player_fruit_counter
            = local_player_.GetComponent<FruitCounter>();
        var remote_player_fruit_counter
            = remote_player_.GetComponent<FruitCounter>();

        var local_player_egg_plant_damage
            = local_player_.GetComponent<EggPlantDamage>();
        var remote_player_egg_plant_damage
            = remote_player_.GetComponent<EggPlantDamage>();

        var local_player_egg_plant_attacker
            = local_player_.GetComponent<EggPlantAttacker>();
        var remote_player_egg_plant_attacker
            = remote_player_.GetComponent<EggPlantAttacker>();

        var local_player_dorian_boom_damage
            = local_player_.GetComponent<DorianBoomDamage>();
        var remote_player_dorian_boom_damage
            = remote_player_.GetComponent<DorianBoomDamage>();

        var local_player_dorian_boom_attacker
            = local_player_.GetComponent<DorianBoomAttacker>();
        var remote_player_dorian_boom_attacker
            = remote_player_.GetComponent<DorianBoomAttacker>();

        local_player_damage.RpcTellClientDamage(
            remote_player_attacker.IsAttack,
            remote_player_attacker.AppleNum * LOCAL_APPLE_MULTIPLE,
            remote_player_attacker.LemonNum * LOCAL_LEMON_MULTIPLE);
        remote_player_damage.RpcTellClientDamage(
            local_player_attacker.IsAttack,
            local_player_attacker.AppleNum * REMOTE_APPLE_MULTIPLE,
            local_player_attacker.LemonNum * REMOTE_LEMON_MULTIPLE);

        local_player_fruit_counter.RpcTellClientCount(
            remote_player_fruit_counter.FruitNum);
        remote_player_fruit_counter.RpcTellClientCount(
            local_player_fruit_counter.FruitNum);

        local_player_attacker.RpcTellClientRemoteDamage(
            remote_player_damage.IsDamage);
        remote_player_attacker.RpcTellClientRemoteDamage(
            local_player_damage.IsDamage);

        local_player_.GetComponent<TimeLimitter>().
            RpcTellClientLimitCount((int)count_);
        remote_player_.GetComponent<TimeLimitter>().
            RpcTellClientLimitCount((int)count_);

        local_player_egg_plant_damage.RpcTellClientEggPlantDamage(
            remote_player_egg_plant_attacker.IsAttack,
            remote_player_egg_plant_attacker.EggPlantNum);
        remote_player_egg_plant_damage.RpcTellClientEggPlantDamage(
            local_player_egg_plant_attacker.IsAttack,
            local_player_egg_plant_attacker.EggPlantNum);

        local_player_egg_plant_attacker.RpcTellClientRemoteDamage(
            remote_player_egg_plant_damage.IsDamage);
        remote_player_egg_plant_attacker.RpcTellClientRemoteDamage(
            local_player_egg_plant_damage.IsDamage);

        local_player_dorian_boom_damage.RpcTellClientDorianBoomDamage(
            remote_player_dorian_boom_attacker.IsAttack);
        remote_player_dorian_boom_damage.RpcTellClientDorianBoomDamage(
            local_player_dorian_boom_attacker.IsAttack);

        local_player_dorian_boom_attacker.RpcTellClientRemoteDamage(
            remote_player_dorian_boom_damage.IsDamage);
        remote_player_dorian_boom_attacker.RpcTellClientRemoteDamage(
            local_player_dorian_boom_damage.IsDamage);
    }

    void PlayerFind()
    {
        if (local_player_ != null) return;
        if (NetworkManager.singleton.numPlayers != READY_PLAYER_NUM) return;

        var players = GameObject.FindObjectsOfType<PlayerSetting>();
        foreach(var player in players)
        {
            if (player.isLocalPlayer)
            {
                local_player_ = player.gameObject;
            }
            else
            {
                remote_player_ = player.gameObject;
            }
        }

        local_player_.GetComponent<GameStartDirector>().RpcTellClientReady();
        remote_player_.GetComponent<GameStartDirector>().RpcTellClientReady();
    }
}