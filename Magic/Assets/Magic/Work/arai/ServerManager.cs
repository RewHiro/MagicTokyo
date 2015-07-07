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