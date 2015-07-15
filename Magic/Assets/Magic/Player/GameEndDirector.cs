using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
using UnityEngine.UI;

public class GameEndDirector : NetworkBehaviour 
{
    float count_ = 0;

    bool is_start_ = false;
    public bool IsStart { get { return is_start_; } }

    const int FRUIT_NUM_ANNOUNCE_TIME = 2;
    const int CHANGE_SCENE_TIME = 4;


    void Update()
    {
        if (!isLocalPlayer) return;
        if (!is_start_) return;
        count_ += Time.deltaTime;

        if (FRUIT_NUM_ANNOUNCE_TIME == (int)count_)
        {
            //　総数表示
        }


        if (count_ < CHANGE_SCENE_TIME) return;
        if (!isServer) return;
        Application.LoadLevel("result");
        NetworkManager.singleton.StopHost();
    }

    [ClientRpc]
    public void RpcTellClientStart()
    {
        is_start_ = true;
        GameObject.Find("Text (4)").GetComponent<Text>().enabled = true;
    }
}
