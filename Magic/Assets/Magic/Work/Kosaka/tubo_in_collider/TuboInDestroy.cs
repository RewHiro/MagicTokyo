using UnityEngine;
using System.Collections;


public class TuboInDestroy : MonoBehaviour
{
    [SerializeField, Tooltip("鍋に入ってから消えるまでの時間(単位：秒)")]
    float DESTROY_TIME = 0.0f;

    //それぞれのくだモンの鍋に入った(消した)数
    int lemon_count_ = 0;
    int apumon_count_ = 0;
    bool is_in_momon_ = false;

    //くだモンの名前
    const string LEMON_NAME = "re-mon";
    const string APUMON_NAME = "apumon";
    const string MOMON_NAME = "momon";

    //-----------------------------------------------------------------

    //それぞれのくだモンの鍋に入った(消した)数のゲッター
    public int GetLemonCount() { return lemon_count_; }
    public int GetApumonCount() { return apumon_count_; }
    public bool GetMomonCount() { return is_in_momon_; }
    public int GetKudamonCount()
    {
        var kudamon_add = lemon_count_ + apumon_count_;
        return kudamon_add;
    }

    public void Update()
    {
        //蓋のコライダーを作る
        var lid = new GameObject();
        if (GetKudamonCount() == 10)
        {
            lid.transform.position = new Vector3(0, 3, 2.5f);
            lid.transform.rotation = new Quaternion(-20, 0, 0, 0.0f);
            lid.transform.localScale = new Vector3(2, 0.1f, 2);

            lid.gameObject.AddComponent<BoxCollider>();
        }

        //ジェスチャーしたらコライダーを消す
        //条件にサークルのジェスチャーを取得して入れれば行けるはず
        if (true)
        {
            GameObject.Destroy(lid);
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
            is_in_momon_ = true;
            //Debug.Log(" Peach Destroy " + momon_count);
        }

    }
}
