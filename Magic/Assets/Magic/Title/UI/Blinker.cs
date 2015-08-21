using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Blinker : MonoBehaviour
{

    float alpha_ = 1.0f;
    float theta_ = 0.0f;

    RawImage raw_image_ = null;

    // Use this for initialization
    void Start()
    {
        raw_image_ = GetComponent<RawImage>();
    }

    // Update is called once per frame
    void Update()
    {
        alpha_ = Mathf.Abs(Mathf.Cos(theta_));
        theta_ += Time.deltaTime;
        raw_image_.color = new Color(1, 1, 1, alpha_);
    }
}
