using UnityEngine;
using System.Collections;

public class TitleCharacterDirector : MonoBehaviour
{

    float time_ = 0.0f;

    [SerializeField]
    float ANGLE = 360;

    [SerializeField]
    bool is_delay_ = false;

    // Use this for initialization
    void Start()
    {
        iTween.PunchRotation(
            gameObject,
            iTween.Hash(
                "amount", Vector3.up * ANGLE,
                "time", 5.0f
                ));



        //iTween.MoveTo(
        //    gameObject, 
        //    iTween.Hash(
        //        "y", 80, 
        //    "time", 2.0f, 
        //    "easetype", iTween.EaseType.easeInBounce,
        //    "looptype", iTween.LoopType.pingPong,
        //    "islocal",true));
        //iTween.ShakePosition(gameObject, Vector3.right * 1, 5.0f);
    }


    void Update()
    {
        time_ += Time.deltaTime;


        if (is_delay_)
        {
            if (time_ >= 10.0f)
            {
                is_delay_ = false;
                time_ = 0.0f;
            }
        }
        else
        {

            if (time_ >= 5.0f)
            {
                iTween.PunchRotation(
        gameObject,
        iTween.Hash(
            "amount", Vector3.up * 360,
            "time", 5.0f
            ));
                time_ = 0.0f;
            }
        }
    }
}
