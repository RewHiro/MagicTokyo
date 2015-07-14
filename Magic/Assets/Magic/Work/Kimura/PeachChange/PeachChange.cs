using UnityEngine;
using System.Collections;

public class PeachChange : MonoBehaviour
{

    JyamamonChangePos[] jyamamonChangePos_;
    JyamamonDestroy[] jyamamondestroy_;
    FruitCreater fruit_creater_;

    ParticleSystem particleSystem;


    int creater_num_;
    JyamamonChangePos[] change_numder_;
   
    void Awake()
    {
        fruit_creater_ = GameObject.Find("FruitManager").GetComponent<FruitCreater>();

        creater_num_ = 1;
        //particleSystem.Stop();
    }

    void Start()
    {
        change_numder_ = GetComponentsInChildren<JyamamonChangePos>();
        jyamamondestroy_ = GetComponentsInChildren<JyamamonDestroy>();
    }

    void Update()
    {
        Change();
    }

    void Change()
    {

        if (Input.GetKeyDown(KeyCode.A))
        {
            //particleSystem.Play();
            for (int i = 0; i < change_numder_.Length; ++i)
            {
                change_numder_[i].JyamamonPos();
                
                fruit_creater_.PeachCreate(creater_num_);
                var momon_pos_ = GameObject.Find("FruitManager/PeachManager").GetComponentsInChildren<MagicScaleChange>();

                momon_pos_[i].transform.position = change_numder_[i].transform.position;
                Behaviour halo = (Behaviour)momon_pos_[i].GetComponent("Halo");
                halo.enabled = true;
                //jyamamondestroy_[i].particleSystem.Play();
                jyamamondestroy_[i].Destroy(true);
                
            }
            
            //particleSystem.Play();
        }

    }
}
