using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class ServerManager : NetworkBehaviour
{

    GameObject local_player_ = null;
    GameObject remote_player_ = null;

    [SerializeField, Range(1, 10), TooltipAttribute("レモネードが出すアプモンの倍率")]
    int LOCAL_APPLE_MULTIPLE = 1;

    [SerializeField, Range(1, 10), TooltipAttribute("レモネードが出すレーモンの倍率")]
    int LOCAL_LEMON_MULTIPLE = 2;

    [SerializeField, Range(1, 10), TooltipAttribute("アプルが出すアプモンの倍率")]
    int REMOTE_APPLE_MULTIPLE = 2;

    [SerializeField, Range(1, 10), TooltipAttribute("アプルが出すレーモンの倍率")]
    int REMOTE_LEMON_MULTIPLE = 1;


    void Update()
    {
        if (!isServer) return;
        PlayerFind();
        if (local_player_ == null) return;

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

        local_player_damage.RpcTellClientDamage(
            remote_player_attacker.IsAttack,
            remote_player_attacker.AppleNum * LOCAL_APPLE_MULTIPLE,
            remote_player_attacker.LemonNum * LOCAL_LEMON_MULTIPLE);
        remote_player_damage.RpcTellClientDamage(
            local_player_attacker.IsAttack,
            local_player_attacker.AppleNum * REMOTE_APPLE_MULTIPLE,
            local_player_attacker.LemonNum * REMOTE_LEMON_MULTIPLE);

        local_player_fruit_counter.RpcTellClientCount(remote_player_fruit_counter.FruitNum);
        remote_player_fruit_counter.RpcTellClientCount(local_player_fruit_counter.FruitNum);

        local_player_attacker.RpcTellClientRemoteDamage(remote_player_damage.IsDamage);
        remote_player_attacker.RpcTellClientRemoteDamage(local_player_damage.IsDamage);
    }

    void PlayerFind()
    {
        if (local_player_ != null) return;
        if (NetworkManager.singleton.numPlayers != 2) return;
        local_player_ = GameObject.Find("Player1");
        remote_player_ = GameObject.Find("Player2");
    }
}