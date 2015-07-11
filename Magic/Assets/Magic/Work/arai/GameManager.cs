using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class GameManager : MonoBehaviour
{
    float count_ = 0;

    [SerializeField]
    int LIMIT_TIME = 2;

    void Update()
    {
        count_ += Time.deltaTime;
        if (count_ < LIMIT_TIME) return;
        if (Input.GetMouseButtonDown(1))
        {
            NetworkManager.singleton.StopHost();
            Application.LoadLevel("result");
        }
    }
}
