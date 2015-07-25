using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
using UnityEngine.UI;

public class TimeLimitter : NetworkBehaviour
{
    [SerializeField,Range(1,60),Tooltip("制限時間")]
    int count_ = 60;
    public int LimitCount { get { return count_; } }


    void Start()
    {
    }

    void Update()
    {
        if (!isLocalPlayer) return;
    }

    [ClientRpc]
    public void RpcTellClientLimitCount(int count)
    {
        count_ = count;
    }
}
