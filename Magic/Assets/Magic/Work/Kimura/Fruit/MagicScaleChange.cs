using UnityEngine;
using System.Collections;

public class MagicScaleChange : MonoBehaviour
{
    float kudamon_scale_max;
    float kudamon_scale_min;

    void Awake()
    {
        kudamon_scale_max = 1.0f;
        kudamon_scale_min = 0.5f;
    }

    public void Update()
    {

    }

    public void ScaleChange(bool ismagic,float kudamon_scale_min)
    {

        if (ismagic == true)
        {
            transform.localScale = new Vector3(kudamon_scale_min, kudamon_scale_min, kudamon_scale_min);
        }
        if (ismagic == false)
        {
            transform.localScale = new Vector3(kudamon_scale_max, kudamon_scale_max, kudamon_scale_max);
        }
    }
}
