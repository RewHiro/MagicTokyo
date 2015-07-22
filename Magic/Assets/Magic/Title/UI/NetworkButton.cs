using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class NetworkButton : MonoBehaviour
{
    void Start()
    {
        var network_manager = GameObject.Find("NetworkManager").GetComponent<MyNetworkManager>();
        var button = GetComponent<Button>();
        button.onClick.AddListener(network_manager.GameStart);
    }
}
