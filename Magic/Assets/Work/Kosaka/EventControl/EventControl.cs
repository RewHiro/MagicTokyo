using UnityEngine;
using System.Collections;

public class EventControl : MonoBehaviour
{
    //時間
    int time_;

    TimeLimitter time_limitter_ = null;
    GameStartDirector game_start_director_ = null;

    [SerializeField, Range(0, 20), Tooltip("イベントの間隔 (短) <---> (長)")]
    int time_balance_ = 20;

    //選ばれたイベント
    enum EventName
    {
        DURIAN_BOMB,        //ドリアンボム
        KUDAMON_BOUND,      //跳ねるくだモン
        KUDAMON_RUSH,       //鍋MAX
        EVENT_MAX,
    }
    EventName select_event_ = EventName.DURIAN_BOMB;

    [SerializeField, Tooltip("ドリアンボムの個数")]
    int durian_bomb_num_ = 1;

    //--------------------------------------------------------------------------

    void Start()
    {

    }

    void Update()
    {
        FindComponent();
        if (time_limitter_ == null) return;

        if (!game_start_director_.IsStart) return;
        var time = time_limitter_.LimitCount;
        if (time_ == time) return;

        if (time % time_balance_ == 0)
        {
            select_event_ = (EventName)Random.Range(0, (int)EventName.EVENT_MAX);

            switch (select_event_)
            {
                case EventName.DURIAN_BOMB:
                    FindObjectOfType<FruitCreater>().DorianCreate(durian_bomb_num_);
                    break;

                case EventName.KUDAMON_BOUND:
                    FindObjectOfType<BounceKudamon>().Starter();
                    break;

                case EventName.KUDAMON_RUSH:
                    GetComponent<RushEventer>().StartEvent();
                    break;
            }
            time_ = time;
        }
    }

    void FindComponent()
    {
        if (time_limitter_ != null) return;
        foreach (var player in FindObjectsOfType<PlayerSetting>())
        {
            if(!player.isLocalPlayer)continue;
            time_limitter_ = player.GetComponent<TimeLimitter>();
            game_start_director_ = player.GetComponent<GameStartDirector>();
        }
    }
}

