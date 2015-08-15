using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class SpriteGenerator : MonoBehaviour {

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

    // Use this for initialization
	void Awake() {
        Do_Character_Image_Generate_ = true;
        instantiate_character_ = GetComponent<Image> () ;

    }

	
	// Update is called once per frame
	void Update () {

        if (Do_Character_Image_Generate_)
        {
            // クダモンのタイプでi変更
            switch (Kudamon_Image_Type_)
            {
                case Kudamon_Type_.Lemon_Win_:
                    instantiate_character_.sprite = Instantiate(lemon_prefab_win_) as Sprite;
                    break;
                case Kudamon_Type_.Lemon_Lose_:
                    instantiate_character_.sprite = Instantiate(lemon_prefab_lose_) as Sprite;
                    break;
                case Kudamon_Type_.Appmon_Win_:
                    instantiate_character_.sprite = Instantiate(appmon_prefab_win_) as Sprite;
                    break;
                case Kudamon_Type_.Appmon_Lose_:
                    instantiate_character_.sprite = Instantiate(appmon_prefab_lose_) as Sprite;
                    break;
            }
        }
    }
}
