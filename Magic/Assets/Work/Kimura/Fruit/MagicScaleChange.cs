using UnityEngine;
using System.Collections;

public class MagicScaleChange : MonoBehaviour
{
    float kudamon_scale_max_;
    float kudamon_scale_min_;
    float small_time_;
    float big_time_;

    void Awake()
    {
        small_time_ = 1.0f;
        big_time_ = 1.0f;

        kudamon_scale_max_ = 1.0f;
        kudamon_scale_min_ = 0.5f;
    }

    public void ScaleChange(bool ismagic_,float kudamon_scale_min_,float scale_change_time_)
    {

        if (ismagic_ == true)
        {
            small_time_ = scale_change_time_;

            iTween.ScaleTo(gameObject, iTween.Hash(
                "x", kudamon_scale_min_,
                "y", kudamon_scale_min_,
                "z", kudamon_scale_min_,
                "time", small_time_,
                "easetype", iTween.EaseType.easeOutQuad));
        }
        if (ismagic_ == false)
        {
            big_time_ = scale_change_time_;
            iTween.ScaleTo(gameObject, iTween.Hash(
                "x", kudamon_scale_max_,
                "y", kudamon_scale_max_,
                "z", kudamon_scale_max_,
                "time", big_time_,
                "easetype", iTween.EaseType.easeOutQuad));
            //easeOutBounce
        }
    }
}
