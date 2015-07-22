using UnityEngine;
using System.Collections;

public class SmokeEffectDestroy : MonoBehaviour {

    public void SmokeDestroy(bool ismagic_)
    {

        if (ismagic_ == true)
        {
            GameObject.Destroy(gameObject,1.0f);
            ismagic_ = false;

        }

    }
}
