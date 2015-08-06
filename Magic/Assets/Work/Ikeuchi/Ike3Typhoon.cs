using UnityEngine;
using System.Collections;

public class Ike3Typhoon : MonoBehaviour
{

    private Vector3 drain_pos_;

    private string fruit_manager_name_;
    private string apple_name_;
    private string lemon_name_;
    private string momon_name_;

    private float power_ = 1.5f; 
    private float limit_time_ = 10.0f;
    private float timer_ = 0.0f;

    private GameObject typhoon_manager_obj_;
    private GameObject typhoon_effect_halo_;
    private GameObject typhoon_effect_cube_;

	// Use this for initialization
	void Start () {
        Init();
	}
	
	// Update is called once per frame
	void Update () {
        LimitTimeUpdate();
        CreateEffectHalo();
        CreateEffectCube();
    }

    void OnTriggerEnter(Collider other)
    {
        ChangeKudamonVelocity(other);
    }

    /////////////////////////////////////////////////////////////////

    void Init()
    {
        var setting = GameObject.Find("Ike3TyphoonSetting")
           .GetComponent<Ike3TyphoonSetting>();
        fruit_manager_name_ = setting.FruitManager_.name;
        apple_name_ = setting.AppleObj.name;
        lemon_name_ = setting.LemonObj_.name;
        momon_name_ = setting.MomonObj.name;
        power_ = setting.Power_;
        limit_time_ = setting.LimitTime_ * 60.0f; // 秒に直す

        typhoon_manager_obj_ = setting.TyhoonManagerObj;
        typhoon_effect_halo_ = setting.TyphoonEffectHalo;
        typhoon_effect_cube_ = setting.TyphoonEffectCube;

        float y = GetComponent<SphereCollider>().radius / -2;
        Vector3 offset = new Vector3(0.0f, y, 0.0f);
        drain_pos_ = transform.position + offset;
    }

    /////////////////////////////////////////////////////////////////

    void LimitTimeUpdate()
    {
        timer_++;
        if (timer_ > limit_time_)
        {
            Destroy(gameObject);
        }
    }

    void CreateEffectHalo()
    {
        if (0 == (timer_) % 2)
        {
            GameObject game_object = Instantiate(typhoon_effect_halo_);
            game_object.transform.SetParent(typhoon_manager_obj_.transform);
            float radius_size =GetComponent<SphereCollider>().radius * 3;
            Vector3 rand_pos = new Vector3(
                Random.Range(-radius_size, radius_size),
                Random.Range(0.0f, transform.position.y),
                Random.Range(-radius_size, radius_size)
                );
            game_object.transform.position = transform.position + rand_pos;
            game_object.name = typhoon_effect_halo_.name;

            Vector3 offset = new Vector3(0.0f, -1.0f, 0.0f);
            game_object.GetComponent<Ike3TyhoonEffectHalo>().DrainPos = drain_pos_ + offset;
        }
    }

    void CreateEffectCube()
    {
        if (0 == (timer_) % 2)
        {
            GameObject game_object = Instantiate(typhoon_effect_cube_);
            game_object.transform.SetParent(typhoon_manager_obj_.transform);
            float radius_size = GetComponent<SphereCollider>().radius * 3;
            Vector3 offset = new Vector3(0.0f, -1.0f, 0.0f);
            game_object.transform.position = drain_pos_ + offset;
            game_object.name = typhoon_effect_cube_.name;
        }
    }

    /////////////////////////////////////////////////////////////////

    // OnTriggerEnter
    void ChangeKudamonVelocity(Collider other)
    {
        var kudamon = GameObject.Find(fruit_manager_name_);
        if (kudamon != null)
        {
            bool hit_flag = other.gameObject.name == apple_name_ ||
                            other.gameObject.name == lemon_name_ ||
                            other.gameObject.name == momon_name_;

            if (hit_flag)
            {
                Debug.Log(0);
                Vector3 from_kudamon_pot =
                    drain_pos_ - other.transform.position;

                //other.GetComponent<Rigidbody>().velocity += from_kudamon_pot * power_;

                other.GetComponent<Rigidbody>().velocity /= 5.0f;
                other.GetComponent<Rigidbody>().velocity += from_kudamon_pot * power_;

                //other.GetComponent<Rigidbody>().velocity = from_kudamon_pot * power_;
            }
        }
    }

    //void OnTriggerExit(Collider other)
    //{
    //     var Typhoon = GameObject.Find(typhoon_manager_obj_.name);
    //     if (Typhoon != null)
    //     {
    //         Debug.Log(other.name);
    //         bool Exit_flag = other.gameObject.name == typhoon_effect_halo_.name;
    //         if (Exit_flag)
    //         {
    //             Debug.Log("homo");
    //             Destroy(other.gameObject);
    //         }
    //     }
    //}	
}
