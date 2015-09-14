using UnityEngine;
using System.Collections;

public class Smoke : MonoBehaviour
{
    int smoke_time_ = 0;

    [SerializeField]
    float smoke_speed_ = 0.5f;

    [SerializeField]
    float smoke_speed_easing_ = 0.1f;

    [SerializeField]
    float alpha_speed_ = 0.05f;

    SpriteRenderer smoke_sprite_renderer_;
    float alpha = 0.7f;

    TuboInDestroy pot_in_get_ = null;

    //--------------------------------------------------------

    void Start()
    {
        pot_in_get_ = FindObjectOfType<TuboInDestroy>();
        smoke_sprite_renderer_ = GetComponent<SpriteRenderer>();

        transform.localPosition = new Vector3(0, 0, 0);
        transform.Rotate(15, 90, 0);

        smoke_speed_ = 0.5f;
        smoke_time_ = 0;
        alpha = 0.7f;
    }

    void Update()
    {
        smoke_time_++;

        //色変更
        if (pot_in_get_.IsInLemon_)
            smoke_sprite_renderer_.color = new Color(1.0f, 1.0f, 0.0f, alpha);
        else if (pot_in_get_.IsInApple_)
            smoke_sprite_renderer_.color = new Color(1.0f, 0.0f, 0.0f, alpha);
        else if (pot_in_get_.IsInPeach_)
            smoke_sprite_renderer_.color = new Color(1.0f, 0.6f, 1.0f, alpha);
        else if (pot_in_get_.IsInJamamon_)
            smoke_sprite_renderer_.color = new Color(1.0f, 0.0f, 1.0f, alpha);
        else if (pot_in_get_.IsInDorain)
            smoke_sprite_renderer_.color = new Color(1.0f, 0.6f, 0.0f, alpha);

        alpha -= alpha_speed_;

        //速度変更
        transform.Translate(0, smoke_speed_, 0);
        smoke_speed_ -= smoke_speed_easing_;

        //煙削除
        if (smoke_time_ > 10)
        {
            Destroy(gameObject);

            pot_in_get_.IsInLemon_ = false;
            pot_in_get_.IsInApple_ = false;
            pot_in_get_.IsInPeach_ = false;
            pot_in_get_.IsInJamamon_ = false;
            pot_in_get_.is_in_dorian_ = false;
        }
    }

}
