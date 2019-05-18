using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Jincom.PickUps
{
    public class XpPickUp : PickUpBase
    {
        public int MinXp = 100;
        public int MaxXp = 500;

        public override void Collect()
        {
            base.Collect();
            int XPtoCollect = Random.Range(MinXp, MaxXp);
        }


    }
}