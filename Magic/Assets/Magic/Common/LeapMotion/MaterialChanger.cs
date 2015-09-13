using UnityEngine;
using System.Collections;

public class MaterialChanger : MonoBehaviour
{

    [SerializeField]
    Material material_1p_ = null;

    [SerializeField]
    Material material_2p_ = null;

    bool is_change_ = false;

    // Use this for initialization
    void Start()
    {


    }

    // Update is called once per frame
    void Update()
    {
        if (is_change_) return;
        if (MyNetworkLobbyManager.s_singleton.Is1P)
        {
            GetComponent<MeshRenderer>().material = material_1p_;
        }
        else
        {
            GetComponent<MeshRenderer>().material = material_2p_;
        }
        is_change_ = true;
    }
}
