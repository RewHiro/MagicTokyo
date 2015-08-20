using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class CharacterGenerator : MonoBehaviour {

    enum Kudamon_Type_
    {
        Lemon_Win_,
        Lemon_Lose_,
        Appmon_Win_,
        Appmon_Lose_,
    };

    //debug用
   [SerializeField]
    private Kudamon_Type_ Kudamon_Image_Type_;

    [SerializeField]
    private Sprite lemon_prefab_win_;
    [SerializeField]
    private Sprite appmon_prefab_win_;
    [SerializeField]
    private Sprite lemon_prefab_lose_;
    [SerializeField]
    private Sprite appmon_prefab_lose_;

    //    private CanvasRenderer canvas_;
    private Image instantiate_character_;
    private bool is_generate_image_;
    public bool Do_Character_Image_Generate_{
        get { return is_generate_image_; }
        set { is_generate_image_ = value; }
    }
    //個別アニメーション設定用.
    private ImageAnimator animator_;

    // Use this for initialization
	void Awake() {

        Do_Character_Image_Generate_ = true;
        instantiate_character_ = GetComponent<Image> () ;
        animator_ = GetComponent<ImageAnimator>();

        //player番号を取得.
        var score_saver = GameObject.FindObjectOfType<ScoreSaver>();
        var result_refernce = GameObject.FindObjectOfType<ResultAnimationManager>();
        bool player_number = score_saver.Is1P;
        bool is_win = result_refernce.IsWin;
        bool is_draw = result_refernce.IsDraw;

        player_number = false;
      

        //プレイヤー番号、勝敗をもとに表示する画像を設定
        if (player_number){
            
            //1P画像.(Lemon)
            if (is_win){
                Kudamon_Image_Type_ = Kudamon_Type_.Lemon_Win_;
            }
            else{
                Kudamon_Image_Type_ = Kudamon_Type_.Lemon_Lose_;
            }

        }
        else { 

            //2P画像.(apmon)
            if (is_win) {
                Kudamon_Image_Type_ = Kudamon_Type_.Appmon_Win_;
            }
            else{
                Kudamon_Image_Type_ = Kudamon_Type_.Appmon_Lose_;
            }

        }
    }

	
	// Update is called once per frame
	void Update () {

        if (Do_Character_Image_Generate_)
        {
            // クダモンのタイプでi変更
            switch (Kudamon_Image_Type_)
            {
                case Kudamon_Type_.Lemon_Win_:
                    animator_.SetType = 1;
                    instantiate_character_.sprite = Instantiate(lemon_prefab_win_) as Sprite;
                    break;
                case Kudamon_Type_.Lemon_Lose_:
                    animator_.SetType = 1;
                    instantiate_character_.sprite = Instantiate(lemon_prefab_lose_) as Sprite;
                    break;
                case Kudamon_Type_.Appmon_Win_:
                    animator_.SetType = 2;
                    animator_.is_add_vec_x = true;
                    animator_.is_add_vec_y = true;
                    animator_.animation_range_ = 0.2F;
                    animator_.animation_speed_ = 0.03F;
                    instantiate_character_.sprite = Instantiate(appmon_prefab_win_) as Sprite;
                    break;
                case Kudamon_Type_.Appmon_Lose_:
                    animator_.SetType = 2;
                    animator_.is_add_vec_x = true;
                    animator_.is_add_vec_y = true;
                    animator_.animation_range_ = 0.2F;
                    animator_.animation_speed_ = 0.01F;
                    instantiate_character_.sprite = Instantiate(appmon_prefab_lose_) as Sprite;
                    break;
            }

            Do_Character_Image_Generate_ = false;
        }
    }
}
