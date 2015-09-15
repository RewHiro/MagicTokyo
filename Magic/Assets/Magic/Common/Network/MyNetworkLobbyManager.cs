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

        s_singleton = this;

        titel = Application.loadedLevelName;

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
            MyNetworkLobbyManager.s_singleton.networkAddress = ip;

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
        var text = File.ReadAllText(Application.dataPath + "/connect.json");
        var textAsset = Resources.Load("connect") as TextAsset;
        JsonNode json = JsonNode.Parse(text);
        string ip = json["IP"].Get<string>();
        MyNetworkLobbyManager.s_singleton.networkAddress = ip;
        MyNetworkLobbyManager.s_singleton.is_1p_ = false;
        MyNetworkLobbyManager.s_singleton.StartClient();
    }

    public override void OnLobbyStopHost()
    {
        MyNetworkLobbyManager.s_singleton.StartHost();
        base.OnLobbyStopHost();
    }

    public override void OnServerDisconnect(NetworkConnection conn)
    {
        Debug.Log("相手なし");
        if (Application.loadedLevelName == "yanai_title")
        {
            foreach (var player in FindObjectsOfType<LobbyPlayer>())
            {
                player.Reset();
            }
        }
        else if (Application.loadedLevelName == "gamemain")
        {
            foreach (var player in FindObjectsOfType<LobbyPlayer>())
            {
                if (!player.isLocalPlayer) continue;
                player.Reset();
            }
            MyNetworkLobbyManager.s_singleton.ServerReturnToLobby();
        }
        base.OnServerDisconnect(conn);
    }

    public override void OnClientError(NetworkConnection conn, int errorCode)
    {
        Debug.Log("Error");
        foreach (var player in FindObjectsOfType<LobbyPlayer>())
        {
            if (!player.isLocalPlayer) continue;
            player.Reset();
        }
        base.OnClientError(conn, errorCode);
    }

    public override void OnClientNotReady(NetworkConnection conn)
    {
        Debug.Log("Error");
        base.OnClientNotReady(conn);
    }

    public override void OnServerError(NetworkConnection conn, int errorCode)
    {
        Debug.Log("Error");
        base.OnServerError(conn, errorCode);
    }

    public override void OnClientConnect(NetworkConnection conn)
    {
        Debug.Log("Connect");
        base.OnClientConnect(conn);
    }

    //public override void OnClientSceneChanged(NetworkConnection conn)
    //{
    //    base.OnClientSceneChanged(conn);
    //}
    //public override void OnLobbyClientSceneChanged(NetworkConnection conn)
    //{
    //    GameObject.Destroy(GameObject.Find("TutorialRoot(Clone)"));
    //    GameObject.Destroy(GameObject.Find("UI_Prefab(Clone)"));
    //    MyNetworkLobbyManager.s_singleton.is_tutorial_ = false;
    //    base.OnLobbyClientSceneChanged(conn);
    //}
}
