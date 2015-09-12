using UnityEngine;
using System.Collections;

public class LidControl : MonoBehaviour
{
    public bool can_rendering_lid_ { get; set; }

    Vector3 lid_rotate_speed_;

    Vector3 lid_scale_;

    float lid_scale_xy_;

    [SerializeField, Tooltip("蓋サイズの最大値")]
    const float LID_SCALE_MAX_ = 0.7f;

    [SerializeField, Tooltip("拡縮の速度 : (早) <---> (遅)")]
    float SCALE_CHANGE_SPEED_SAVER_ = 30.0f;

    Collider lid_collider;

    RushEventer kudamon_rush_;

    //------------------------------------------------

    public void Awake()
    {
        lid_collider = gameObject.GetComponent<Collider>();
        lid_scale_xy_ = LID_SCALE_MAX_;
    }

    void Start()
    {
        can_rendering_lid_ = true;

        lid_rotate_speed_ = new Vector3(0, 0, 1);

        lid_scale_ = new Vector3(lid_scale_xy_, lid_scale_xy_, 1.0f);
    }

    void Update()
    {
        //蓋のサイズが変わる速度
        var scale_change_speed =
            LID_SCALE_MAX_ / SCALE_CHANGE_SPEED_SAVER_;

        //----------------------------------------------------------------------------
        //ここのTODOができていないので修正おねがい or やり方(ラッシュの開始と終了の取得方法)提示おねがい

        // TODO : ラッシュが始まったら蓋が消え
        //if (kudamon_rush_.IsStart) { can_rendering_lid_ = false; }
        // TODO : ラッシュが終わったら蓋が出る
        //else { can_rendering_lid_ = true; }

        //----------------------------------------------------------------------------

        //蓋の有無が切り替わった時の動き
        //サイズの変更・Triggerの切り替え・回転
        if (can_rendering_lid_)
        {
            if (lid_scale_xy_ <= LID_SCALE_MAX_)
                lid_scale_xy_ += scale_change_speed;

            gameObject.transform.Rotate(lid_rotate_speed_);

            lid_collider.isTrigger = false;
        }
        else
        {
            if (lid_scale_xy_ >= 0.0f)
                lid_scale_xy_ -= scale_change_speed;
            if (lid_scale_xy_ <= 0.0f)
                lid_scale_xy_ = 0.0f;

            gameObject.transform.Rotate(-lid_rotate_speed_);

            lid_collider.isTrigger = true;
        }

        lid_scale_ = new Vector3(lid_scale_xy_, lid_scale_xy_, 1.0f);
        gameObject.transform.localScale = lid_scale_;
    }

}
