using UnityEngine;
using System.Collections;

public class Ike3roulette : MonoBehaviour {
	
	[SerializeField]
	float count_speed = 0.1f;
	[SerializeField]
	float count_time = 1.0f;

	float counter = 0;
	int timer = 0;
	int roulette_num = 0;
	bool roulette_flag = false;

	[SerializeField]
	//Texture[] image = new Texture[5];
	Material[] image = new Material[5];

	// Use this for initialization
	void Start () {
		count_time = count_time * 60;	// 秒数に戻す
		Debug.Log (count_time);
	}
	
	// Update is called once per frame
	void Update () {
		//if (Input.GetKeyDown (KeyCode.E)) {
		//	Debug.Log("homo");
		//	roulette_flag = true;
		//}

		if (roulette_flag) {
			timer++;
			counter += count_speed;
			roulette_num = (int)((60 * counter) % (image.Length));
			Debug.Log (counter);
			if(timer > count_time){
				roulette_flag = false;
				counter = 0;
				timer = 0;
				roulette_num = Random.Range(0,image.Length - 1);
			}
			GetComponent<Renderer>().material = image[roulette_num];
		}
	}

	public void RouletteOn(){
		roulette_flag = true;
	}
}
