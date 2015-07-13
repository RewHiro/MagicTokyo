using UnityEngine;
using System.Collections;


public class TuboInDestroy : MonoBehaviour
{
    [SerializeField, Tooltip("鍋に入ってから消えるまでの時間(単位：秒)")]
    float destroy_time = 3.0f;

    //それぞれのくだモンの鍋に入った(消した)数
    int lemon_count = 0;
    int apumon_count = 0;
    int momon_count = 0;

    //くだモンの名前
    const string lemon_name = "re-mon";
    const string apumon_name = "apumon";
    const string momon_name = "momon";

    //-----------------------------------------------------------------

    //それぞれのくだモンの鍋に入った(消した)数のゲッター
    public int getLemonCount() { return lemon_count; }
    public int getApumonCount() { return apumon_count; }
    public int getMomonCount() { return momon_count; }
    public int getKudamonCount() {
        var kudamonAdd = lemon_count + apumon_count + momon_count;
        return kudamonAdd;
    }

    //鍋の中のTrigger判定
    void OnTriggerEnter(Collider other)
    {

        //それぞれのくだモンを「消す処理」と「カウント処理」（と「入ったものを出力」するためのデバッグ）
        if (other.name == lemon_name)
        {
            Destroy(other.gameObject, destroy_time);
            lemon_count++;
            Debug.Log(" Lemon Destroy " + lemon_count);
        }

        if (other.name == apumon_name)
        {
            Destroy(other.gameObject, destroy_time);
            apumon_count++;
            Debug.Log(" Apple Destroy " + apumon_count);
        }

        if (other.name == momon_name)
        {
            Destroy(other.gameObject, destroy_time);
            momon_count++;
            Debug.Log(" Peach Destroy " + momon_count);
        }

    }
}
