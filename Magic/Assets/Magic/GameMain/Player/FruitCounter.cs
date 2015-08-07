﻿using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
using UnityEngine.UI;

public class FruitCounter : NetworkBehaviour
{

    GameObject apple_manager_ = null;
    GameObject lemon_manager_ = null;
    GameObject peach_manager_ = null;
    GameObject egg_plant_manager_ = null;

    int fruit_count_ = 0;
    public int FruitNum { get { return fruit_count_; } }

    int remote_fruit_count_ = 0;
    public int RemoteFruitNum { get { return remote_fruit_count_; } }

    void Start()
    {
        if (!isLocalPlayer) return;
        apple_manager_ = GameObject.Find("AppleManager");
        lemon_manager_ = GameObject.Find("LemonManager");
        peach_manager_ = GameObject.Find("PeachManager");
        egg_plant_manager_ = GameObject.Find("EggPlantManager");
    }

    void Update()
    {
        if (!isLocalPlayer) return;

        var count = apple_manager_.transform.childCount;
        count += lemon_manager_.transform.childCount;
        count += peach_manager_.transform.childCount;
        count += egg_plant_manager_.transform.childCount;

        if (count == fruit_count_) return;
        
        CmdTellToServerCount(count);
        fruit_count_ = count;
    }


    [Command]
    void CmdTellToServerCount(int count)
    {
        fruit_count_ = count;
    }

    [ClientRpc]
    public void RpcTellClientCount(int count)
    {
        remote_fruit_count_ = count;
    }
}