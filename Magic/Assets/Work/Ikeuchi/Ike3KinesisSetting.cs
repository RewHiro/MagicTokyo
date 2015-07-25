using UnityEngine;
using System.Collections;

public class Ike3KinesisSetting : MonoBehaviour {

    [SerializeField, Range(0, 1000), TooltipAttribute("浮かせる個数")]
    private int FLOAT_NUM_ = 20;
    public int FloatNum { get { return FLOAT_NUM_; } }

	[SerializeField,Range(0,600),TooltipAttribute("浮かせる時間(秒)")]
	private float FLOAT_SECOND_ = 5.0f;
    public float FloatSecond { get { return FLOAT_SECOND_; } }
	
	[SerializeField,Range(0,1),TooltipAttribute("鍋に飛ばす速度")]
	private float SHOOT_SPEED_ = 0.03f;
    public float ShootSpeed { get { return SHOOT_SPEED_; } }

    [SerializeField, TooltipAttribute("ツボの「prefab」を「Hierarchy」から入れてください")]
    private GameObject tubo_obj_;
    public GameObject TuboObj { get { return tubo_obj_; } }
}
