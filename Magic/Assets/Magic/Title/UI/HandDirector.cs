using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class HandDirector : MonoBehaviour
{


    // Use this for initialization
    void Start()
    {

        iTween.PunchPosition(gameObject, iTween.Hash("amount", Vector3.forward * 105, "time", 2.0f, "looptype", iTween.LoopType.loop));
    }
}
