using UnityEngine;
using System.Collections;


//RigidBodyと計算で吸い込みを演出var
public class SuckAdd : MonoBehaviour
{

    //くだモンの名前
    const string LEMON_NAME = "re-mon";
    const string APUMON_NAME = "apumon";
    const string MOMON_NAME = "momon";

    //渦の原点
    Vector3 gravity_center_pos_;
    //吸い込まれるもの
    //pos
    Vector3 kudamon_pos_;
    //rigid
    Rigidbody kudmaon_rigid_;

    //吸い込む計算を始める瞬間の取得
    bool is_cyclone_ = false;

    [SerializeField, Range(50, 300), Tooltip("吸い込む力 (弱) <---> (強) ")]
    int suck_power_ = 100;
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
            //otherのposition取得
            kudamon_pos_ = other.transform.position;

            //otherのRigidBody取得
            kudmaon_rigid_ = other.gameObject.GetComponent<Rigidbody>();

            kudmaon_rigid_.drag = 2;
            kudmaon_rigid_.mass = 10;

            is_cyclone_ = true;
        }
    }

    //吸い取り機能の最中
    public void FixedUpdate()
    {
        if (is_cyclone_)
        {
            kudmaon_rigid_.AddRelativeForce(0.0f, 0.1f, 0.0f);
            kudmaon_rigid_.AddForce(transform.forward * 5);
            kudmaon_rigid_.AddForce(new Vector3(0, -5, 0));

            var vectors_ = gravity_center_pos_ - kudamon_pos_;
            vectors_.Normalize();
            kudmaon_rigid_.AddForce(vectors_ * suck_power_);

            if (kudamon_pos_ == gravity_center_pos_)
            {
                is_cyclone_ = false;
            }
        }
    }

    //吸い取り機能の終了判定
    void OnTriggerExit(Collider other)
    {
        is_cyclone_ = false;
    }
}


