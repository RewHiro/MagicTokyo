using UnityEngine;
using System.Collections;


//RigidBodyのみを変えて吸い込みを演出var
public class Suck : MonoBehaviour
{
    [SerializeField, Range(0.0f, 1.0f), Tooltip("吸い取る力 (弱)<--->(強) ")]
    float suck_pow_y = 0.5f;

    [SerializeField, Range(100, 10000), Tooltip("変更する質量の大きさ (軽)<--->(重) ")]
    float rigid_mass = 100;
    [SerializeField, Range(0, 2), Tooltip("変更する空気抵抗の大きさ (小)<--->(大) ")]
    float rigid_drag = 1;

    //くだモンの名前
    const string lemon_name = "re-mon";
    const string apumon_name = "apumon";
    const string momon_name = "momon";

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
        //otherのRigidBody取得
        var Rigid = other.gameObject.GetComponent<Rigidbody>();

        //質量を増やして落とすことで鍋に入れ易くする
        Rigid.mass += rigid_mass;
    }

    //吸い取り機能の終了判定
    void OnTriggerExit(Collider other)
    {
        //otherのRigidBody取得
        var Rigid = other.gameObject.GetComponent<Rigidbody>();

        //動かしたRigidBodyの要素を元に戻す
        Rigid.mass = 1;
        Rigid.drag = 0;
    }
    
}
