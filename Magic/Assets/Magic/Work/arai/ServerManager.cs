using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class ServerManager : NetworkBehaviour
{

    bool is_player_find_ = false;

    GameObject local_player_ = null;
    GameObject remote_player_ = null;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (!isServer) return;
        PlayerFind();
        if (!is_player_find_) return;
        local_player_.GetComponent<PlayerDamage>().RpcTellClientDamage(remote_player_.GetComponent<PlayerAttacker>().IsAttack);
        remote_player_.GetComponent<PlayerDamage>().RpcTellClientDamage(local_player_.GetComponent<PlayerAttacker>().IsAttack);

        local_player_.GetComponent<PlayerDamage>().RpcTellClientFruitNum(remote_player_.GetComponent<PlayerAttacker>().AppleNum * 2, remote_player_.GetComponent<PlayerAttacker>().LemonNum);
        remote_player_.GetComponent<PlayerDamage>().RpcTellClientFruitNum(local_player_.GetComponent<PlayerAttacker>().AppleNum, local_player_.GetComponent<PlayerAttacker>().LemonNum * 2);

        local_player_.GetComponent<FruitCounter>().RpcTellClientCount(remote_player_.GetComponent<FruitCounter>().FruitNum);
        remote_player_.GetComponent<FruitCounter>().RpcTellClientCount(local_player_.GetComponent<FruitCounter>().FruitNum);

        local_player_.GetComponent<PlayerAttacker>().RpcTellClientRemoteDamage(remote_player_.GetComponent<PlayerDamage>().IsDamage);
        remote_player_.GetComponent<PlayerAttacker>().RpcTellClientRemoteDamage(local_player_.GetComponent<PlayerDamage>().IsDamage);
    }

    void PlayerFind()
    {
        if (is_player_find_) return;
        if (NetworkManager.singleton.numPlayers != 2) return;
        is_player_find_ = true;
        local_player_ = GameObject.Find("Player1");
        remote_player_ = GameObject.Find("Player2");
    }
}