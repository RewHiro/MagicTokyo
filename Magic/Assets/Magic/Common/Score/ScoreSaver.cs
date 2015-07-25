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
}
