using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class FruitFallDowner : MonoBehaviour
{

    float time_ = 0.0f;
    float theta_ = 0.0f;
    float alpha_ = 0.0f;

    Image image_ = null;

    // Use this for initialization
    void Start()
    {
        gameObject.transform.localPosition = new Vector3(Random.Range(-800, 800), 490, 0);
        image_ = GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        gameObject.transform.localPosition += Vector3.down * 5.0f;
        gameObject.transform.localRotation *= Quaternion.AngleAxis(3, Vector3.forward);

        if (gameObject.transform.localPosition.y <= -260)
        {
            if (alpha_ <= 0.1f)
            {
                Destroy(gameObject);
            }
            else
            {
                alpha_ = Mathf.Abs(Mathf.Sin(theta_));
                theta_ += Time.deltaTime * 2;
                image_.color = new Color(1, 1, 1, alpha_);
            }
        }
        else
        {
            if (alpha_ <= 0.9f)
            {
                alpha_ = Mathf.Abs(Mathf.Sin(theta_));
                theta_ += Time.deltaTime * 2;
                image_.color = new Color(1, 1, 1, alpha_);
            }
        }
    }
}
