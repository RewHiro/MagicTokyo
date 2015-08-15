using UnityEngine;
using System.Collections;
using System.Collections.Generic;


//吸い込まれるもの
//pos / rigid / 吸い込む計算を始める瞬間の取得
struct Kudamon
{
    public Vector3 pos_;
    public Rigidbody rigid_;
    public bool is_cyclone_;
}


//RigidBodyと計算で吸い込みを演出var
public class SuckAdd : MonoBehaviour
{

    [SerializeField, Range(0, 2), Tooltip("変更する空気抵抗の大きさ (小)<--->(大) ")]
    float RIGID_DRAG = 1;

    //くだモンの名前
    const string LEMON_NAME = "re-mon";
    const string APUMON_NAME = "apumon";
    const string MOMON_NAME = "momon";
    const string POD_NAME = "tubo_kai";

    //渦の原点
    Vector3 gravity_center_pos_;

    //くだモン
    List<Kudamon> kudamons = new List<Kudamon>();

    [SerializeField, Range(50, 300), Tooltip("吸い込む力 (弱) <---> (強) ")]
    int suck_power_ = 100;
    [SerializeField, Range(50, 300), Tooltip("下に吸い込む力 (弱) <---> (強) ")]
    int down_force_ = 100;

    //-----------------------------------------------------------------

    void Awake()
    {
        gravity_center_pos_ = transform.position;
    }

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
        {
            //動かすくだモンのリストに追加
            var kudamons_init = new Kudamon();
            kudamons_init.pos_ = other.transform.position;
            kudamons_init.rigid_ = other.gameObject.GetComponent<Rigidbody>();
            kudamons_init.is_cyclone_ = false;
            kudamons.Add(kudamons_init);

            for (var i = 0; i < kudamons.Count; ++i)
            {
                //空気抵抗を加えて動きを抑制する
                kudamons[i].rigid_.drag = 2;
                //質量を増やす
                kudamons[i].rigid_.mass = 10;

                //くだモンが渦に巻き込まれる状態に変更
                Kudamon tmpDate = kudamons[i];
                tmpDate.is_cyclone_ = true;
                kudamons[i] = tmpDate;
            }
        }
    }

    //吸い取り機能の最中
    void OnTriggerStay(Collider other)
    {
        for (var i = 0; i < kudamons.Count; ++i)
        {
            if (kudamons[i].is_cyclone_)
            {
                //回転させてその方向に応じて移動する(渦の再現？)
                kudamons[i].rigid_.AddRelativeForce(0.0f, 0.1f, 0.0f);
                kudamons[i].rigid_.AddForce(transform.forward * 5);

                //入りやすくする為の重力
                kudamons[i].rigid_.AddForce(new Vector3(0, -down_force_, 0));

                //中心に向けて動かす
                var vectors_ = gravity_center_pos_ - kudamons[i].pos_;
                vectors_.Normalize();
                kudamons[i].rigid_.AddForce(vectors_ * suck_power_);

                //中心点まで来たら渦に巻き込まれてる状態解除
                if (kudamons[i].pos_ == gravity_center_pos_)
                {
                    kudamons[i].rigid_.drag = 0;
                    kudamons[i].rigid_.mass = 1;

                    Kudamon tmpDate = kudamons[i];
                    tmpDate.is_cyclone_ = false;
                    kudamons[i] = tmpDate;

                    kudamons.Remove(kudamons[i]);
                }
            }
        }
    }

    //吸い取り機能の終了判定
    void OnTriggerExit(Collider other)
    {
        //Colliderから外れたら渦に巻き込まれてる状態解除
        for (var i = 0; i < kudamons.Count; ++i)
        {
            kudamons[i].rigid_.drag = 0;
            kudamons[i].rigid_.mass = 1;

            Kudamon tmpDate = kudamons[i];
            tmpDate.is_cyclone_ = false;
            kudamons[i] = tmpDate;

            kudamons.Remove(kudamons[i]);
        }
    }
}