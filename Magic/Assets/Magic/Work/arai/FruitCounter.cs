using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
using UnityEngine.UI;

public class FruitCounter : NetworkBehaviour
{

    GameObject apple_manager_ = null;
    GameObject lemon_manager_ = null;
    GameObject peach_manager_ = null;

    Text text_;

    int fruit_count_ = 0;

    // Use this for initialization
    void Start()
    {
        if (!isLocalPlayer) return;
        apple_manager_ = GameObject.Find("AppleManager");
        lemon_manager_ = GameObject.Find("LemonManager");
        peach_manager_ = GameObject.Find("PeachManager");
        text_ = GameObject.Find("Text").GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!isLocalPlayer) return;

        var count = apple_manager_.transform.childCount;
        count += lemon_manager_.transform.childCount;
        count += peach_manager_.transform.childCount;
        //CmdTellToServerCount(count);
        fruit_count_ = count;
        //Debug.Log(netId.Value + ":" + count.ToString());
        text_.text = count.ToString();
    }


    //[Command]
    //void CmdTellToServerCount(int count)
    //{
    //    fruit_count_ = count;
    //}
}
