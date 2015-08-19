using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class LogoSpriteGenerator : MonoBehaviour {

    /* [SerializeField]
     private Image LogoSpritePrefabs;
     [SerializeField]    
     private int SpriteWidth;
     Image[] logo_sprite_index;*/

    [SerializeField]
    private Sprite prefab_win_;
    [SerializeField]
    private Sprite prefab_draw_;
    [SerializeField]
    private Sprite prefab_lose_;

    private Image image_;

    private bool is_win_;
    private bool is_draw_;
    
    void Awake()
    {
        /*   Image[] logo_ = new Image[SpriteWidth];

           for (int i = 0; i < SpriteWidth; ++i)
           {
               logo_[i] = Instantiate(LogoSpritePrefabs);


               logo_[i].transform.parent = gameObject.transform;
               logo_[i].rectTransform.sizeDelta = new Vector2(.1f + (i * 1.0f), 500.0f);
               logo_[i].rectTransform.localPosition = new Vector3(0, 0, 0);
               logo_[i].rectTransform.localScale = new Vector3(1, 1, 1);
           }*/

        image_ = GetComponent<Image>();

        var result_reference_ = GameObject.FindObjectOfType<ResultAnimationManager>();
        SpriteAnimator animator_ = GetComponent<SpriteAnimator>();

        is_win_ = result_reference_.IsWin;
        is_draw_ = result_reference_.IsDraw;

        if (is_draw_){
            animator_.SetAnimation = 1;
            image_.sprite = Instantiate(prefab_draw_) as Sprite;
        }
        else if (is_win_){

            animator_.SetAnimation = 3;
            image_.sprite = Instantiate(prefab_win_) as Sprite;

        }
        else{

            animator_.SetAnimation = 1;
            image_.sprite = Instantiate(prefab_lose_) as Sprite;

        }

    }
	// Use this for initialization
	void Start ()
    {
     
	}
	
	// Update is called once per frame
	void Update () 
    {
	
	}
}
