using UnityEngine;
using System.Collections;


//RigidBodyのみを変えて吸い込みを演出var
public class Suck : MonoBehaviour
{
    [SerializeField, Range(100, 10000), Tooltip("変更する質量の大きさ (軽)<--->(重) ")]
    float RIGID_MASS = 100;
    [SerializeField, Range(0, 2), Tooltip("変更する空気抵抗の大きさ (小)<--->(大) ")]
    float RIGID_DRAG = 1;

    //くだモンの名前
    const string LEMON_NAME = "re-mon";
    const string APUMON_NAME = "apumon";
    const string MOMON_NAME = "momon";

    //-----------------------------------------------------------------

    //吸い取り機能の開始判定
    void OnTriggerEnter(Collider other)
    {
        //当たった時の出力デバッグ
        if (other.name == LEMON_NAME ||
            other.name == APUMON_NAME ||
            other.name == MOMON_NAME)
        {
            //otherのRigidBody取得
            var rigid = other.gameObject.GetComponent<Rigidbody>();

            //空気抵抗を付けて動きを抑制
            rigid.drag = RIGID_DRAG;

            Debug.Log(" Rigid Trriger Collision");
        }
    }

    //吸い取り機能の最中
    void OnTriggerStay(Collider other)
    {
        //otherのRigidBody取得
        var rigid = other.gameObject.GetComponent<Rigidbody>();

        //質量を増やして落とすことで鍋に入れ易くする
        rigid.mass += RIGID_MASS;
    }

    //吸い取り機能の終了判定
    void OnTriggerExit(Collider other)
    {
        //otherのRigidBody取得
        var rigid = other.gameObject.GetComponent<Rigidbody>();

        //動かしたRigidBodyの要素を元に戻す
        rigid.mass = 1;
        rigid.drag = 0;
    }
    
}
