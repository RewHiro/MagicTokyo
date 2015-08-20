using UnityEngine;
using System.Collections;

public class SpriteAnimator : MonoBehaviour
{

    enum iTweenAnimation : int
    {
        Move = 0,
        Rotate,
        LoopRotate,
        Scale,
    };
    [SerializeField]
    private iTweenAnimation animation_patern_;
   
    public int SetAnimation{
        set {
            //Move = 0,Rotate = 1,LoopRotate,Scale
            animation_patern_ = (iTweenAnimation)value;
        }
    }
    [SerializeField]
    private iTweenAnimation second_animation_patern_;
    public bool do_loop_;

    //アニメーションの目的位置、サイズ、回転量（ｚ軸のみ）
    //到達時間、遅延時間. iTween用にハッシュテーブルを用意.
    public Vector2 target_pos_;
    public Vector2 target_size_;
    public float target_rotate_;
    public float animation_speed_;
    public float target_time_;
    public float delay_time_;
    private Hashtable hash_table_ = new Hashtable();
   


    [SerializeField]
    private GameObject target_image_prefab;
    [SerializeField]
    bool is_ease_out_bounce;
    [SerializeField, Range(0, 10)]
    float out_bounce_time;
    [SerializeField]
    float out_of_bounce_tgt_pos;
    [SerializeField]
    bool is_move;
    [SerializeField, Range(0, 10)]
    float move_time;
    [SerializeField]
    float move_tgt_pos;



    void Awake()
    {
       
    }
    // Use this for initialization
    void Start()
    {
        

        //到達時間,遅延時間と、ループ処理を初期化.
        hash_table_.Add("time", target_time_);
        hash_table_.Add("delay", delay_time_);

        if (do_loop_){

            //ループタイプは”start~end,end~start,start~end”で今のとこ固定
            hash_table_.Add("looptype", iTween.LoopType.pingPong);

        }
        //アニメーションパターンに分けて、ハッシュテーブルを変更
        //上から、移動、拡縮、回転アニメーション
        switch (animation_patern_)
        {
            case iTweenAnimation.Move:
                hash_table_.Add("x", target_pos_.x);
                hash_table_.Add("y", target_pos_.y);
                iTween.MoveTo(gameObject, hash_table_);
                break;
            case iTweenAnimation.Scale:
                hash_table_.Add("x", target_size_.x);
                hash_table_.Add("y", target_size_.y);
                iTween.ScaleTo(gameObject, hash_table_);
                break;
            case iTweenAnimation.Rotate:
                hash_table_.Add("z", target_rotate_);
                iTween.RotateTo(gameObject, hash_table_);
                break;

            default:
                Debug.Log("iTweenアニメーションが設定されていません.");
                break;
          
        }


       

    }


    void Update()
    {
       

    }
}
