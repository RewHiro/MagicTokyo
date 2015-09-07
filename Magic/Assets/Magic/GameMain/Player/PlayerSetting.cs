using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class PlayerSetting : NetworkBehaviour
{
    [SerializeField]
    GameObject ui_;

    // Use this for initialization
    void Start()
    {
        if (isLocalPlayer)
        {
            Instantiate(ui_);
            name = "Player1";
        }
        else
        {
            name = "Player2";
        }


        if (MyNetworkLobbyManager.s_singleton.IsTutorial)
        {
            Instantiate(ui_);
        }
    }
}
