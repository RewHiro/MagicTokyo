using UnityEngine;
using System.Collections;

public class SpriteAnimator : MonoBehaviour {
    [SerializeField]
    private GameObject target_image_prefab;
    [SerializeField]
    bool is_ease_out_bounce;
     [SerializeField,Range(0,10)]
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
	void Start () 
    {
        //”ｘ”現在位置からー２０ランダムに揺れる .
      //     iTween.ShakePosition(target_image_prefab,iTween.Hash("x",-200));
       //    iTween.ShakeRotation(target_image_prefab, iTween.Hash("x", 260));

       // このコードが適用されたオブジェクトは、3秒かけて原点(0, 0, 0)を向くように調整される
       // iTween.LookTo(target_image_prefab, new Vector3(0,90,0), 3);
        if (is_ease_out_bounce)
        {
            iTween.MoveTo(target_image_prefab,
                                     iTween.Hash("y", out_of_bounce_tgt_pos, "time",out_bounce_time, "easetype", iTween.EaseType.easeOutBounce));
        }
        if (is_move)
        {
            iTween.MoveTo(target_image_prefab,
                                     iTween.Hash("x",move_tgt_pos, "time", move_time, "easetype", iTween.EaseType.easeInBack));
        }

	}
	
	
	void Update () 
    {
       
    }
}
