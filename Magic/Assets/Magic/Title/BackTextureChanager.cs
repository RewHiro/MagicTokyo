using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class BackTextureChanager : MonoBehaviour
{

    [SerializeField]
    Texture texture_1p_ = null;

    [SerializeField]
    Texture texture_2p_ = null;

    bool is_change_ = false;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (is_change_) return;
        if (MyNetworkLobbyManager.s_singleton.Is1P)
        {
            GetComponent<RawImage>().texture = texture_1p_;
        }
        else
        {
            GetComponent<RawImage>().texture = texture_2p_;
        }
        is_change_ = true;
    }
}
