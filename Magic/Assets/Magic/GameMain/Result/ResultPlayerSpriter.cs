using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ResultPlayerSpriter : MonoBehaviour
{

    [SerializeField]
    Sprite one_player_sprite_ = null;
    [SerializeField]
    Sprite two_player_sprite_ = null;

    // Use this for initialization
    void Start()
    {
        if (MyNetworkLobbyManager.s_singleton.Is1P)
        {
            GetComponent<Image>().sprite = one_player_sprite_;
        }
        else
        {
            GetComponent<Image>().sprite = two_player_sprite_;
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
