using UnityEngine;
using System.Collections;

public class ChangeBounce : MonoBehaviour
{

    SphereCollider spherecollider;

    bool is_magic_;
    float bounce_power_;

    void Awake()
    {
        is_magic_ = false;
        bounce_power_ = 0.9f;
        spherecollider = GetComponent<SphereCollider>();
    }

    public void Bounce(bool is_magic_, float bounce_power_)
    {
        if (is_magic_ == false)
        {
            spherecollider.material.bounciness = 0.0f;
        }
        else
            if (is_magic_ == true)
            {
                spherecollider.material.bounciness = bounce_power_;
            }
       
    }
}