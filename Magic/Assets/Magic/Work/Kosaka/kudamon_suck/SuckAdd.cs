using UnityEngine;
using System.Collections;


//RigidBodyと計算で吸い込みを演出var
public class SuckAdd : MonoBehaviour {

    [SerializeField, Range(0, 2), Tooltip("変更する空気抵抗の大きさ (小)<--->(大) ")]
    float rigid_drag = 1;

    //くだモンの名前
    const string lemon_name = "re-mon";
    const string apumon_name = "apumon";
    const string momon_name = "momon";
    const string pod_name = "tubo_kai";

    //-----------------------------------------------------------------

    //吸い取り機能の開始判定
    void OnTriggerEnter(Collider other)
    {
        //otherのRigidBody取得
        var Rigid = other.gameObject.GetComponent<Rigidbody>();

        //空気抵抗を付けて動きを抑制
        Rigid.drag = rigid_drag;

        //当たった時の出力デバッグ
        if (other.name == lemon_name ||
            other.name == apumon_name ||
            other.name == momon_name)
            Debug.Log(" Rigid Trriger Collision");
    }

    //吸い取り機能の最中
    void OnTriggerStay(Collider other)
    {
        //鍋の位置(座標)を取得
        var tubo_pos = GameObject.Find(pod_name).GetComponent<Transform>().position;

        //Triggerの中にあるものの判定(くだモンのみに判定)
        if (other.name == lemon_name ||
            other.name == apumon_name ||
            other.name == momon_name)
        {
            //くだモンの位置(座標)を取得
            var kudamon_pos = other.gameObject.GetComponent<Transform>().position;

            //くだモンの位置を計算で移動
            kudamon_pos =
                Vector3.MoveTowards(transform.position, tubo_pos, 5.0f);
        }
    }

    //吸い取り機能の終了判定
    void OnTriggerExit(Collider other)
    {
        //otherのRigidBody取得
        var Rigid = other.gameObject.GetComponent<Rigidbody>();

        //動かしたRigidBodyの要素を元に戻す
        Rigid.drag = 0;
    }
}

//プログラム変更予定
//before ： OnTrigeerEnter(動きの抑制) -> 
//          OnTrigeerMove(直進で鍋に入る) -> 
//          OnTrigeerExit(RigidBody初期化)

//after  ： OnTrigeerEnter( 動きの抑制 , is_active_move = true ) -> 
//          Update( if(is_active_move){回転しながら鍋に入る(竜巻式)} ) -> 
//          OnCollisionEnter( RigidBody初期化 , is_active_move = false )


