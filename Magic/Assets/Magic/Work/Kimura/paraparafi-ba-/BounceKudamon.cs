using UnityEngine;
using System.Collections;

public class BounceKudamon : MonoBehaviour
{
    enum IsMagic
    {
        UNUSED_MAGIC,
        MAGIC_START,
        MAGIC_END,
    }

    IsMagic is_magic_;

    GameObject moment_decision_;

    [SerializeField, Range(1, 60), TooltipAttribute("イベント時間")]
    float EVENT_TIME = 10.0f;

    [SerializeField, Range(0.0f, 1.0f), TooltipAttribute("跳ねる力")]
    float BOUNCE_POWER = 0.9f;


    float elapsed_time_;

    ChangeBounce[] changebounce;
    void Awake()
    {
        moment_decision_ = GameObject.Find("MomentDecision");
        elapsed_time_ = EVENT_TIME;
        moment_decision_.GetComponent<SphereCollider>().enabled = false;
        changebounce = GetComponentsInChildren<ChangeBounce>();
        is_magic_ = IsMagic.UNUSED_MAGIC;
    }


    void Update()
    {
        BounceSwitching();
    }

    void BounceSwitching()
    {

        switch (is_magic_)
        {

            case IsMagic.UNUSED_MAGIC:
                {
                    if (Input.GetKey(KeyCode.A))
                    {
                        is_magic_ = IsMagic.MAGIC_START;
                        moment_decision_.GetComponent<SphereCollider>().enabled = true;
                    }
                }
                break;

            case IsMagic.MAGIC_START:
                {
                    moment_decision_.GetComponent<SphereCollider>().enabled = false;
                    elapsed_time_ -= Time.deltaTime;
                    for (int i = 0; i < changebounce.Length; ++i)
                    {
                        changebounce[i].Bounce(true,BOUNCE_POWER);
                    }
                    
                    if (elapsed_time_ <= 0)
                    {
                        is_magic_ = IsMagic.MAGIC_END;
                    }
                }
                break;

            case IsMagic.MAGIC_END:
                {
                    for (int i = 0; i < changebounce.Length; ++i)
                    {
                        changebounce[i].Bounce(false, BOUNCE_POWER);
                    }
                    is_magic_ = IsMagic.UNUSED_MAGIC;
                    elapsed_time_ = EVENT_TIME;
                }
                break;
        }

    }
}
