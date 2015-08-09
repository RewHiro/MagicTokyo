using UnityEngine;
using System.Collections;

public class ScoreSaver : MonoBehaviour
{

    int fruit_num_ = 0;
    public int FruitNum { get { return fruit_num_; } set { fruit_num_ = value; } }

    int remote_fruit_num_ = 0;
    public int RemoteFruitNum { get { return fruit_num_; } set { fruit_num_ = value; } }

    bool is_1p_ = true;
    public bool Is1P { get { return is_1p_; } set { is_1p_ = value; } }

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
