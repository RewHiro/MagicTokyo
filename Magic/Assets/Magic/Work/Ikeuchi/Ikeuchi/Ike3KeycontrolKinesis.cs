using UnityEngine;
using System.Collections;

public class Ike3KeycontrolKinesis : MonoBehaviour {

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown(KeyCode.S)){
			Debug.Log(0);
			var kinesis = GameObject.Find ("FruitManager").GetComponent<Ike3KudamonKinesisu> ();
			kinesis.KudamonKinesis();
		}

		if(Input.GetKeyDown(KeyCode.A)){
			Debug.Log(0);
			var apple_move = GameObject.Find ("FruitManager/AppleManager").GetComponentsInChildren<Ike3KudamonMove> ();
			var lemon_move = GameObject.Find ("FruitManager/LemonManager").GetComponentsInChildren<Ike3KudamonMove> ();
			var peach_move = GameObject.Find ("FruitManager/PeachManager").GetComponentsInChildren<Ike3KudamonMove> ();
			for(var i = 0; i < apple_move.Length; i++){
				apple_move[i].ShootOn();
			}
			for(var i = 0; i < lemon_move.Length; i++){
				lemon_move[i].ShootOn();
			}
			for(var i = 0; i < peach_move.Length; i++){
				peach_move[i].ShootOn();
			}
		}
	}
}
