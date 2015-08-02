using UnityEngine;
using System.Collections;

public class MyRigidHand : RiggedHand {

    GameStartDirector game_start_director = null;

    [SerializeField]
    bool Debug = false;

    void Awake()
    {
        foreach (var player in FindObjectsOfType<PlayerSetting>())
        {
            if (!player.isLocalPlayer) continue;
            game_start_director = player.GetComponent<GameStartDirector>();
        }
    }

    public override void InitHand()
    {
        if (!Debug)
        {
            if (Application.loadedLevelName == "gamemain")
            {
                if (!game_start_director.IsStart) return;
            }
        }

        base.InitHand();
    }

    public override void UpdateHand()
    {
        if (!Debug)
        {
            if (Application.loadedLevelName == "gamemain")
            {
                if (!game_start_director.IsStart) return;
            }
        }

        base.UpdateHand();
    }
}
