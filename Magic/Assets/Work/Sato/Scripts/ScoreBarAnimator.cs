using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class ScoreBarAnimator : MonoBehaviour {


    public GameObject particle_;
    public GameObject finished_particle_;
    private GameObject particle_index_;
    private Slider slider_reference_;
    public float slider_value_;


    //総合得点数
    //自己得点数
    //総合得点数から見た、自己得点数の割合
    public float synthesis_kudamon_value_;
    public float my_score_;
    public float percentage_score_;

    //アニメーションフラグ.
    public bool do_animation_ = false;
    public bool DoAnimation { set { do_animation_ = value; }  }
    public bool finshed_animation_ = false;
    public bool FinishedAnimation { set { finshed_animation_ = value; } }

    public float end_animation_speed_ = 0.075F;
    public float animation_counter_ = 0.0F;

    public float jadge_animation_speed_ = 0.25F;
    public float target_value_;
    public float change_timer_ = 0;
    public float change_timer_random_max_ = 1.0F;
    public float change_timer_random_min_ = 0.1F;


    // Use this for initialization
    void Start () {
               
        //アタッチしてあるsliderを呼び出す.
        slider_reference_ = GetComponent<Slider>();

        target_value_ = Random.Range(0.15F, 0.4F);

        //自分のスコア.    
        my_score_ = GameObject.FindObjectOfType<ScoreSaver>().FruitNum;
        //総合スコア.
        synthesis_kudamon_value_ = my_score_ + GameObject.FindObjectOfType<ScoreSaver>().RemoteFruitNum;

        //最初にスコアから、自己得点数の割合を保存.
        percentage_score_ = ScorePercentage();
    }
	
	// Update is called once per frame
	void Update () {

        //スライダーのアニメーションを開始.
        if (do_animation_){

            //アニメーションカウンターを更新.
            animation_counter_ += 1.0F * Time.deltaTime;
            particle_.SetActive(true);

            RandomScoreValue();
           
     
        }
        //結果を徐々に表示
        if (finshed_animation_){

            do_animation_ = false;
            if (particle_index_ == null)
            {
            
                //パーティクル生成
                particle_index_ = Instantiate(finished_particle_) as GameObject;
                //位置を固定
                particle_index_.transform.position = new Vector3(-0.05F,4.64F,-5.32F);

            }
                
             //スコアバーを表示.
            slider_reference_.value += (percentage_score_ - slider_reference_.value) * end_animation_speed_;
            particle_.SetActive(false);

        }

	}


    //スコアの割合を取得
    public float  ScorePercentage(){

       
        float percentage = my_score_ / synthesis_kudamon_value_;

        Debug.Log("percentage" + percentage);


        return percentage;

        
    }
   
    //ランダムでバーをアニメーション.
    public void RandomScoreValue(){
       
        //ランダムで決めたtarget位置に到着後　ターゲットの位置を更新
        if (change_timer_ < 0)
        {

            //valueが半分以上の場合、それ以下
            if (target_value_ > 0.5F){

                target_value_ = Random.Range(0.15F, 0.4F);

            }
            else{//以下の場合はそれ以上.

                target_value_ = Random.Range(0.6F, 0.85F);

            }


            //タイマーリセット
            change_timer_ = Random.Range(change_timer_random_min_,change_timer_random_max_);

        }
        else
        {
            //ターゲット位置まで移動.
            slider_reference_.value += (target_value_ - slider_reference_.value) * jadge_animation_speed_;

            //
            change_timer_ -= Time.deltaTime;
        }

    }

    //スコアの割合を数字で表示.
    void SetPercentageValue()
    {

        //未実装.

    }
}
