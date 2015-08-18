using UnityEngine;
using System.Collections;

public class LidControl : MonoBehaviour
{

    Collider lid_collider_;
    Renderer lid_renderer_;

    public bool can_rendering_lid_ { get; set; }

    //------------------------------------------------

    void Awake()
    {
        lid_collider_ = GetComponent<Collider>();
        lid_renderer_ = GetComponent<Renderer>();
    }

    void Start()
    {
        can_rendering_lid_ = true;

        lid_collider_.isTrigger = false;
        lid_renderer_.enabled = true;
    }

    void Update()
    {
        //蓋の開け閉め
        lid_collider_.isTrigger = (can_rendering_lid_) ? false : true;
        lid_renderer_.enabled = (can_rendering_lid_) ? true : false;
    }

}
