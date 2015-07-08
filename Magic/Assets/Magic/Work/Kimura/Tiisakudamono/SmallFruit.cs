using UnityEngine;
using System.Collections;

public class SmallFruit : MonoBehaviour
{

    HandController hand_controller_ = null;


    MagicScaleChange[] magic_sacale_change;

    [SerializeField,Range(0.1f,1.0f), TooltipAttribute("くだモンのどれだけ小さくなるかを入れてください")]
    float sacale_min;

    int start_create_max;

    void Awake()
    {
        sacale_min = 0.5f;

        start_create_max = 100;

        hand_controller_ = GameObject.Find("LeapHandController").GetComponent<HandController>();

//        for (int i = 0; i < start_create_max; ++i)
//       {
            magic_sacale_change = GetComponentsInChildren<MagicScaleChange>();
//        }
        
    }

    void Update()
    {
        
      IsMagic();
    }

    void IsMagic()
    {
        var current_number = GetComponentsInChildren<MagicScaleChange>();

        if (Input.GetKey(KeyCode.A))
        {

            for (int i = 0; i < current_number.Length; ++i)
            {
                magic_sacale_change[i].ScaleChange(true,sacale_min);
            }
        }

        if (Input.GetKey(KeyCode.S))
        {
            for (int i = 0; i < current_number.Length; ++i)
            {

                magic_sacale_change[i].ScaleChange(false, sacale_min);
            }
        }

    }

}
