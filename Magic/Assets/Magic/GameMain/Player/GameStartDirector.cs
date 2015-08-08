using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
using UnityEngine.UI;

public class GameStartDirector : NetworkBehaviour
{

    public enum State
    {
        CONNECT,
        READY,
        START,
    }
    State state_ = State.CONNECT;

    public bool IsConnect { get { return state_ == State.CONNECT; } }
    public bool IsReady { get { return state_ == State.READY; } }
    public bool IsStart { get { return state_ == State.START; } }

    float count_ = 3;
    public int ReadyCount { get { return (int)count_; } }

    Text text_;

    void Start()
    {
        text_ = GameObject.Find("StartText").GetComponent<Text>();
        CmdTellServerStart(state_);
    }

    void Update()
    {
        if (!isLocalPlayer) return;
        CmdTellServerStart(state_);
        if (state_ != State.READY) return;
        ChangeText();
        count_ += -Time.deltaTime;

        if (count_ > 0) return;

        state_ = State.START;
        CmdTellServerStart(State.START);
        text_.enabled = false;
    }

    void ChangeText()
    {
        var count = (int)count_;
        text_.text = count.ToString();
        if (count_ > 0.5f) return;
        text_.text = "Start";
    }

    [ClientRpc]
    public void RpcTellClientReady()
    {
        state_ = State.READY;
    }

    [Command]
    public void CmdTellServerStart(State state)
    {
        state_ = state;
    }
}
