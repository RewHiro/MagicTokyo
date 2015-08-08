using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class LobbyPlayer : NetworkLobbyPlayer
{

    readonly int TITLE_HASH_CODE = "title".GetHashCode();


    void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    void Start()
    {
        if (isServer) return;
        if (!isLocalPlayer) return;
        SendReadyToBeginMessage();
    }
}
