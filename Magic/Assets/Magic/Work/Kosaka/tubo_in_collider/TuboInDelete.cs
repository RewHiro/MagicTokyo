using UnityEngine;
using System.Collections;

public class TuboInDelete : MonoBehaviour
{

    public void OnTriggerEnter(Collider other)
    {

        if (other.name == "Lemon")
        {
            Debug.Log(" Lemon_in ");
            Destroy(GameObject.Find("Lemon"));
        }

        if (other.name== "Apple")
        {
            Debug.Log(" Apple_in ");
            Destroy(GameObject.Find("Apple"));
        }

        if (other.name == "Peach")
        {
            Debug.Log(" Peach_in ");
            Destroy(GameObject.Find("Peach"));
        }

    }
}
