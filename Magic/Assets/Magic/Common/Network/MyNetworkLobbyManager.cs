using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
using UnityEngine.UI;
using System.IO;

public class MyNetworkLobbyManager : NetworkLobbyManager
{

    public static MyNetworkLobbyManager s_singleton = null;
    string titel = null;

    int bgm_count_ = 0;
    public int BGMCount { get { return bgm_count_; } set { bgm_count_ = value; } }

    bool is_1p_ = true;
    public bool Is1P { get { return is_1p_; } }

    bool is_tutorial_ = false;
    public bool IsTutorial { get { return is_tutorial_; } set { is_tutorial_ = value; } }

    void Start()
    {
        titel = Application.loadedLevelName;
        s_singleton = this;

        var text = File.ReadAllText(Application.dataPath + "/connect.json");
        var textAsset = Resources.Load("connect") as TextAsset;
        JsonNode json = JsonNode.Parse(text);

        var is_host = json["Host"].Get<bool>();
        if (is_host)
        {
            MyNetworkLobbyManager.s_singleton.StartHost();
        }
        else
        {
            string ip = json["IP"].Get<string>();
            MyNetworkLobbyManager.singleton.networkAddress = ip;

            MyNetworkLobbyManager.s_singleton.is_1p_ = false;
            MyNetworkLobbyManager.s_singleton.StartClient();
        }
    }

    public void GameStart()
    {

    }

    public override void OnClientDisconnect(NetworkConnection conn)
    {
        base.OnClientDisconnect(conn);
        var textAsset = Resources.Load("connect") as TextAsset;
        JsonNode json = JsonNode.Parse(textAsset.text);
        string ip = json["IP"].Get<string>();
        MyNetworkLobbyManager.singleton.networkAddress = ip;
        MyNetworkLobbyManager.s_singleton.is_1p_ = false;
        MyNetworkLobbyManager.s_singleton.StartClient();
    }

}
