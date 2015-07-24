using UnityEngine;
using System.Collections;

public class Ike3TestHit : MonoBehaviour {

    bool flag = false;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        if (flag)
        {
            GetComponent<SphereCollider>().isTrigger = true;
            flag = false;
        }

        if (Input.GetKeyDown(KeyCode.X))
        {
            GetComponent<SphereCollider>().isTrigger = false;
            flag = true;
        }
	}
}
