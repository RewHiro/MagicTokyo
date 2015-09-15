using UnityEngine;
using System.Collections;

public class Ike3SpecialEventManager : MonoBehaviour {

    [SerializeField
    , TooltipAttribute("金のリンゴモデルを入れてください")]
    private GameObject gold_apple_;

    [SerializeField
    , TooltipAttribute("銀のレモンモデルを入れてください")]
    private GameObject silver_lemon_;

    [SerializeField
    , TooltipAttribute("このイベントを起こす残り時間を入れてください(秒)")]
    private float invocation_time_ = 30;

    private TimeLimitter time_limitter_ = null;

    private bool only_once_flag_ = true;

    private Vector3 CreatePosition { get { return new Vector3(0.0f, 5.0f, 1.0f); } }

    // Use this for initialization
    void Start()
    {
        //CreateSilverLemon();
        //CreateGoldApple();
    }

    // Update is called once per frame
    void Update () {
        //if (Input.GetKeyDown("f"))
        //{
        //    CreateSilverLemon();
        //    CreateGoldApple();
        //}

        SpecialEvent();
    }

    void SearchTimer()
    {
        if (time_limitter_ == null)
        {
            foreach (var player in FindObjectsOfType<TimeLimitter>())
            {
                if (!player.isLocalPlayer) continue;
                time_limitter_ = player;
            }
        }
    }

    void SpecialEvent()
    {
        SearchTimer();
        //Debug.Log(time_limitter_.LimitCount);
        if (only_once_flag_)
        {
            if (time_limitter_.LimitCount == invocation_time_)
            {
                CreateFruit();
                only_once_flag_ = false;
            }
        }
    }

    void CreateGoldApple()
    {
        if (gold_apple_ == null) return;
        GameObject game_object = Instantiate(gold_apple_);
        game_object.transform.SetParent(transform);
        game_object.transform.position = CreatePosition;
        game_object.name = gold_apple_.name;
    }

    void CreateSilverLemon()
    {
        if (silver_lemon_ == null) return;
        GameObject game_object = Instantiate(silver_lemon_);
        game_object.transform.SetParent(transform);
        game_object.transform.position = CreatePosition;
        game_object.name = silver_lemon_.name;
    }

    void CreateFruit()
    {
        //Debug.Log("homo");
        if (MyNetworkLobbyManager.s_singleton.Is1P)
        {
            //Debug.Log("AP");
            CreateSilverLemon();
        }
        else
        {
            //Debug.Log("LE");
            CreateGoldApple();
        }
    }
}
