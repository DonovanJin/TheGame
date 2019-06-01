using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Jincom.Agent;

namespace Jincom.PickUps
{
    public class XpPickUp : PickUpBase
    {
        public int MinXp = 100;
        public int MaxXp = 500;

        public override void Collect(PlayerAgent player)
        {
            base.Collect(player);
            int XPtoCollect = Random.Range(MinXp, MaxXp);
        }


    }
}