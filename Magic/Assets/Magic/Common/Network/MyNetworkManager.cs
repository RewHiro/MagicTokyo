using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
using UnityEngine.UI;
using MiniJSON;

public class MyNetworkManager : NetworkManager
{

    bool is_start_ = false;
    int count_ = 0;

    const int CHANGE_HOST_TIME = 90;

    public void GameStart()
    {
        var textAsset = Resources.Load("connect") as TextAsset;
        JsonNode json = JsonNode.Parse(textAsset.text);
        string ip = json["IP"].Get<string>();
        networkAddress = ip;

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

        string result = "";

        foreach (var player in FindObjectsOfType<FruitCounter>())
        {
            if (!player.isLocalPlayer) continue;
            var local_fruit_num = player.FruitNum;
            var remote_fruit_num = player.RemoteFruitNum;
            if (local_fruit_num == remote_fruit_num)
            {
                result = "draw";
            }
            else if (local_fruit_num > remote_fruit_num)
            {
                result = "win";
            }
            else
            {
                result = "lose";
            }
            FindObjectOfType<ScoreSaver>().FruitNum = local_fruit_num;
        }
        Application.LoadLevel(result);
    }
}
