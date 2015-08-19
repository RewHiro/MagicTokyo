using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class FadeIn : MonoBehaviour {

    private Image image_;
    
    private float alpfa_;
    private Color color_;

	// Use this for initialization

    void Awake()  {

        color_ = new Color(1, 1, 1, 0);

        image_ = GetComponent<Image>();
        image_.color = color_;

    }
	void Start () {

      

    }

    // Update is called once per frame
    void Update () {

        if (alpfa_ < 255)
        {
            alpfa_ += 1 * Time.deltaTime;
            color_.a = alpfa_;
            image_.color = color_;
        }
	}
}
