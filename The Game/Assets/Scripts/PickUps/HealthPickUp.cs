using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Jincom.PickUps
{
    public class HealthPickUp : PickUpBase
    {
        public int MinHealth = 100;
        public int MaxHealth = 500;

        public override void Collect()
        {
            base.Collect();
            int HealthtoCollect = Random.Range(MinHealth, MaxHealth);
        }
    }
}