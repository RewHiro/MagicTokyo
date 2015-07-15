using UnityEngine;
using System.Collections;

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
            Application.LoadLevel("title");
        }

        count_ +=Time.deltaTime;
        if (count_ < 10) return;
        Application.LoadLevel("title");
    }
}
