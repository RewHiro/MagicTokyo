using UnityEngine;
using System.Collections;


//RigidBodyと計算で吸い込みを演出var
public class SuckAdd : MonoBehaviour {

    [SerializeField, Range(0, 2), Tooltip("変更する空気抵抗の大きさ (小)<--->(大) ")]
    float RIGID_DRAG = 1;

    //移動できるかどうか？
    bool is_move_ = false;

    //くだモンの名前
    const string LEMON_NAME = "re-mon";
    const string APUMON_NAME = "apumon";
    const string MOMON_NAME = "momon";
    const string POD_NAME = "tubo_kai";

    //-----------------------------------------------------------------

    //吸い取り機能の開始判定
    void OnTriggerEnter(Collider other)
    {
        //otherのRigidBody取得
        var rigid = other.gameObject.GetComponent<Rigidbody>();

        //空気抵抗を付けて動きを抑制
        rigid.drag = RIGID_DRAG;

        //当たった時の出力デバッグ
        if (other.name == LEMON_NAME ||
            other.name == APUMON_NAME ||
            other.name == MOMON_NAME)
            Debug.Log(" Rigid Trriger Collision");
    }

    //吸い取り機能の最中
    void OnTriggerStay(Collider other)
    {
        //鍋の位置(座標)を取得
        var tubo_pos = GameObject.Find(POD_NAME).GetComponent<Transform>().position;

        //Triggerの中にあるものの判定(くだモンのみに判定)
        if (other.name == LEMON_NAME ||
            other.name == APUMON_NAME ||
            other.name == MOMON_NAME)
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
        var rigid = other.gameObject.GetComponent<Rigidbody>();

        //動かしたRigidBodyの要素を元に戻す
        rigid.drag = 0;
    }
}


