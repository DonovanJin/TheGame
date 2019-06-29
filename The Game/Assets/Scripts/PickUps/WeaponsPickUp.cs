using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Jincom.Core;
using Jincom.Agent;

namespace Jincom.PickUps
{
    public class WeaponsPickUp : PickUpBase
    {
        public WeaponData Weapon;

        public override void Collect(PlayerAgent player)
        {
            base.Collect(player);
            player.PlayerData.AddWeapon(Weapon);
        }
    }
}


