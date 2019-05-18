using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Jincom.Core;

namespace Jincom.PickUps
{
    public class AmmoPickUp : PickUpBase
    {

        public WeaponData Weapon;
       
        public int MinAmmo = 100;
        public int MaxAmmo = 500;

        public override void Collect()
        {
            base.Collect();
            int AmmotoCollect = Random.Range(MinAmmo, MaxAmmo);

        }
    }
}

