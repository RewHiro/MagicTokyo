using UnityEngine;
using System.Collections;

public class Ike3DorianSetting : MonoBehaviour {

	[SerializeField,Range(0,1000)
    ,TooltipAttribute("爆発させる時間(秒)")]
	private float explosion_time_ = 10.0f;
    public float ExplosionTime { get { return explosion_time_; } }

    [SerializeField, Range(0, 1000)
    ,TooltipAttribute("爆発させる大きさ\n(直径[10 → 半径ドリアン５個分の大きさ])")]
    private float explosion_size_ = 10.0f;
    public float ExplosionSize { get { return explosion_size_; } }
	
	[SerializeField,Range(0,1000)
    ,TooltipAttribute("生成するアブモンの数")]
    private int apple_num_ = 9;
    public int AppleNum { get { return apple_num_; } }

	[SerializeField,Range(0,1000)
    ,TooltipAttribute("生成するレーモンの数")]
    private int lemon_num_ = 9;
    public int LemonNum { get { return lemon_num_; } }

	[SerializeField,Range(0,1000)
    ,TooltipAttribute("生成するモモンの数")]
    private int peach_num_ = 1;
    public int PeachNum { get { return peach_num_; } }

	[SerializeField,Range(0,1000)
    ,TooltipAttribute("生成するジャマモンの数")]
    private int eggplant_num_ = 1;
    public int EggplantNum { get { return eggplant_num_; } }

    [SerializeField, Range(0, 1000)
    ,TooltipAttribute("デバッグドリアン生成\n(確かめ終わったら０に戻してください)")]
    private int dorian_num_ = 0;

    [SerializeField
    ,TooltipAttribute("ここに「FruitManager」prefabを入れてください\n(プログラマー用)")]
    private GameObject fruit_manager_;
    public GameObject FruitManager { get { return fruit_manager_; } }

    [SerializeField
    , TooltipAttribute("ここに「Main　Camera」prefabを「Hierarchy」から入れてください\n(爆発カウントを向けたいオブジェクト)\n(プログラマー用)")]
    private GameObject main_camera_;
    public GameObject MainCamera { get { return main_camera_; } }

    void Start()
    {
        // fruit_manager_
        //    .GetComponent<FruitCreater>().DorianCreate(dorian_num_);

        GameObject.Find(fruit_manager_.name)
            .GetComponent<FruitCreater>().DorianCreate(dorian_num_);
    }
}
