using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class MyNetworkManager : NetworkManager
{
    public void GameStart()
    {
        base.StartHost();
    }

    public void GameJoint()
    {
        base.StartClient();
    }
}
