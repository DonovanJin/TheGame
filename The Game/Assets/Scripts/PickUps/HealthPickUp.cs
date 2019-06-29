using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Jincom.Agent;

namespace Jincom.PickUps
{
    public class HealthPickUp : PickUpBase
    {
        public int MinHealth = 100;
        public int MaxHealth = 500;

        public override void Spawn()
        {
            base.Spawn();
            
        }

        public override void Collect(PlayerAgent player)
        {
            base.Collect(player);
            int HealthtoCollect = Random.Range(MinHealth, MaxHealth);

            player.PlayerData.AddHealth(HealthtoCollect);

        }
    }
}