using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
using UnityEngine.UI;

public class NetworkManagerUtility : MonoBehaviour {

    NetworkManager network_manager_;

    [SerializeField]
    GameObject text_;

    void Awake()
    {
        network_manager_ = GetComponent<NetworkManager>();
    }

    public void StartHost()
    {
        network_manager_.StartHost();
    }

    public void StartClient()
    {
        network_manager_.networkAddress = "192.168.2.100";
        network_manager_.StartClient();
        text_.GetComponent<Text>().enabled = true;
    }
}
