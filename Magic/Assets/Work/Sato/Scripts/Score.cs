using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class Score : MonoBehaviour {

    //scoreumber ０～９を設定
    [SerializeField]
    private Sprite[] Score_Number_ = new Sprite[10];
    private Image number_object_;
    private int number_;
    private bool is_end_send_;
    //Animation用変数.(表示の遅延時間と、カウンター）
    public float show_lag_ = 0.3F;
    public float animation_end_target_time = 1.0F;
    [SerializeField]
    private float show_delay_time_ = 3.0F;
   
    public float ShowDelayTime{

        get { return show_delay_time_; }
        set { show_delay_time_ = value; }

    }
    private float time_counter_;
    SpriteAnimator sprite_animator_;
    ImageAnimator image_animator_;
    ScoreGenerator score_generator_refernce_;

    // Use this for initialization
    void Awake () {

        //タイマーを初期化.
        time_counter_ = 0.0F;
        image_animator_ = GetComponent<ImageAnimator>();
        is_end_send_ = false;
        //桁番号参照用に、スコアジェネレータを初期化.
        //animationの遅延時間をスコア表示側から設定、
        //一桁ずつアニメーション時間をずらす.
        score_generator_refernce_ = GameObject.FindObjectOfType<ScoreGenerator>();
        sprite_animator_ = GetComponent<SpriteAnimator>();
        sprite_animator_.target_time_ = animation_end_target_time;
        sprite_animator_.delay_time_ = show_delay_time_
                                       + (score_generator_refernce_.DigitCounter * show_lag_);
  
        //スコアを参照.(1桁分のみ)
        GameObject score_refernce = GameObject.Find("ScoreGenerator(Clone)") as GameObject;
        number_ = score_refernce.GetComponent<ScoreGenerator>().Score;
        //Spriteを初期化.数字をランダムに取得.
        number_object_ = GetComponent<Image>();
        number_object_.sprite = Instantiate(Score_Number_[Random.Range(0,9)]) as Sprite;

    }



    // Update is called once per frame
    void Update() {

        //タイムカウンターを加算.
        time_counter_ += Time.deltaTime * 1.0F;

        //animation終了通知を行うまで.
        if (!is_end_send_)
        {
            //表示遅延時間まで、スコアをランダム表示.
            if (time_counter_ < show_delay_time_)
            {
                number_object_.sprite = Instantiate(Score_Number_[Random.Range(0, 9)]) as Sprite;
            }
            else
            {
                //表示アニメーション終了後、キャラクターを表示.
                number_object_.sprite = Instantiate(Score_Number_[number_]) as Sprite;
                image_animator_.do_stop_animation_ = true;

                //最大桁のアニメーションが終わったら、キャラクター、ロゴを表示.
                if (time_counter_ > animation_end_target_time + show_delay_time_)
                {
                    if (transform.name == "1P_Digit3")
                    {
                        GameObject.FindObjectOfType<ResultAnimationManager>().DoOpen = true;
                        is_end_send_ = true;
                    }
                }

            }
        }
    }
}
