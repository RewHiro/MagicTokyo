using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class PlayerDamage : NetworkBehaviour {

    [SyncVar]
    bool is_damage_ = false;

    [SerializeField]
    GameObject apple;

	// Use this for initialization
    void Start()
    {

    }
	
	// Update is called once per frame
    void Update()
    {
        if (isLocalPlayer)
        {
            CmdTellToServerDamage(FindObjectOfType<HandController>().IsAttack);
        }
        else
        {
            if (is_damage_)
            {
                for (int i = 0; i < 5; ++i)
                {
                    Instantiate(apple);
                }
            }
        }
    }
    [Command]
    void CmdTellToServerDamage(bool is_damage)
    {
        is_damage_ = is_damage;
    }
}
