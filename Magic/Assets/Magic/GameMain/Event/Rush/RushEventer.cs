using UnityEngine;
using System.Collections;

public class RushEventer : MonoBehaviour
{
    float time_ = 0.0f;
    bool is_start_ = false;
    public bool IsStart { get { return is_start_; } }


    void Update()
    {
        if (!is_start_) return;
        time_ += Time.deltaTime;
        if (time_ <= 5.0f) return;
        is_start_ = false;
    }

    public void StartEvent()
    {
        is_start_ = true;
        time_ = 0.0f;
    }
}
