using UnityEngine;
using System.Collections;


public class TuboInDestroy : MonoBehaviour
{
    [SerializeField, Tooltip("鍋に入ってから消えるまでの時間(単位：秒)")]
    float DESTROY_TIME = 3.0f;

    //それぞれのくだモンの鍋に入った(消した)数
    int lemon_count_ = 0;
    int apumon_count_ = 0;
    int momon_count_ = 0;

    //カウントリセットのためのbool
    bool is_reset_ = false;
    //カウントリセットの時間（間隔）
    int reset_time_ = 0;
    const int RESET_TIME_LIMIT = 1;

    //くだモンの名前
    const string LEMON_NAME = "re-mon";
    const string APUMON_NAME = "apumon";
    const string MOMON_NAME = "momon";

    //-----------------------------------------------------------------

    //それぞれのくだモンの鍋に入った(消した)数のゲッター
    public int GetLemonCount { get { return lemon_count_; } }
    public int GetApumonCount { get { return apumon_count_; } }
    public int GetMomonCount { get { return momon_count_; } }
    public int GetKudamonCount
    {
        get { return lemon_count_ + apumon_count_ + momon_count_; }
    }

    void Update()
    {
        var players = FindObjectsOfType<PlayerSetting>();
        foreach (var player in players)
        {
            if (player.isLocalPlayer)
            {
                var is_player_attack = GetComponent<PlayerAttacker>();
                if (is_player_attack.IsAttack) { is_reset_ = true; }
            }
        }

        if (is_reset_)
        {
            reset_time_++;
            if (reset_time_ == RESET_TIME_LIMIT)
            {
                lemon_count_ = 0;
                apumon_count_ = 0;
                momon_count_ = 0;
                is_reset_ = false;
            }
        }
    }

    //鍋の中のTrigger判定
    void OnTriggerEnter(Collider other)
    {

        //それぞれのくだモンを「消す処理」と「カウント処理」（と「入ったものを出力」するためのデバッグ）
        if (other.name == LEMON_NAME)
        {
            Destroy(other.gameObject, DESTROY_TIME);
            lemon_count_++;
            //Debug.Log(" Lemon Destroy " + lemon_count);
        }

        if (other.name == APUMON_NAME)
        {
            Destroy(other.gameObject, DESTROY_TIME);
            apumon_count_++;
            //Debug.Log(" Apple Destroy " + apumon_count);
        }

        if (other.name == MOMON_NAME)
        {
            Destroy(other.gameObject, DESTROY_TIME);
            momon_count_++;
            //Debug.Log(" Peach Destroy " + momon_count);
        }

    }
}
