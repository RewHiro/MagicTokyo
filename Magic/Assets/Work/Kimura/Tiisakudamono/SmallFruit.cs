using UnityEngine;
using System.Collections;
using UnityEngine.Networking;


public class SmallFruit : NetworkBehaviour
{

    bool is_attack_ = false;
    public bool IsAttack { get { return is_attack_; } }

    enum IsMagic
    {
        UNUSED_MAGIC,
        EffECT_START,
        MAGIC_START,
        MAGIC_END,
    }

    IsMagic ismagic_;

    HandController hand_controller_ = null;


    MagicScaleChange[] magic_scale_change_;

    [SerializeField, Range(0.1f, 1.0f), TooltipAttribute("くだモンのどれだけ小さくなるかを入れてください")]
    float SCALE_MIN = 0.5f;

    [SerializeField, Range(1, 100), TooltipAttribute("魔法の全体時間　\n※最低でもSCALE_CHANGE_TIMEの２倍以上の値にしてください")]
    float MAGIC_TIME = 5.0f;

    [SerializeField, Range(1, 100), TooltipAttribute("大きさが変化する時間 \n※MAGIC_TIMEの値を超えていた場合、MAGIC_TIMEの２分の１になる")]
    float SCALE_CHANGE_TIME = 1.0f;

    float elapsed_time_;

    int start_create_max_;

    float effect_time_;

    ParticleSystem magic_start_effect;

    ParticleSystem explosion_effecct;

    [SerializeField, TooltipAttribute("魔法が始まるパーティクルを入れてください")]
    ParticleSystem magic_start_effect_particle_;

    [SerializeField, TooltipAttribute("魔法が始まるパーティクルを入れてください")]
    ParticleSystem explosion_effect_particle_;

    SmokeEffectDestroy[] smoke_effect_destroy_;

    GameObject fruit_manager_;

    void Start()
    {
        if (!isLocalPlayer) return;

        fruit_manager_ = FindObjectOfType<FruitCreater>().gameObject;

        effect_time_ = 2.0f;
        elapsed_time_ = MAGIC_TIME;
        ismagic_ = IsMagic.UNUSED_MAGIC;
        start_create_max_ = 100;
        hand_controller_ = GameObject.Find("LeapHandController").GetComponent<HandController>();
        magic_scale_change_ = fruit_manager_.GetComponentsInChildren<MagicScaleChange>();
        if (SCALE_CHANGE_TIME >= MAGIC_TIME)
        {
            SCALE_CHANGE_TIME = MAGIC_TIME / 2;
        }
    }

    void Update()
    {
        if (!isLocalPlayer) return;
        SmallChangeMagic();
     
    }

    void SmallChangeMagic()
    {

        switch (ismagic_)
        {

            case IsMagic.UNUSED_MAGIC:
                {
                    if (Input.GetKeyDown(KeyCode.A))
                    {

                        ismagic_ = IsMagic.EffECT_START;

                    }

                    if(ismagic_ == IsMagic.EffECT_START)
                    {
                        GameObject particle_manager_ = GameObject.Find("Ike3ParticleManager");
                        explosion_effecct = Instantiate(explosion_effect_particle_);
                        explosion_effecct.transform.SetParent(particle_manager_.transform);
                        explosion_effecct.name = explosion_effect_particle_.name;

                    }
                }
                break;

            case IsMagic.EffECT_START:
                {
                    if (effect_time_ >= 2.0f)
                    {
                        GameObject particle_manager_ = GameObject.Find("Ike3ParticleManager");
                        explosion_effecct = Instantiate(explosion_effect_particle_);
                        explosion_effecct.transform.SetParent(particle_manager_.transform);
                        explosion_effecct.name = explosion_effect_particle_.name;
                    }
                   Vector3 fall = explosion_effecct.transform.position;
                    if (fall.y >= 4.5f)
                    {
                        fall.y -= 0.08f;
                        explosion_effecct.transform.position = fall;
                    }

                    effect_time_ -= Time.deltaTime;
                    if (effect_time_ <= 0)
                    {
                        
                        magic_scale_change_ = fruit_manager_.GetComponentsInChildren<MagicScaleChange>();
                        for (int i = 0; i < magic_scale_change_.Length; ++i)
                        {

                            GameObject particle_manager_ = GameObject.Find("Ike3ParticleManager");
                            magic_start_effect = Instantiate(magic_start_effect_particle_);
                            magic_start_effect.transform.SetParent(particle_manager_.transform);
                            magic_start_effect.transform.position = magic_scale_change_[i].transform.position;
                            magic_start_effect.name = magic_start_effect_particle_.name;
                            smoke_effect_destroy_ = particle_manager_.GetComponentsInChildren<SmokeEffectDestroy>();
                            magic_scale_change_[i].ScaleChange(true, SCALE_MIN, SCALE_CHANGE_TIME);
                            smoke_effect_destroy_[i].SmokeDestroy(true);
                        }
                        ismagic_ = IsMagic.MAGIC_START;
                    }

                }
                break;

            case IsMagic.MAGIC_START:
                {
                    

                    elapsed_time_ -= Time.deltaTime;

                    Debug.Log(elapsed_time_);
                    if (elapsed_time_ <= SCALE_CHANGE_TIME)
                    {
                        ismagic_ = IsMagic.MAGIC_END;
                    }
                }
                break;

            case IsMagic.MAGIC_END:
                {
                    magic_scale_change_ = fruit_manager_.GetComponentsInChildren<MagicScaleChange>();

                    for (int i = 0; i < magic_scale_change_.Length; ++i)
                    {

                        magic_scale_change_[i].ScaleChange(false, SCALE_MIN, SCALE_CHANGE_TIME);
                        
                    }
                    ismagic_ = IsMagic.UNUSED_MAGIC;
                    elapsed_time_ = MAGIC_TIME;
                    effect_time_ = 2.0f;
                    GameObject effect_destroy = GameObject.Find("FireBall");
                    
                    Destroy(effect_destroy);

                }
                break;
        }
    }

    [Command]
    void CmdTellServerAttack()
    {
        is_attack_ = true;
    }

    [ClientRpc]
    public void RpcTellClientSmallFruitStart()
    {
        ismagic_ = IsMagic.EffECT_START;
    }

    [ClientRpc]
    public void RpcTellClientStopAttack()
    {
        is_attack_ = false;
    }

    public void SmallFruitStart()
    {
        CmdTellServerAttack();
        is_attack_ = true;
    }

}
