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

    //くだモンの名前
    const string LEMON_NAME = "re-mon";
    const string APUMON_NAME = "apumon";
    const string MOMON_NAME = "momon";

    //渦の原点
    Vector3 gravity_center_pos_;

    //くだモン
    List<Kudamon> kudamons = new List<Kudamon>();

    [SerializeField, Range(50, 300), Tooltip("吸い込む力 (弱) <---> (強) ")]
    int suck_power_ = 100;
    [SerializeField, Range(50, 300), Tooltip("下に吸い込む力 (弱) <---> (強) ")]
    int down_force_ = 100;
    //-----------------------------------------------------------------


    public void Awake()
    {
        gravity_center_pos_ = transform.position;
    }

    //吸い取り機能の開始判定
    void OnTriggerEnter(Collider other)
    {
        if (other.name == LEMON_NAME ||
            other.name == APUMON_NAME ||
            other.name == MOMON_NAME)
        {
            var kudamons_init = new Kudamon { };
            kudamons_init.pos_ = other.transform.position;
            kudamons_init.rigid_ = other.gameObject.GetComponent<Rigidbody>();
            kudamons_init.is_cyclone_ = false;
            kudamons.Add(kudamons_init);

            for (var i = 0; i < kudamons.Count; ++i)
            {
                kudamons[i].rigid_.drag = 2;
                kudamons[i].rigid_.mass = 10;

                Kudamon tmpDate = kudamons[i];
                tmpDate.is_cyclone_ = true;
                kudamons[i] = tmpDate;
            }
        }
    }

    //吸い取り機能の最中
    public void FixedUpdate()
    {
        for (var i = 0; i < kudamons.Count; ++i)
        {
            if (kudamons[i].is_cyclone_)
            {
                kudamons[i].rigid_.AddRelativeForce(0.0f, 0.1f, 0.0f);
                kudamons[i].rigid_.AddForce(transform.forward * 5);
                kudamons[i].rigid_.AddForce(new Vector3(0, -down_force_, 0));

                var vectors_ = gravity_center_pos_ - kudamons[i].pos_;
                vectors_.Normalize();
                kudamons[i].rigid_.AddForce(vectors_ * suck_power_);

                if (kudamons[i].pos_ == gravity_center_pos_)
                {
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
        for (var i = 0; i < kudamons.Count; ++i)
        {
            Kudamon tmpDate = kudamons[i];
            tmpDate.is_cyclone_ = false;
            kudamons[i] = tmpDate;

            kudamons.Remove(kudamons[i]);
        }
    }
}


