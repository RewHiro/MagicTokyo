using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
using UnityEngine.UI;

public class MyNetworkLobbyManager : NetworkLobbyManager
{

    public static MyNetworkLobbyManager s_singleton = null;
    string titel = null;

    int bgm_count_ = 0;
    public int BGMCount{get{ return bgm_count_; }set { bgm_count_ = value; } }

    bool is_1p_ = true;
    public bool Is1P { get { return is_1p_; }}

    void Start()
    {
        titel = Application.loadedLevelName;
        s_singleton = this;
        var textAsset = Resources.Load("connect") as TextAsset;
        JsonNode json = JsonNode.Parse(textAsset.text);
        string ip = json["IP"].Get<string>();
        MyNetworkLobbyManager.singleton.networkAddress = ip;

        if (null == MyNetworkLobbyManager.s_singleton.StartHost())
        {
            MyNetworkLobbyManager.s_singleton.is_1p_ = false;
            MyNetworkLobbyManager.s_singleton.StartClient();
        }
    }

    public void GameStart()
    {

    }

}
