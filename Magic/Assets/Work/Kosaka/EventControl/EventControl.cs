using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class EventControl : NetworkBehaviour
{
    //時間
    int time_;

    TimeLimitter time_limitter_ = null;
    GameStartDirector game_start_director_ = null;

    [SerializeField, Range(0, 20), Tooltip("イベントの間隔 (短) <---> (長)")]
    int TIME_BALANCE = 20;

    //選ばれたイベント
    public enum EventName
    {
        DURIAN_BOMB,        //ドリアンボム
        KUDAMON_BOUND,      //跳ねるくだモン
        KUDAMON_RUSH,       //鍋MAX
        EVENT_MAX,
    }

    [SyncVar]
    EventName select_event_ = EventName.EVENT_MAX;

    public EventName SelectEvent
    {
        get { return select_event_; }
    }

    bool is_create_durian_boom_ = false;

    public bool IsCreateDurianBoom
    {
        get { return is_create_durian_boom_; }
    }

    [ClientRpc]
    public void RpcSetSelectEvent(EventName value, bool is_durian_boom)
    {
        select_event_ = value;
        is_create_durian_boom_ = is_durian_boom;
    }

    [Command]
    void CmdTellServerSelectEvent(EventName value, bool is_durian_boom)
    {
        select_event_ = value;
        is_create_durian_boom_ = is_durian_boom;
    }

    //--------------------------------------------------------------------------

    void Start()
    {
        if (!isLocalPlayer) return;
        FindComponent();
        if (!isServer) return;
        select_event_ = (EventName)Random.Range(0, (int)EventName.EVENT_MAX);
        is_create_durian_boom_ = MyRandom.RandomBool();
    }

    void Update()
    {
        if (!isLocalPlayer) return;

        RandEvent();
    }

    void RandEvent()
    {
        if (time_limitter_ == null) return;
        Debug.Log(game_start_director_.IsStart);
        Debug.Log(game_start_director_.isLocalPlayer);

        if (!game_start_director_.IsStart) return;
        var time = time_limitter_.LimitCount;
        if (time_ == time) return;
        if (time == 60) return;
        if (time <= 0) return;

        if (time % TIME_BALANCE == 0)
        {
            switch (select_event_)
            {
                case EventName.DURIAN_BOMB:
                    if (is_create_durian_boom_)
                    {
                        FindObjectOfType<FruitCreater>().DorianCreate();
                    }
                    break;

                case EventName.KUDAMON_BOUND:
                    FindObjectOfType<BounceKudamon>().Starter();
                    break;

                case EventName.KUDAMON_RUSH:
                    FindObjectOfType<RushEventer>().StartEvent();
                    break;
            }
            time_ = time;
        }

        if(time == 50 || time == 30)
        {

            if (!isServer) return;

            var select_event = (EventName)Random.Range(0, (int)EventName.EVENT_MAX);
            var is_durian_boom = MyRandom.RandomBool();
            select_event_ = select_event;
            is_create_durian_boom_ = is_durian_boom;
            CmdTellServerSelectEvent(select_event, is_durian_boom);
        }
    }

    void FindComponent()
    {
        if (time_limitter_ != null) return;
        foreach (var player in FindObjectsOfType<PlayerSetting>())
        {
            if (!player.isLocalPlayer) continue;

            time_limitter_ = player.GetComponent<TimeLimitter>();
            game_start_director_ = player.GetComponent<GameStartDirector>();
        }
    }
}

