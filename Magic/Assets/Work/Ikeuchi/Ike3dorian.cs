using UnityEngine;
using System.Collections;

public class Ike3dorian : MonoBehaviour {

    private float change_color_and_size_count_ = 0;
    private float count_speed_ = 0;

    private float explosion_count_ = 0.0f;
    public float ExplosionCount { get { return explosion_count_; } }
    private float explosion_limit_time_ = 0.0f;
    public float ExplosionLimitTime { get { return explosion_limit_time_; } }
    private float explosion_size_ = 0.0f;

    private int apple_num_ = 0;
    private int lemon_num_ = 0;
    private int peach_num_ = 0;
    private int eggplant_num_ = 0;

    private const int NEXT_FRAME = 1;

    private bool is_explosion_ = false;
    public bool IsExplosion { get { return is_explosion_; } }

    [SerializeField
    ,TooltipAttribute("ここに「FruitManager」prefabを入れてください\n(プログラマー用)")]
    private GameObject fruit_manager_;

    [SerializeField
    , TooltipAttribute("ここに「Ike3ParticleManager」prefabを入れてください\n(プログラマー用)")]
    private GameObject particle_manager_;

    [SerializeField
    ,TooltipAttribute("ここに「Ike3DorianSetting」prefabを入れてください\n(プログラマー用)")]
    private GameObject dorian_setting_;

    [SerializeField
    ,TooltipAttribute("ドリアンの色を変えたい箇所「Material」を入れてください\n(トゲのマテリアル[lambert28])")]
    private Material change_color_material_;

	[SerializeField
    ,TooltipAttribute("ドリアンが爆発するときに起こす「ParticleSystem」、「Ike3Explosion」を入れてください")]
    private ParticleSystem particle_;

	// Use this for initialization
	void Start () {
        DorianSetting();
	}
	
	// Update is called once per frame
	void Update () {
        ChangeColorAndSize();
        DorianExplosion();
	}

    void DorianSetting()
    {
        var setting = GameObject.Find(dorian_setting_.name)
            .GetComponent<Ike3DorianSetting>();
        explosion_limit_time_ = setting.ExplosionTime * 60;
        explosion_size_ = setting.ExplosionSize;
        apple_num_ = setting.AppleNum;
        lemon_num_ = setting.LemonNum;
        peach_num_ = setting.PeachNum;
        eggplant_num_ = setting.EggplantNum;
    }

    void ChangeColorAndSize()
    {
        count_speed_ += 0.001f;

        // color ----------------------------------------------------------------------------
        change_color_and_size_count_ += count_speed_;
        const float COLOR_OFFSET = 0.5f;
        const float COLOR_LIMIT_OFFSET = 0.5f;
        float red_width = Mathf.Sin(change_color_and_size_count_) * COLOR_LIMIT_OFFSET;
        float green_width = Mathf.Cos(change_color_and_size_count_) * COLOR_LIMIT_OFFSET;
        float red = red_width + COLOR_OFFSET;
        float green = green_width + COLOR_OFFSET;     
        change_color_material_.color = new Color(red, green, 0.0f);

        // size -----------------------------------------------------------------------------
        const float size_offset = 1.0f;
        const float size_limit_width = 0.1f;
        float dorian_size_width = Mathf.Sin(change_color_and_size_count_) * size_limit_width;
        float dorian_size = dorian_size_width + size_offset;
        transform.localScale = Vector3.one * dorian_size;
    }

    void DorianExplosion()
    {
        if (explosion_count_ == explosion_limit_time_)
        {
            is_explosion_ = true;
            transform.localScale = Vector3.one * explosion_size_;
            var create = GameObject.Find(fruit_manager_.name).GetComponent<FruitCreater>();
            create.AppleCreate(apple_num_);
            create.LemonCreate(lemon_num_);
            create.PeachCreate(peach_num_);
            create.EggPlantCreate(eggplant_num_);

            GameObject particle_manager = GameObject.Find(particle_manager_.name);
            ParticleSystem game_object = Instantiate(particle_);
            game_object.transform.SetParent(particle_manager.transform);
            game_object.transform.position = transform.position;
            game_object.name = particle_.name;
            AudioManager.Instance.PlaySe(9);
        }
        else if (explosion_count_ == explosion_limit_time_ + NEXT_FRAME)
        {
            Destroy(gameObject);
        }
        explosion_count_++;
    }

    public void ExplodeForcibly()
    {
        explosion_count_ = explosion_limit_time_;
    }
}
