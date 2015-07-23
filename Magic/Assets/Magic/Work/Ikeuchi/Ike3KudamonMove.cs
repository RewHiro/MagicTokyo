using UnityEngine;
using System.Collections;

public class Ike3KudamonMove : MonoBehaviour {

    enum Patern
    {
        NORMAL,
        KINESIS,
        SHOOT,
    }
    private Patern patern_ = Patern.NORMAL;

    private const float RESET_ = 0.0f;

    private float float_second_ = 0.0f;
    private float shoot_speed_ = 0.0f;

    private Vector3 float_pos_ = Vector3.zero;
    public Vector3 FloatPos { set { float_pos_ = value; } }

    private Vector3[] bezier_pos_ = new Vector3[3];
    private Vector3 BezierMiddleOffset { get { return new Vector3(0.0f, 8.0f, 0.0f); } }

    private GameObject tubo_obj_;
    private Behaviour halo_;	// 発光のオン・オフ

    ///////////////////////////////////////////////////////////////////
    // ------------------------------------------------------------
    private float count_ = 0.0f;
    // パターン		  : 処理
    // Patern.KINESIS : sinでY座標を動かすcount
    // Patern.SHOOT	  : ベジェ曲線を動かすcount
    // ------------------------------------------------------------
    // ベジェ曲線の時間(count = 0～1)をイージングで操作するためのカウント
    private float ease_count_ = 0.0f;
    ///////////////////////////////////////////////////////////////////

	// Use this for initialization
	void Start () {
        // Ike3KinesisSetting は自分が作った「prefab」だから名前勝手に変えないだろうという慢心Find
		var setting_ = GameObject.Find("Ike3KinesisSetting").GetComponent<Ike3KinesisSetting> ();
		float_second_ = setting_.FloatSecond * 60.0f; // 秒に直す
		shoot_speed_ = setting_.ShootSpeed;

        tubo_obj_ = setting_.TuboObj;

		halo_ = (Behaviour)GetComponent("Halo");
	}

	// Update is called once per frame
	void Update () {
        PaternUpdate();
	}

    void PaternUpdate()
    {
        switch (patern_)
        {
            case Patern.NORMAL:
                // 通常時
                break;
            case Patern.KINESIS:
                Kinesis();
                break;
            case Patern.SHOOT:
                Shoot();
                break;
        }
    }

	void Kinesis(){
		// 浮く場所向かい、ふわふわする処理
		count_++;
        const float MOVE_SPEED = 5.0f;
		var move_value = float_pos_ - transform.position;
		var move_value_normal = move_value.normalized;		
		transform.position = Vector3.MoveTowards(transform.position,float_pos_,Time.deltaTime * MOVE_SPEED);

        const float COUNT_SPEED_ = 0.1f;
        const float SIN_VALUE_ = 0.05f;
		var y = Mathf.Sin (count_ * COUNT_SPEED_) * SIN_VALUE_;
		transform.position += new Vector3(0.0f, y, 0.0f);

		transform.eulerAngles = Vector3.zero;
		
		// 制限時間を超えたら浮かせるの止める
		if(count_ > float_second_){
			patern_ = Patern.NORMAL;
			GetComponent<Rigidbody>().useGravity = true;
			count_ = RESET_;
			halo_.enabled = false;
		}
	}

	void Shoot(){
		// 鍋に突っ込ませる処理
        const float EASE_MAX = 1.0f;
        const float EASE_MIN = 0.0f;
        const float EASE_TOTALTIME = 2.0f;
		ease_count_ += shoot_speed_;
        count_ = (float)OutQuint(ease_count_, EASE_TOTALTIME, EASE_MAX, EASE_MIN);
        transform.position = Bezier(count_, bezier_pos_);
		
		if(count_ > EASE_MAX){
			patern_ = Patern.NORMAL;
			count_ = RESET_;
			ease_count_ = RESET_;
			halo_.enabled = false;
		}
	}

    void OnCollisionEnter(Collision collision)
    {
        var Hand = GameObject.Find("RigidHand(Clone)");

        if (Hand != null)
        {

            bool hit_flag = collision.gameObject.name == "bone3" ||
                            collision.gameObject.name == "bone2" ||
                            collision.gameObject.name == "bone1" ||
                            collision.gameObject.name == "palm";

            if (hit_flag)
            {
                ShootOn();
            }

        }
	}

    Vector3 Bezier(float count, Vector3[] pos)
    {
        float x = (1 - count) * (1 - count) * pos[0].x
                  + 2 * (1 - count) * count * pos[1].x
                            + count * count * pos[2].x;
        float y = (1 - count) * (1 - count) * pos[0].y
                  + 2 * (1 - count) * count * pos[1].y
                            + count * count * pos[2].y;
        float z = (1 - count) * (1 - count) * pos[0].z
                  + 2 * (1 - count) * count * pos[1].z
                            + count * count * pos[2].z;
        return new Vector3(x, y, z);
    }

	double OutQuint(double t,double totaltime,double max ,double min )
	{
		max -= min;
		t = t/totaltime-1;
		return max*(t*t*t*t*t+1)+min;
	}

	public void ShootOn(){
		if (Patern.KINESIS == patern_) {
            patern_ = Patern.SHOOT;
            GetComponent<Rigidbody>().useGravity = true;
            count_ = RESET_;

            // ベジェ曲線必要な座標３点を設定
            bezier_pos_[0] = transform.position;
            Vector3 offset = BezierMiddleOffset;
            Vector3 middle = (transform.position - tubo_obj_.transform.position) / 2.0f;
            bezier_pos_[1] = middle + offset;
            bezier_pos_[2] = tubo_obj_.transform.position;
		}
	}

    public void KinesisuOn()
    {
        patern_ = Patern.KINESIS;
        halo_.enabled = true;
        GetComponent<Rigidbody>().useGravity = false;
    }
}
