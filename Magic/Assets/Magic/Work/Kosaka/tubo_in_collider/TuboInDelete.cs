using UnityEngine;
using System.Collections;

public class TuboInDelete : MonoBehaviour
{

    public void OnTriggerEnter(Collider other)
    {

        if (other.name == "Lemon")
        {
            Debug.Log(" Lemon_in ");
            Destroy(other);
        }

        if (other.name== "Apple")
        {
            Debug.Log(" Apple_in ");
            Destroy(other);
        }

        if (other.name == "Peach")
        {
            Debug.Log(" Peach_in ");
            Destroy(other);
        }

    }
}
