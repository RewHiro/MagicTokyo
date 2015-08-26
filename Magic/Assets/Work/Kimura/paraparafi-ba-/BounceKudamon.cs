using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class BounceKudamon : MonoBehaviour
{
    enum IsMagic
    {
        UNUSED_MAGIC,
        MAGIC_START,
        MAGIC_END,
    }

    IsMagic is_magic_;

    Text start_text_;

    [SerializeField, Range(1, 60), TooltipAttribute("イベント時間")]
    float EVENT_TIME = 10.0f;

    [SerializeField, Range(0.0f, 1.0f), TooltipAttribute("跳ねる力")]
    float BOUNCE_POWER = 0.9f;

    [SerializeField, Range(0.0f, 1.0f), TooltipAttribute("画面の揺れ具合")]
    float SWAY_POWER = 0.01f;

    [SerializeField, Range(0.0f, 10.0f), TooltipAttribute("画面の揺れる時間")]
    float START_EVENT_TIME = 3.0f;

    [SerializeField, Range(0.0f, 100.0f), TooltipAttribute("テロップの流れる速度")]
    float FLOW_TELOP_SPEED = 50.0f;

    float elapsed_time_;

    float start_time_;

    Quaternion origin_camera_rotation_;

    Vector3 start_text_position_;

    ChangeBounce[] changebounce;
    void Awake()
    {
        elapsed_time_ = EVENT_TIME;
        changebounce = GetComponentsInChildren<ChangeBounce>();
        is_magic_ = IsMagic.UNUSED_MAGIC;
        origin_camera_rotation_ = Camera.main.transform.rotation;
        start_text_ = GameObject.Find("BounceText").GetComponent<Text>();
        start_text_.enabled = false;
        start_time_ = START_EVENT_TIME;
        start_text_position_ = start_text_.rectTransform.localPosition;
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
                    if (Input.GetKeyDown(KeyCode.E))
                    {
                        is_magic_ = IsMagic.MAGIC_START;
                    }



                    if (is_magic_ == IsMagic.MAGIC_START)
                    {
                        start_text_.enabled = true;

                        changebounce = GetComponentsInChildren<ChangeBounce>();

                        for (int i = 0; i < changebounce.Length; ++i)
                        {
                            changebounce[i].Bounce(true, BOUNCE_POWER);
                        }

                    }
                }
                break;

            case IsMagic.MAGIC_START:
                {
                    if (start_time_ >= 3.0f)
                    {
                        AudioManager.Instance.PlaySe(12);
                        start_text_.enabled = true;

                        changebounce = GetComponentsInChildren<ChangeBounce>();

                        for (int i = 0; i < changebounce.Length; ++i)
                        {
                            changebounce[i].Bounce(true, BOUNCE_POWER);
                        }
                    }

                    if (start_time_ >= 0 )
                    {
                        Vector3 flow_telop_ = start_text_.rectTransform.localPosition;
                        flow_telop_.x -= FLOW_TELOP_SPEED;
                        start_text_.rectTransform.localPosition = flow_telop_;
                        start_time_ -= Time.deltaTime;
                        Quaternion main_camera_rotation = Camera.main.transform.rotation;
                        float vibration_x = Random.Range(-SWAY_POWER, SWAY_POWER);
                        float vibration_y = Random.Range(-SWAY_POWER, SWAY_POWER);

                        main_camera_rotation.x = origin_camera_rotation_.x + vibration_x;
                        main_camera_rotation.y = origin_camera_rotation_.y + vibration_y;

                        Camera.main.transform.rotation = main_camera_rotation;

                    }

                    if(elapsed_time_ <= 9.0f)
                    {
                        start_text_.enabled = false;
                        start_text_.rectTransform.localPosition = start_text_position_;
                    }

                    if(start_time_ <= 0)
                    {

                        Camera.main.transform.rotation = origin_camera_rotation_;

                        start_time_ = 0;
                        elapsed_time_ -= Time.deltaTime;

                    }



                    if (elapsed_time_ <= 0)
                    {


                        start_time_ = START_EVENT_TIME;
                        is_magic_ = IsMagic.MAGIC_END;
                    }
                }
                break;

            case IsMagic.MAGIC_END:
                {
                    changebounce = GetComponentsInChildren<ChangeBounce>();


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

    public void Starter()
    {
        is_magic_ = IsMagic.MAGIC_START;
        

    }
}
