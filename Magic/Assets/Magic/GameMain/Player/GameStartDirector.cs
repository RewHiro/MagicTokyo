using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
using UnityEngine.UI;

public class GameStartDirector : NetworkBehaviour
{

    enum State
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
    float standby_count_ = 0.0f;
    const float STANBY_LIMIT = 3.0f;
    public int ReadyCount { get { return (int)count_; } }

    Text text_;

    [SerializeField
   , TooltipAttribute("ここに「Ike3ParticleManager」prefabを入れてください\n(プログラマー用)")]
    private GameObject particle_manager_;

    [SerializeField
    , TooltipAttribute("表示させたいパーティクルを入れてください(「「3」」←これ　こ　れ)→「2」→「1」→「Start」")]
    private ParticleSystem particle_;

    void Start()
    {
        text_ = GameObject.Find("StartText").GetComponent<Text>();
    }

    [ClientRpc]
    public void RpcCountDownLocal()
    {
        GameObject particle_manager = GameObject.Find(particle_manager_.name);
        ParticleSystem game_object = Instantiate(particle_);
        game_object.transform.SetParent(particle_manager.transform);
        game_object.transform.position = new Vector3(0.0f, 3.0f, 1.5f);
        game_object.name = particle_.name;
    }

    //public void RpcCountDownRemote()
    //{
    //    GameObject particle_manager = GameObject.Find(particle_manager_.name);
    //    ParticleSystem game_object = Instantiate(particle_);
    //    game_object.transform.SetParent(particle_manager.transform);
    //    game_object.transform.position = new Vector3(0.0f, 4.0f, 1.5f);
    //    game_object.name = particle_.name;
    //}

    void Update()
    {
        if (!isLocalPlayer) return;
        if (state_ != State.READY) return;

        if(STANBY_LIMIT > standby_count_)
        {
            standby_count_ += Time.deltaTime;
            return;
        }
        if(count_ == STANBY_LIMIT) { RpcCountDownLocal(); }
        //ChangeText();
        text_.text = "";
        count_ += -Time.deltaTime;

        if (count_ > -1) return;

        state_ = State.START;
        CmdTellServerStart();
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
    public void CmdTellServerStart()
    {
        state_ = State.START;
    }
}
