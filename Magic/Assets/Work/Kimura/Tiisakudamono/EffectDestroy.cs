using UnityEngine;
using System.Collections;

public class EffectDestroy : MonoBehaviour {

    public void EffectDelete(bool ismagic_)
    {

        if (ismagic_ == true)
        {
            Destroy(gameObject);
            ismagic_ = false;

        }

    }
}
