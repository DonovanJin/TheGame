using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Jincom.Core;
using Jincom.Agent;

namespace Jincom.PickUps
{
    public class AmmoPickUp : PickUpBase
    {

        public WeaponData Weapon;
       
        public int MinAmmo = 100;
        public int MaxAmmo = 500;

        public override void Collect(PlayerAgent player)
        {
            base.Collect(player);
            int AmmotoCollect = Random.Range(MinAmmo, MaxAmmo);
            player.PlayerData.AddAmmo(Weapon, AmmotoCollect);
        }
    }
}

