using UnityEngine;
using System.Collections;

public class Smoke : MonoBehaviour
{

    int smoke_time_ = 0;

    [SerializeField]
    float smoke_speed_ = 0.3f;

    [SerializeField]
    float alpha_speed_ = 0.05f;

    SpriteRenderer smoke_sprite_renderer_;
    float alpha = 1.0f;

    //--------------------------------------------------------

    void Start()
    {
        smoke_sprite_renderer_ = GetComponent<SpriteRenderer>();

        transform.localPosition = new Vector3(0, 0, 0);
        transform.Rotate(15, 90, 0);
    }

    void Update()
    {
        smoke_time_++;
        transform.Translate(0, smoke_speed_, 0);

        alpha -= alpha_speed_;
        smoke_sprite_renderer_.color = new Color(1, 1, 1, alpha);

        if (smoke_time_ > 60)
        {
            Destroy(gameObject);
            smoke_time_ = 0;
            alpha = 1.0f;
        }
    }

}
