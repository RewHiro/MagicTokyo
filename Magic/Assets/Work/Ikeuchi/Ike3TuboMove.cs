using UnityEngine;
using System.Collections;

public class Ike3TuboMove : MonoBehaviour {

	[SerializeField,Range(0,10)
    ,TooltipAttribute("横移動させる距離(3.0でちょうど壁にあたる)")]
	float range = 2.0f;

	[SerializeField,Range(0,1)
    ,TooltipAttribute("移動の速さ")]
	float speed = 0.1f;

	float count = 0;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		count += speed;
		transform.position = new Vector3 (Mathf.Cos(count) * range, transform.position.y, transform.position.z);
	}
}
