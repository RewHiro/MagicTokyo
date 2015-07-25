using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
using UnityEngine.UI;

public class GameStartDirector : NetworkBehaviour {

    enum State
    {
        CONNECT,
        READY,
        START,
    }
    State state_ = State.CONNECT;

    public bool IsStart { get { return state_ == State.START; } }

    float count_ = 3;
    public int ReadyCount { get { return (int)count_; } }

    void Update()
    {
        if (!isLocalPlayer) return;
        if (state_ != State.READY) return;

        count_ += -Time.deltaTime;
        
        if (count_ > 0) return;

        state_ = State.START;
        CmdTellServerStart();
    }

    [ClientRpc]
    public void RpcTellClientReady()
    {
        state_ = State.READY;
    }

    [Command]
    public void CmdTellServerStart()
    {
        state_ = State.START;
    }
}
