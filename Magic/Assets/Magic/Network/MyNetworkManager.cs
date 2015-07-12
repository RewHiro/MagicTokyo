using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
using UnityEngine.UI;

public class MyNetworkManager : NetworkManager
{

    bool is_start_ = false;
    int count_ = 0;

    const int CHANGE_HOST_TIME = 90;

    public void GameStart()
    {
        NetworkManager.singleton.StartClient();
        is_start_ = true;
        GameObject.Find("WaitText").GetComponent<Text>().enabled = true;
    }

    void Update()
    {
        if (!is_start_) return;
        count_++;
        if (count_ < CHANGE_HOST_TIME) return;
        count_ = 0;
        is_start_ = false;
        NetworkManager.singleton.StartHost();
    }

    public override void OnClientDisconnect(NetworkConnection conn)
    {
        base.OnClientDisconnect(conn);
        Application.LoadLevel("result");
    }
}
