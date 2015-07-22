using UnityEngine;
using System.Collections;

public class Ike3counter : MonoBehaviour {

	private TextMesh count_text_;
    private Ike3dorian dorian_;

    [SerializeField
    , TooltipAttribute("テキストの座標位置")]
    private Vector3 offset_ = new Vector3(-1.5f, 1.0f, 0.0f);

    private Ike3DorianSetting setting_;
    private Vector3 look_at_;

	// Use this for initialization
	void Start () {
        Init();
	}
	
	// Update is called once per frame
	void Update () {
        TextUpdate();
	}

    void Init()
    {
        count_text_ = GetComponent<TextMesh>();
        dorian_ = GetComponentInParent<Ike3dorian>();
        setting_ = GameObject.Find("Ike3DorianSetting")
            .GetComponent<Ike3DorianSetting>();
    }

    void TextUpdate()
    {
        var count = (int)((dorian_.ExplosionLimitTime - dorian_.ExplosionCount) / 60);
        count_text_.text = string.Format("{0}", count);

        look_at_ = setting_.transform.eulerAngles;
        transform.position = dorian_.transform.position + offset_;
        transform.eulerAngles = look_at_;
    }
}
