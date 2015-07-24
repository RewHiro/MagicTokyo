using UnityEngine;
using System.Collections;

public class Ike3TyhoonEffectCube : MonoBehaviour {

    private float angle_ = 0.0f;
    private float move_value_ = 0.0f;

	// Use this for initialization
	void Start () {
        angle_ = Random.Range(0.0f, Mathf.PI * 2);

        transform.eulerAngles = new Vector3(
            Random.Range(0.0f, Mathf.PI * 2),
            Random.Range(0.0f, Mathf.PI * 2),
            Random.Range(0.0f, Mathf.PI * 2));
	}
	
	// Update is called once per frame
	void Update () {
        angle_ += 0.5f;
        move_value_ += 0.05f;
        float x = Mathf.Cos(angle_) * move_value_;
        float y = move_value_ / 10;
        float z = Mathf.Sin(angle_) * move_value_;
        transform.position += new Vector3(x, y, z);

        if (move_value_ > 5.0f)
        {
            Destroy(gameObject);
        }
	}
}
