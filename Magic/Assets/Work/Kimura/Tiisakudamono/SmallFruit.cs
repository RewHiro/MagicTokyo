using UnityEngine;
using System.Collections;



public class SmallFruit : MonoBehaviour
{

    enum IsMagic
    {
        UNUSED_MAGIC,
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

    void Awake()
    {
        elapsed_time_ = MAGIC_TIME;
        ismagic_ = IsMagic.UNUSED_MAGIC;
        start_create_max_ = 100;
        hand_controller_ = GameObject.Find("LeapHandController").GetComponent<HandController>();
        magic_scale_change_ = GetComponentsInChildren<MagicScaleChange>();
        if (SCALE_CHANGE_TIME >= MAGIC_TIME)
        {
            SCALE_CHANGE_TIME = MAGIC_TIME / 2;
        }
    }

    void Update()
    {
        SmallChangeMagic();
    }

    void SmallChangeMagic()
    {

        switch (ismagic_)
        {

            case IsMagic.UNUSED_MAGIC:
                {
                    if (Input.GetKey(KeyCode.A))
                    {

                        ismagic_ = IsMagic.MAGIC_START;
                    }
                }
                break;

            case IsMagic.MAGIC_START:
                {
                    magic_scale_change_ = GetComponentsInChildren<MagicScaleChange>();

                    elapsed_time_ -= Time.deltaTime;
                    for (int i = 0; i < magic_scale_change_.Length; ++i)
                    {

                        magic_scale_change_[i].ScaleChange(true, SCALE_MIN, SCALE_CHANGE_TIME);
                    }
                    Debug.Log(elapsed_time_);
                    if (elapsed_time_ <= SCALE_CHANGE_TIME)
                    {
                        ismagic_ = IsMagic.MAGIC_END;
                    }
                }
                break;

            case IsMagic.MAGIC_END:
                {
                    magic_scale_change_ = GetComponentsInChildren<MagicScaleChange>();

                    for (int i = 0; i < magic_scale_change_.Length; ++i)
                    {

                        magic_scale_change_[i].ScaleChange(false, SCALE_MIN, SCALE_CHANGE_TIME);
                    }
                    ismagic_ = IsMagic.UNUSED_MAGIC;
                    elapsed_time_ = MAGIC_TIME;
                }
                break;
        }
    }

    public void SmallFruitStart()
    {
        ismagic_ = IsMagic.MAGIC_START;
    }

}
