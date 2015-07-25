using UnityEngine;
using System.Collections;

public class PeachChange : MonoBehaviour
{

    JyamamonDestroy[] jyamamondestroy_;
    FruitCreater fruit_creater_;

    [SerializeField]
    ParticleSystem particleSystem = null;

    ParticleSystem game_object;

    SmokeEffectDestroy[] smoke_effect_destroy_;

    int creater_num_;
    JyamamonChangePos[] change_numder_;

    void Awake()
    {
        fruit_creater_ = GameObject.Find("FruitManager").GetComponent<FruitCreater>();

        creater_num_ = 1;
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

        if (Input.GetKeyDown(KeyCode.H))
        {
            change_numder_ = GetComponentsInChildren<JyamamonChangePos>();
            jyamamondestroy_ = GetComponentsInChildren<JyamamonDestroy>();
 
            for (int i = 0; i < change_numder_.Length; ++i)
            {
                change_numder_[i].JyamamonPos();

                GameObject particle_manager_ = GameObject.Find("Ike3ParticleManager");
                game_object = Instantiate(particleSystem);
                game_object.transform.SetParent(particle_manager_.transform);
                game_object.transform.position = change_numder_[i].transform.position;
                game_object.name = particleSystem.name;

                fruit_creater_.PeachCreate(creater_num_);
                var momon_pos_ = GameObject.Find("FruitManager/PeachManager").GetComponentsInChildren<MagicScaleChange>();

                momon_pos_[i].transform.position = change_numder_[i].transform.position;
                smoke_effect_destroy_ = GetComponentsInChildren<SmokeEffectDestroy>();

                jyamamondestroy_[i].Destroy(true);

                smoke_effect_destroy_[i].SmokeDestroy(true);
            }
        }
    }

    public void PeachChangeStart()
    {
        change_numder_ = GetComponentsInChildren<JyamamonChangePos>();
        jyamamondestroy_ = GetComponentsInChildren<JyamamonDestroy>();

        for (int i = 0; i < change_numder_.Length; ++i)
        {
            change_numder_[i].JyamamonPos();

            GameObject particle_manager_ = GameObject.Find("Ike3ParticleManager");
            game_object = Instantiate(particleSystem);
            game_object.transform.SetParent(particle_manager_.transform);
            game_object.transform.position = change_numder_[i].transform.position;
            game_object.name = particleSystem.name;

            fruit_creater_.PeachCreate(creater_num_);
            var momon_pos_ = GameObject.Find("PeachManager").GetComponentsInChildren<MagicScaleChange>();

            momon_pos_[i].transform.position = change_numder_[i].transform.position;
            smoke_effect_destroy_ = particle_manager_.GetComponentsInChildren<SmokeEffectDestroy>();

            jyamamondestroy_[i].Destroy(true);

            smoke_effect_destroy_[i].SmokeDestroy(true);
        }
    }
}
