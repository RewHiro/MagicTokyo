using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class ChangeScoreColorBar : MonoBehaviour {


    Image _image;
    public bool is_own_;
    // Use this for initialization

    void Start()
    {

        //初期化！うぉおおおお
        _image = GetComponent<Image>();


        //自分自身のゲージの色設定
        if (is_own_)
        {
            if (MyNetworkLobbyManager.s_singleton.Is1P)
            {

                //1P赤?
                _image.color = Color.red;

            }
            else
            {
                //2P黄色?
                _image.color = Color.yellow;

            }
        }
        else//自分と逆の色になるように設定.
        {

            if (MyNetworkLobbyManager.s_singleton.Is1P)
            {

                //1P赤?
                _image.color = Color.yellow;

            }
            else
            {
                //2P黄色?
                _image.color = Color.red;




            }
        }
    }

	// Update is called once per frame
	void Update () {
	
	}
}
