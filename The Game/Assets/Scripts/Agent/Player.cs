using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Jincom.Core;
using Jincom.PickUps;

namespace Jincom.Agent
{
    public class Player
    {
        public int CurrentHealth;
        public int MaxHealth;
        public int CurrentArmour;
        public int MaxArmour;
        public GameConstants.Elements CurrentElement;
        public WeaponData CurrentWeapon;
        public Dictionary<WeaponData, int> Weapons = new Dictionary<WeaponData, int>();

        public void AddHealth(int healthtoCollect)
        {
            int maxToAdd = MaxHealth - CurrentHealth;

            int collectAmount = Mathf.Clamp(healthtoCollect, 0, maxToAdd);

            CurrentHealth += collectAmount;
        }

        public void AddAmmo(WeaponData weapon, int ammotoCollect)
        {
            int maxToAdd = weapon.MaxiumumCapacity - Weapons[weapon];

            int collectAmount = Mathf.Clamp(ammotoCollect, 0, maxToAdd);

            Weapons[weapon] += collectAmount;

            Debug.Log(Weapons[weapon]);
        }

        public void AddWeapon(WeaponData weapon)
        {
            Weapons.Add(weapon, weapon.MaxiumumCapacity);
        }
    }
}