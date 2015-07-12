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
        GameObject.Find("Text (3)").GetComponent<Text>().text = ReadyCount.ToString();
        
        if (count_ > 0) return;
        
        CmdTellServerStart();
        GameObject.Find("Text (3)").GetComponent<Text>().enabled = false;
    }

    [ClientRpc]
    public void RpcTellClientReady()
    {
        GameObject.Find("Text (2)").GetComponent<Text>().enabled = false;
        state_ = State.READY;
    }

    [Command]
    public void CmdTellServerStart()
    {
        state_ = State.START;
    }
}
