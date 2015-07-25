using UnityEngine;
using System.Collections;

public class MyRigidHand : RiggedHand {

    GameStartDirector game_start_director = null;

    public override void InitHand()
    {
        //var players = FindObjectsOfType<PlayerSetting>();
        //foreach (var player in players)
        //{
        //    if (!player.isLocalPlayer) continue;
        //    game_start_director = player.GetComponent<GameStartDirector>();
        //}
        base.InitHand();
    }

    public override void UpdateHand()
    {
        //if (!game_start_director.IsStart) return;
        base.UpdateHand();
    }
}
