using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class PlayerSetting : NetworkBehaviour
{

    // Use this for initialization
    void Start()
    {
        if (isLocalPlayer)
        {
            name = "Player1";
        }
        else
        {
            name = "Player2";
        }
    }
}
