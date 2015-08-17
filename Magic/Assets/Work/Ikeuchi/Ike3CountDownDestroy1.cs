using UnityEngine;
using System.Collections;

public class Ike3CountDownDestroy1 : MonoBehaviour {

    ParticleSystem particle_;

    [SerializeField
   , TooltipAttribute("ここに「Ike3ParticleManager」prefabを入れてください\n(プログラマー用)")]
    private GameObject particle_manager_;

    [SerializeField
    , TooltipAttribute("次に表示させたいパーティクルを入れてください「3」→「2」→「1」→「Start」")]
    private ParticleSystem particle_next_;

    void Start()
    {
        Init();
    }

    void Update()
    {
        ParticleDestroy();
    }

    void Init()
    {
        particle_ = GetComponent<ParticleSystem>();
    }

    void ParticleDestroy()
    {
        if (particle_)
        {
            if (!particle_.IsAlive())
            {
                Destroy(gameObject);

                if (particle_next_ != null)
                {
                    GameObject particle_manager = GameObject.Find(particle_manager_.name);
                    ParticleSystem game_object = Instantiate(particle_next_);
                    game_object.transform.SetParent(particle_manager.transform);
                    game_object.transform.position = transform.position;
                    game_object.name = particle_next_.name;
                }
            }
        }
        else
        {
            Debug.Log("これは「ParticleSystem」じゃないです");
        }
    }
}
