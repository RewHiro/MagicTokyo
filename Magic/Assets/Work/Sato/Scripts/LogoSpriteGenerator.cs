using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEditor;
using UnityEditor.Sprites;
public class LogoSpriteGenerator : MonoBehaviour {

    [SerializeField]
    private Image LogoSpritePrefabs;
    [SerializeField]    
    private int SpriteWidth;
    Image[] logo_sprite_index;
  

    void Awake()
    {
        Image[] logo_ = new Image[SpriteWidth];

        for (int i = 0; i < SpriteWidth; ++i)
        {
            logo_[i] = Instantiate(LogoSpritePrefabs);


            logo_[i].transform.parent = gameObject.transform;
            logo_[i].rectTransform.sizeDelta = new Vector2(.1f + (i * 1.0f), 500.0f);
            logo_[i].rectTransform.localPosition = new Vector3(0, 0, 0);
            logo_[i].rectTransform.localScale = new Vector3(1, 1, 1);
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
