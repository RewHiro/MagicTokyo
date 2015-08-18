using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
using UnityEngine.UI;

public class MyNetworkLobbyManager : NetworkLobbyManager
{

    public static MyNetworkLobbyManager s_singleton = null;

    readonly int TITLE_HASH_CODE = "title".GetHashCode();
    readonly int GAMEMAIN_HASH_CODE = "gamemain".GetHashCode();
    readonly int YANAI_TITLE_HASH_CODE = "yanai_title".GetHashCode();

    bool is_ready_ = false;

    void Start()
    {
        s_singleton = this;
        var textAsset = Resources.Load("connect") as TextAsset;
        JsonNode json = JsonNode.Parse(textAsset.text);
        string ip = json["IP"].Get<string>();
        MyNetworkLobbyManager.singleton.networkAddress = ip;
    }

    public void GameStart()
    {
        if (is_ready_) return;
        if (null == MyNetworkLobbyManager.s_singleton.StartHost())
        {
            MyNetworkLobbyManager.s_singleton.StartClient();
        }
        is_ready_ = true;
        GameObject.Find("WaitText").GetComponent<Text>().enabled = true;
    }

    public void Stop()
    {
        Destroy(gameObject);
        MyNetworkLobbyManager.s_singleton.StopHost();
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
            else if (local_fruit_num < remote_fruit_num)
            {
                result = "win";
            }
            else
            {
                result = "lose";
            }
            var scoresaver = FindObjectOfType<ScoreSaver>();
            scoresaver.FruitNum = local_fruit_num;
            scoresaver.RemoteFruitNum = remote_fruit_num;
            scoresaver.Is1P = false;
            MyNetworkLobbyManager.s_singleton.StopClient();
            Application.LoadLevel(result);
        }
    }

    void Update()
    {
        ChangeScene();
        var loaded_level_name = Application.loadedLevelName.GetHashCode();
        if (loaded_level_name == TITLE_HASH_CODE ||
            loaded_level_name == GAMEMAIN_HASH_CODE ||
            loaded_level_name == YANAI_TITLE_HASH_CODE)
            return;
        Destroy(gameObject);
    }

    void ChangeScene()
    {
        var loaded_level_name = Application.loadedLevelName.GetHashCode();
        if (!(loaded_level_name == TITLE_HASH_CODE ||
            loaded_level_name == YANAI_TITLE_HASH_CODE)) return;
        if (lobbySlots.Length == 0) return;
        if (lobbySlots[1] == null) return;
        if (lobbySlots[0].readyToBegin) return;
        if (lobbySlots[1].readyToBegin)
        {
            lobbySlots[0].SendReadyToBeginMessage();
        }
    }

}
