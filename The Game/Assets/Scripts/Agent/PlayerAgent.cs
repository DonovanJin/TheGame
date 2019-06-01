using System;
using System.Collections;
using System.Collections.Generic;
using Jincom.PickUps;
using UnityEngine;

namespace Jincom.Agent
{
    public class PlayerAgent : AgentBase
    {
        //Transfer to dictionary
        public WeaponData CurrentWeapon;
        public Dictionary<WeaponData, int> Weapons = new Dictionary<WeaponData, int>();
  

        public void Update()
        {
            AgentUpdate();
        }

        public override void AgentUpdate()
        {
            PlayerMovement();
            PlayerJump();
        }

        internal void AddAmmo(WeaponData weapon, int ammotoCollect)
        {
            int maxToAdd = weapon.MaxiumumCapacity - Weapons[weapon];

            int collectAmount = Mathf.Clamp(ammotoCollect, 0, maxToAdd);

            Weapons[weapon] += collectAmount;

            Debug.Log(Weapons[weapon]);
        }

        internal void AddWeapon(WeaponData weapon)
        {
            Weapons.Add(weapon, weapon.MaxiumumCapacity);
        }

        private void PlayerMovement()
        {
            //Movement
            if (Input.GetAxis("Horizontal") != 0)
            {
                MoveAgent(Input.GetAxis("Horizontal"));
            }
        }

        internal void AddHealth(int healthtoCollect)
        {
            int maxToAdd = MaxHealth - CurrentHealth;

            int collectAmount = Mathf.Clamp(healthtoCollect, 0, maxToAdd);

            CurrentHealth += collectAmount;
        }

        private void PlayerJump()
        {

        }

        private void PlayerAttack()
        {

        }

    }
}
