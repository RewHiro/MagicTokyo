using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class ResultTest : MonoBehaviour
{
    float count_ = 0;
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            ChangeTitle();
        }

        count_ +=Time.deltaTime;
        if (count_ < 10) return;
        ChangeTitle();
    }

    private void ChangeTitle()
    {
        Application.LoadLevel("title");
        NetworkManager.singleton.StopServer();
    }
}
