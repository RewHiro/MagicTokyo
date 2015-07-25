using UnityEngine;
using System.Collections;

public class JyamamonChangePos : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

        //JyamamonPos();
        //Debug.Log(JyamamonPos());
	}

   public Vector3 JyamamonPos ()
    {
        Vector3 pos = transform.position; 

        return pos;
    }

}
