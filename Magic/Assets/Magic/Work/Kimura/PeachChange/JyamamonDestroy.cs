using UnityEngine;
using System.Collections;

public class JyamamonDestroy : MonoBehaviour
{

    ParticleSystem particleSystem;

    public void Destroy(bool ismagic_)
    {

        //particleSystem.Stop();

        if (ismagic_ == true)
        {
            GameObject.Destroy(gameObject);
            ismagic_ = false;
            //particleSystem.Play();
        }

    }


}
