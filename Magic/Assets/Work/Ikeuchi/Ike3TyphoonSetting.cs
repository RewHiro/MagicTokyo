using UnityEngine;
using System.Collections;

public class Ike3TyphoonSetting : MonoBehaviour {

    [SerializeField,Range(0, 100)
    ,TooltipAttribute("ツボへの吸引力の強さ")]
    private float power_ = 1.5f;
    public float Power_ { get { return power_; } }

    [SerializeField, Range(0, 100)
    ,TooltipAttribute("タイフーンを何秒起こすか(秒)")]
    private float limit_time_ = 10.0f;
    public float LimitTime_ { get { return limit_time_; } }

    [SerializeField
    ,TooltipAttribute("ここに「FruitManager」prefabを入れてください\n(プログラマー用)")]
    private GameObject fruit_manager_;
    public GameObject FruitManager_ { get { return fruit_manager_; } }

    [SerializeField
    ,TooltipAttribute("アプモンのオブジェクトを入れてください")]
    private GameObject apple_obj_;
    public GameObject AppleObj { get { return apple_obj_; } }

    [SerializeField
    ,TooltipAttribute("レーモンのオブジェクトを入れてください")]
    private GameObject lemon_obj_;
    public GameObject LemonObj_ { get { return lemon_obj_; } }

    [SerializeField
    ,TooltipAttribute("モモンのオブジェクトを入れてください")]
    private GameObject momon_obj_;
    public GameObject MomonObj { get { return momon_obj_; } }

    [SerializeField
    ,TooltipAttribute("ツボの「prefab」を「Hierarchy」から入れてください")]
    private GameObject tubo_obj_;

    [SerializeField
    ,TooltipAttribute("「Ike3Typhoon」prefabを入れてください")]
    private GameObject typhoon_obj_;

    [SerializeField
    ,TooltipAttribute("「Ike3TyphoonManager」prefabを「Hierarchy」入れてください")]
    private GameObject typhoon_manager_obj_;
    public GameObject TyhoonManagerObj { get { return typhoon_manager_obj_; } }

    [SerializeField
    ,TooltipAttribute("「Ike3TyphoonEffectHalo」prefabを入れてください")]
    private GameObject typhoon_effect_halo_;
    public GameObject TyphoonEffectHalo { get { return typhoon_effect_halo_; } }

    [SerializeField
    ,TooltipAttribute("「Ike3TyphoonEffectCube」prefabを入れてください")]
    private GameObject typhoon_effect_cube_;
    public GameObject TyphoonEffectCube { get { return typhoon_effect_cube_; } }


    void Start()
    {
        //TyphoonOn();
    }

    public void TyphoonOn()
    {
        GameObject game_object = Instantiate(typhoon_obj_);
        game_object.transform.SetParent(typhoon_manager_obj_.transform);
        game_object.transform.eulerAngles = tubo_obj_.transform.eulerAngles;
        game_object.transform.position = tubo_obj_.transform.position;
        game_object.transform.Translate(0.0f, 3.0f, 0.0f);
        game_object.name = typhoon_obj_.name;
        AudioManager.Instance.PlaySe(13);
    }
}
