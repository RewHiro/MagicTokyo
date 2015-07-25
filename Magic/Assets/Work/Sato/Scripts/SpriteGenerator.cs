using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class SpriteGenerator : MonoBehaviour {

    

    //debug用
   [SerializeField,Range(0,2)]
    private int kudamon_type_ = 0;

    [SerializeField]
    private Sprite lemon_prefab_;
    [SerializeField]
    private Sprite appmon_prefab_;
    [SerializeField]
    private Sprite momon_prefab_;

//    private CanvasRenderer canvas_;
    private Image instantiate_character_;

    // Use this for initialization
	void Awake() {

        instantiate_character_ = GetComponent<Image> () ;

        //クダモンのタイプでi変更
        switch (kudamon_type_)
        {
            case 0:
                instantiate_character_.sprite = Instantiate(lemon_prefab_) as Sprite;
                break;
            case 1:
                instantiate_character_.sprite = Instantiate(appmon_prefab_) as Sprite;
                break;
            case 2:
                instantiate_character_.sprite = Instantiate(momon_prefab_) as Sprite;
                break;
        }
      
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
