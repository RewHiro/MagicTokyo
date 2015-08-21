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
    const string POD_NAME = "tubo_kai";



    [SerializeField, Range(1.0f, 5.0f), Tooltip("下に吸い込む力 (弱) <---> (強) ")]
    float down_force_ = 2.0f;

    //-----------------------------------------------------------------



    //吸い取り機能の最中
    void OnTriggerStay(Collider other)
    {
        if (!(other.name == LEMON_NAME || other.name == APUMON_NAME || other.name == MOMON_NAME)) return;


        var fruit_rigidbody = other.gameObject.GetComponent<Rigidbody>();
        var direction = gameObject.transform.position - other.transform.position;
        direction = new Vector3(direction.x, 0, direction.z);
        
        fruit_rigidbody.AddForce(direction.normalized * 10);
        fruit_rigidbody.AddForce(new Vector3(0, -down_force_, 0));
    }
}