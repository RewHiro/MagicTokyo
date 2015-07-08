using UnityEngine;
using System.Collections;

public class TuboInDestroy : MonoBehaviour
{

    public void OnTriggerEnter(Collider other)
    {

        if (other.name == "re-mon")
        {
            Debug.Log(" Lemon Destroy ");
            Destroy(other.gameObject);
        }

        if (other.name == "apumon")
        {
            Debug.Log(" Apple Destroy ");
            Destroy(other.gameObject);
        }

        if (other.name == "momon")
        {
            Debug.Log(" Peach Destroy ");
            Destroy(other.gameObject);
        }

    }
}
