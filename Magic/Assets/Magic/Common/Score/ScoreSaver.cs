using UnityEngine;
using System.Collections;

public class ScoreSaver : MonoBehaviour
{

    int fruit_num_ = 0;
    public int FruitNum { get { return fruit_num_; } set { fruit_num_ = value; } }

    void Awake()
    {
        DontDestroyOnLoad(this);
    }

    void Update()
    {
        if (Application.loadedLevelName != "title") return;
        Destroy(this);
    }
}
