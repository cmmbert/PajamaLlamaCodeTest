using Platformer.Core;
using Platformer.Model;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHurt : Simulation.Event<PlayerHurt>
{
    PlatformerModel model = Simulation.GetModel<PlatformerModel>();
    public Vector3 BounceVector = new(0, 5, 0); //Bounce functionality does not bounce sideways for some reason so we can only go up
    public override void Execute()
    {
        var player = model.player;
        if (player.health.IsAlive)
        {
            player.health.Decrement();
            player.Bounce(BounceVector);
        }
    }
}
