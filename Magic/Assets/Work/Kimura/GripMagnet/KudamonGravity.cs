using UnityEngine;
using System.Collections;

public class KudamonGravity : MonoBehaviour {



   public void IsGravity(bool switching_gravity)
    {
        if (switching_gravity == true)
        {
            GetComponent<Rigidbody>().useGravity = false;
            GetComponent<Rigidbody>().isKinematic = true;
        }
        else
        if (switching_gravity == false)
        {
            GetComponent<Rigidbody>().useGravity = true;
            GetComponent<Rigidbody>().isKinematic = false;
        }
    }
}
