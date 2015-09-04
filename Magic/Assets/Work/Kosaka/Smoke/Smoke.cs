using UnityEngine;
using System.Collections;

public class Smoke : MonoBehaviour
{

    int smoke_time_ = 0;

    [SerializeField]
    float smoke_speed_ = 0.3f;

    Vector3 pot_pos_;

    //--------------------------------------------------------

    void Awake()
    {
        pot_pos_ = new Vector3(0, 0, 0);
    }

    void Start()
    {
        transform.localPosition = new Vector3(0, 0, 0);
        transform.Rotate(15, 90, 0);
    }

    void Update()
    {
        smoke_time_++;
        transform.Translate(0, smoke_speed_, 0);

        if (smoke_time_ > 60)
        {
            Destroy(gameObject);
        }
    }

}
