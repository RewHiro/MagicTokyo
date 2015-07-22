using UnityEngine;
using System.Collections;

public class JyamamonDestroy : MonoBehaviour
{


    public void Destroy(bool ismagic_)
    {

        if (ismagic_ == true)
        {
            GameObject.Destroy(gameObject);
            ismagic_ = false;

        }

    }


}
