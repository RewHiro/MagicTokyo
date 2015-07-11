using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
using UnityEngine.UI;

public class StartDeirector : NetworkBehaviour
{
    bool is_start_ = false;
    public bool IsStart { get { return is_start_; } }


    bool is_ready_ = false;
    float count_ = 0;

    [ClientRpc]
    public void RpcTellClientReady(bool is_ready)
    {
        is_ready_ = is_ready;
    }

    [Command]
    public void CmdTellServerStart(bool is_start)
    {
        is_start_ = is_start;
    }

    void Update()
    {
        if (!isLocalPlayer) return;
        if (!is_ready_) return;
        count_ += Time.deltaTime;
        GameObject.Find("Text (2)").GetComponent<Text>().enabled = false;
        if (count_ < 1) return;
        CmdTellServerStart(true);
    }
}
