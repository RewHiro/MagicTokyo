using UnityEngine;
using System.Collections;

public class ScoreSaver : MonoBehaviour
{

    readonly int TITLE_HASH_CODE = "title".GetHashCode();
    readonly int YANAI_TITLE_HASH_CODE = "yanai_title".GetHashCode();

    int fruit_num_ = 0;
    public int FruitNum { get { return fruit_num_; } set { fruit_num_ = value; } }

    int remote_fruit_num_ = 0;
    public int RemoteFruitNum { get { return remote_fruit_num_; } set { remote_fruit_num_ = value; } }

    bool is_1p_ = true;
    public bool Is1P { get { return is_1p_; } set { is_1p_ = value; } }

    void Awake()
    {
        DontDestroyOnLoad(this);
    }

    void Update()
    {
        var scene_hash_code = Application.loadedLevelName.GetHashCode();
        if (!(TITLE_HASH_CODE == scene_hash_code ||
            YANAI_TITLE_HASH_CODE == scene_hash_code))
            return;
        Destroy(this);
    }
}
