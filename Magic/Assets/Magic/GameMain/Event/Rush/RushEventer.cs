using UnityEngine;
using System.Collections;

public class RushEventer : MonoBehaviour
{
    float time_ = 0.0f;
    bool is_start_ = false;
    public bool IsStart { get { return is_start_; } }

    ParticleSystem game_object_;
    [SerializeField]
    ParticleSystem particleSystem_;

    
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
        GameObject nabe_ = GameObject.Find("Pot");
        GameObject particle_manager_ = GameObject.Find("Ike3ParticleManager");
        game_object_ = Instantiate(particleSystem_);
        game_object_.transform.SetParent(particle_manager_.transform);
        var lid_object = FindObjectOfType<LidControl>().gameObject;
        game_object_.transform.position = lid_object.transform.position;
        game_object_.transform.rotation = lid_object.transform.rotation;
        //game_object_.transform.position = nabe_.transform.position;
        game_object_.name = particleSystem_.name;
    }
}
