using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class ResultText : MonoBehaviour {
    //Textが格納されているゲームオブジェクトを入れる箱
    [SerializeField]
    private GameObject target_canvas_text;
    //Textデータをインスペクターで変動可能
    //文字データ、色、フォント、サイズ、２次元位置（ｚ値は０で固定）
    [SerializeField]
    private string text_index;
     [SerializeField]
    private Color32 text_color;
     [SerializeField]
     private Font text_font;
     [SerializeField]
     Vector2 text_pos;
     [SerializeField]
     bool is_debug;
     [SerializeField, Range(0, 200)]
     private int text_size;
     [SerializeField, Range(-5,5)]
     private float text_glimpse_speed;

    
     private Text view_text;
     private Vector3 text_set_pos;
     private float text_alpfa;
    //テキストのアルファ値を２５５～０の間で変動、見え隠れさせる.
     void TextGlimpse() {
         text_alpfa += text_glimpse_speed;
         text_color.a = (byte)(255 * System.Math.Abs(System.Math.Sin(System.Math.PI * text_alpfa / 180.0)));
         view_text.color = text_color;
     } 
  
	// Use this for initialization
	void Awake () {
        view_text =target_canvas_text.GetComponent<Text>();
        view_text.text = text_index;
        view_text.color = text_color;
        view_text.fontSize = text_size;
        view_text.font = text_font;
        text_set_pos = new Vector3(text_pos.x,text_pos.y,0);
        view_text.rectTransform.localPosition =  text_set_pos;
	}
	
	// Update is called once per frame
	void Update () {

        TextGlimpse();
        //view_text.color = text_color;
        if (is_debug)
        {
            text_set_pos = new Vector3(text_pos.x, text_pos.y, 0);
            view_text.rectTransform.localPosition = text_set_pos;
            view_text.text = text_index;
            view_text.color = text_color;
            view_text.fontSize = text_size;
            view_text.font = text_font;
        }
	}
}
