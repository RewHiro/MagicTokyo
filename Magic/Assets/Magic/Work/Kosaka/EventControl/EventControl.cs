using UnityEngine;
using System.Collections;

public class EventControl : MonoBehaviour
{
    //時間
    int time_;

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

    void Update()
    {
        time_ = gameObject.GetComponent<TimeLimitter>().LimitCount;

        if (time_ % time_balance_ == 0)
        {
            select_event_ = (EventName)Random.Range(0, (int)EventName.EVENT_MAX);

            switch (select_event_)
            {
                case EventName.DURIAN_BOMB:
                    gameObject.GetComponent<FruitCreater>().DorianCreate(durian_bomb_num_);
                    break;

                case EventName.KUDAMON_BOUND:
                    gameObject.GetComponent<BounceKudamon>().Starter();
                    break;

                case EventName.KUDAMON_RUSH:
                    break;
            }
        }
    }
}
