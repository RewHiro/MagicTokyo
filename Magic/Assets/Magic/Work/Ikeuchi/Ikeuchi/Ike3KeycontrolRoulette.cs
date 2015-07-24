using UnityEngine;
using System.Collections;

public class Ike3KeycontrolRoulette : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown(KeyCode.E)) {
			Debug.Log(0);
			var roulette = GameObject.Find("roulette").GetComponent<Ike3roulette>();
			roulette.RouletteOn();
		}
	}
}
