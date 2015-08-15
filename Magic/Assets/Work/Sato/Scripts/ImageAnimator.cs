using UnityEngine;
using System.Collections;

public class ImageAnimator : MonoBehaviour
{
    //移動、拡縮アニメーション.
    enum AnimationType : int
    {
        SIN_ANIMA,
        SIN_ABS_ANIMA,
        ZOOMING_ANIMA,
        ROTATO,
    }

    public bool do_stop_animation_ = false;
    [SerializeField]
    private AnimationType animation_type_;
    [SerializeField, Range(-10, 10)]
    private float animation_speed_ = 0.1f;
    [SerializeField, Range(-50, 50)]
    private float animation_range_ = 1.0f;
    [SerializeField]
    private bool is_add_vec_x = false;
    [SerializeField]
    private bool is_add_vec_y = false;
    [SerializeField]
    private bool is_add_vec_z = false;

   

    private float animation_count_;
    private Vector3 sprite_pos_;
    private Vector3 sprite_scale_;
    private int add_x_, add_y_, add_z_;
    private Color sprite_color_;

    void Awake()
    {
        animation_count_ = 0.0f;
        sprite_pos_ = new Vector3(this.transform.localPosition.x, this.transform.localPosition.y, this.transform.localPosition.z);
        sprite_scale_ = new Vector3(this.transform.localScale.x, this.transform.localScale.y, this.transform.localScale.z);
       
        add_x_ = add_y_ = add_z_ = 0;
        if (is_add_vec_x) add_x_ = 1; else add_x_ = 0;
        if (is_add_vec_y) add_y_ = 1; else add_y_ = 0;
        if (is_add_vec_z) add_z_ = 1; else add_z_ = 0;

    }
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //Do animation!
        if (!do_stop_animation_)
        {
            animation_count_ += animation_speed_;
            //デバッグ用.
            if (is_add_vec_x) add_x_ = 1; else add_x_ = 0;
            if (is_add_vec_y) add_y_ = 1; else add_y_ = 0;
            if (is_add_vec_z) add_z_ = 1; else add_z_ = 0;


            //インスペクターでアニメーションを設定.
            switch (animation_type_)
            {
                case AnimationType.SIN_ANIMA:

                    this.transform.localPosition =
                    new Vector3(sprite_pos_.x + add_x_ * ((float)(System.Math.Sin(animation_count_)) * animation_range_),
                                         sprite_pos_.y + add_y_ * ((float)(System.Math.Sin(animation_count_)) * animation_range_),
                                         sprite_pos_.z + add_z_ * ((float)(System.Math.Sin(animation_count_)) * animation_range_));

                    break;
                case AnimationType.SIN_ABS_ANIMA:

                    this.transform.localPosition =
                    new Vector3(sprite_pos_.x + add_x_ * ((float)System.Math.Abs(System.Math.Sin(animation_count_)) * animation_range_),
                                         sprite_pos_.y + add_y_ * ((float)System.Math.Abs(System.Math.Sin(animation_count_)) * animation_range_),
                                         sprite_pos_.z + add_z_ * ((float)System.Math.Abs(System.Math.Sin(animation_count_)) * animation_range_));

                    break;
                case AnimationType.ZOOMING_ANIMA:

                    this.transform.localScale =
                    new Vector3(sprite_scale_.x + add_x_ * ((float)System.Math.Abs(System.Math.Sin(animation_count_)) * animation_range_),
                                         sprite_scale_.y + add_y_ * ((float)System.Math.Abs(System.Math.Sin(animation_count_)) * animation_range_),
                                         sprite_scale_.z + add_z_ * ((float)System.Math.Abs(System.Math.Sin(animation_count_)) * animation_range_));

                    break;
                case AnimationType.ROTATO:

                    transform.Rotate(0, 0, Time.deltaTime * animation_speed_);

                    break;


            }
        }
       
    }
}
