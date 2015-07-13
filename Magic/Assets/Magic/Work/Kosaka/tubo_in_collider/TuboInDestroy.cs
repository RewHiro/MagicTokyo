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

    //くだモンの名前
    const string LEMON_NAME = "re-mon";
    const string APUMON_NAME = "apumon";
    const string MOMON_NAME = "momon";

    //-----------------------------------------------------------------

    //それぞれのくだモンの鍋に入った(消した)数のゲッター
    public int GetLemonCount() { return lemon_count_; }
    public int GetApumonCount() { return apumon_count_; }
    public int GetMomonCount() { return momon_count_; }
    public int GetKudamonCount() {
        var kudamon_add = lemon_count_ + apumon_count_ + momon_count_;
        return kudamon_add;
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
