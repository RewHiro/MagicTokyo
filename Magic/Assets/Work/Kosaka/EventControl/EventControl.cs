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
    int time_balance_ = 20;

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

    [ClientRpc]
    public void RpcSetSelectEvent(EventName value)
    {
        select_event_ = value;
    }

    [Command]
    void CmdTellServerSelectEvent(EventName value)
    {
        select_event_ = value;
    }

    [SerializeField, Tooltip("ドリアンボムの個数")]
    int durian_bomb_num_ = 1;

    //--------------------------------------------------------------------------

    void Start()
    {
        if (!isLocalPlayer) return;
        FindComponent();
        if (!isServer) return;
        select_event_ = (EventName)Random.Range(0, 3);
    }

    void Update()
    {
        if (!isLocalPlayer) return;
        RandEvent();
    }

    void RandEvent()
    {
        if (time_limitter_ == null) return;

        if (!game_start_director_.IsStart) return;
        var time = time_limitter_.LimitCount;

        if (time_ == time) return;
        if (time == 60) return;
        if (time <= 0) return;

        if (time % time_balance_ == 0)
        {
            switch (select_event_)
            {
                case EventName.DURIAN_BOMB:
                    FindObjectOfType<FruitCreater>().DorianCreate();
                    break;

                case EventName.KUDAMON_BOUND:
                    FindObjectOfType<BounceKudamon>().Starter();
                    break;

                case EventName.KUDAMON_RUSH:
                    FindObjectOfType<RushEventer>().StartEvent();
                    break;
            }
            time_ = time;

            if (!isServer) return;
            var select_event = (EventName)Random.Range(0, 3);
            select_event_ = select_event;
            CmdTellServerSelectEvent(select_event);
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

