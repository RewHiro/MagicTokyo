
using UnityEngine;


public class TitleKudamonArch : MonoBehaviour
{

    void Start()
    {
    }

    void Update()
    {
        GetComponent<Rigidbody>().velocity += new Vector3(0, -100, 0);
        //transform.position += new Vector3(0, -30, 0);
        if(transform.position.y < -1000.0f || transform.position.x > 900)
        {
            Destroy(gameObject);
        }
    }
}
