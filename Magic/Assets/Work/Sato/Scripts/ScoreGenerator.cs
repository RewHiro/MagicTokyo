using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class ScoreGenerator : MonoBehaviour
{

    //scorenumber ０～９を設定
    [SerializeField]
    private Sprite[] Score_Number_ = new Sprite[10];
    [SerializeField]
    private GameObject Score_Prefab_;
    [SerializeField]
    private float Ajust_Spite_Position_ = 100.0F;
    
    //score値保存用、スコア表示フラグ.
    private int[] score_index_ = new int[2];
    private bool do_open_score_;
    public bool DoOpenScore
    {
        get { return do_open_score_; }
        set { do_open_score_ = value; }
    }
    private int score_;
    public int Score { get { return score_; } }
    private int digit_counter_;
    public int DigitCounter
    {
        get { return digit_counter_; }
        set { digit_counter_ = value; }
    }
  


    // Use this for initialization
    void Start()
    {
       
        DoOpenScore = true;

        //ScoreSaverから自陣地のアㇷ゚モンの数を取得.
        GameObject score_reference_ = GameObject.Find("ScoreManager") as GameObject;
        score_index_[0] = score_reference_.GetComponent<ScoreSaver>().FruitNum;
        score_index_[1] = score_reference_.GetComponent<ScoreSaver>().RemoteFruitNum;
        // Debug用
        if (score_index_ == null)
        {

            score_index_[0] = Random.Range(0, 10000);
            score_index_[1] = Random.Range(0, 10000);
            Debug.Log("Debug用スコア処理.\n又はScoreSaverからFruitNumを取得できていません.");

        }
    }


    // Update is called once per frame
    void Update()
    {

        //score表示フラグがたったらスコアを表示.
        if (DoOpenScore)
        {

            //表示位置参照用のゲームオブジェクトを参照.
            GameObject[] first_position_reference = { GameObject.Find("1PScorePosition") as GameObject,
                                                      GameObject.Find("2PScorePosition") as GameObject};

            for (int i = 0; i < score_index_.Length; i++)
            {
                //桁数カウント変数を用意、
                digit_counter_ = 0;

                //取得できなくなるまで、スコアを１の位から一桁ずつ取得.
                while (score_index_[i] >= 1)
                {

                    //スコアを１の位から一桁ずつ取得後,その桁を消す.
                    score_ = score_index_[i] % 10;
                    score_index_[i] = score_index_[i] / 10;

                    //取得した数字で、Sprite(Image)を作成.
                    GameObject Score = Instantiate(Score_Prefab_) as GameObject;

                    //表示位置を取得、
                    //サイズ（スケール）を指定、
                    //桁ごとに位置をアジャスト
                    Score.transform.parent = first_position_reference[i].transform;
                    Score.transform.localScale = Vector3.one;
                    Score.transform.localPosition = new Vector3(transform.position.x - (digit_counter_ * Ajust_Spite_Position_ * 2.5F),
                                                                transform.position.y,
                                                                transform.position.z);
                    Score.transform.name = ((i + 1) + "P_Digit" + (digit_counter_ + 1 ));
                    //    Score.GetComponent<Score>().ShowDelayTime = digit_counter_;

                    //桁番号参照用、カウンターをインクリメント.
                    digit_counter_++;
                }
            }

            DoOpenScore = false;
        }

    }
}
