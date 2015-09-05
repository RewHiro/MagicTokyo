using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class DorianBoomAttacker : NetworkBehaviour {

    bool is_attack_ = false;
    public bool IsAttack { get { return is_attack_; } }

    bool is_remote_damage_ = false;

    TuboInDestroy tubo_in_destory_ = null;

    void Start()
    {
        if (!MyNetworkLobbyManager.s_singleton.IsTutorial)
        {
            if (!isLocalPlayer) return;
        }
        tubo_in_destory_ = FindObjectOfType<TuboInDestroy>();
    }

    void Update()
    {
        if (!MyNetworkLobbyManager.s_singleton.IsTutorial)
        {
            if (!isLocalPlayer) return;
        }
        if (tubo_in_destory_.IsInDorain)
        {
            CmdTellServerAttack(true);
            is_attack_ = true;
        }
        if (is_remote_damage_)
        {
            CmdTellServerAttack(false);
            is_attack_ = false;
            tubo_in_destory_.ResetDorian();
        }
    }

    [Command]
    void CmdTellServerAttack(bool is_attack)
    {
        is_attack_ = is_attack;
    }

    [ClientRpc]
    public void RpcTellClientRemoteDamage(bool is_remote_damage)
    {
        is_remote_damage_ = is_remote_damage;
    }
}
