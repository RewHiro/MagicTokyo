using UnityEngine;
using System.Collections;

public class ResultAnimationManager : MonoBehaviour {

    //あらいさんへ！おつかれさまです。
    //リザルトを始めたいときはこれをよんでtrueに設定してください!
    public bool do_result_;//test 用にpublic　後でなおすもん。
    public bool DoResult{

        set { do_result_ = value; }
        get { return do_result_; }

    }

    //スコア表示、キャラクター表示、勝敗表示を管理.
    public GameObject ScorePanel;
    public GameObject CharacterPanel;
    public GameObject ResultPanel;
    private GameObject canvas_refernce_;
    private bool do_open_;
    public bool DoOpen{

        set { do_open_ = value;}
        get { return do_open_; }

    }
    private bool is_win_;
    public bool IsWin{ get { return is_win_; }}
    private bool is_draw_;
    public bool IsDraw { get { return is_draw_; } }

    void JadageWin() {

        var score_saver = GameObject.FindObjectOfType<ScoreSaver>();

           //勝ち,負け,引き分け、判定.
           if (score_saver.FruitNum > score_saver.RemoteFruitNum)
           {

               //勝ち
               is_win_= true;
               is_draw_= false;


           }
           else if (score_saver.FruitNum < score_saver.RemoteFruitNum)
           {

               //負け
               is_win_= false;
               is_draw_= false;

           }
           else
           {
               //引き分け
               is_draw_= true;
           }
        /* ///TEST TEST　 TEST TEST TEST TEST ////　
                const int test_1p = 100;
                const int test_2p = 200;
                //勝ち,負け,引き分け、判定.
                if (test_1p > test_2p)
                {

                    //勝ち
                    is_win_ = true;
                    is_draw_ = false;


                }
                else if (test_1p < test_2p)
                {

                    //負け
                    is_win_ = false;
                    is_draw_ = false;

                }
                else
                {
                    //引き分け
                    is_draw_ = true;
                }
        */ // TEST TEST TEST TEST TEST TEST TEST //

    }


    // Use this for initialization
    void Start () {

        canvas_refernce_ = GameObject.Find("Canvas") as GameObject;
        canvas_refernce_.GetComponent<Canvas>().enabled = false;
        do_open_ = false;
        do_result_ = false;
       
    }

    // Update is called once per frame
    void Update () {

        //りざると開始
        if (do_result_)
        {
            canvas_refernce_.GetComponent<Canvas>().enabled = true;
            //勝ち負けをジャッジ！
            JadageWin();
            
            //最初にスコアのみ表示.どぅるるるが始まります.
            var obj = Instantiate(ScorePanel);
            obj.transform.parent = canvas_refernce_.transform;

            do_result_ = false;
        }

        //スコア表示終了後
        if (do_open_){
            
            //キャラクターと、勝敗ロゴを生成.
            //キャンバスの子にすると、pos,scale情報がずれるので
            //初期値を補完、その後再代入.(いいやり方他におもいつくまで)
            var logo = Instantiate(ResultPanel);
            var defalt_logo_pos = logo.transform.position;
            var defalt_logo_scale = logo.transform.localScale;

            logo.transform.parent = canvas_refernce_.transform;
            logo.transform.localPosition = defalt_logo_pos;
            logo.transform.localScale = defalt_logo_scale;

            var character = Instantiate(CharacterPanel);
            var defalt_chara_pos = character.transform.position;
            var defalt_chara_scale = character.transform.localScale;

            character.transform.parent = canvas_refernce_.transform;
            character.transform.localPosition = defalt_chara_pos;
            character.transform.localScale = defalt_chara_scale;

          

            //一度だけ作成.
            do_open_ = false;

        }

	}
}
