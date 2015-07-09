using UnityEngine;
using System.Collections;

public class PeachChange : MonoBehaviour {

    JyamamonChangePos[] jyamamonChangePos;

    void Awake()
    {
        jyamamonChangePos = GetComponentsInChildren<JyamamonChangePos>();
    }

	void Start () 
    {
	
	}
	
	void Update () 
    {
        Change();
        
	
	}

    void Change()
    {
        var change_numder = GetComponentsInChildren<JyamamonChangePos>();

        for(int i = 0; i < change_numder.Length; ++i)
        {
            jyamamonChangePos[i].JyamamonPos();
           // Debug.Log(jyamamonChangePos[i]);
        }

    }
}
