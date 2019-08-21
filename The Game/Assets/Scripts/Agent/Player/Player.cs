using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Jincom.Core;
using Jincom.PickUps;

namespace Jincom.Agent
{
    public class Player
    {
        //HERMANN - Jeff, I know the properrties of the player can directly be changed via script, but is there a way to access the properties via the inspector?
        // For example, how can I change the player's health or give him a different weapon during testing? The 'Player' script isn't mono behaviour.

        public int CurrentHealth; 
        public int MaxHealth = 100;
        public int CurrentArmour;
        public int MaxArmour = 100;
        public float JumpHeight = 650f;
        public GameConstants.Elements CurrentElement;
        public WeaponData CurrentWeapon;
        public Dictionary<WeaponData, int> Ammo = new Dictionary<WeaponData, int>();
        public List<GameConstants.Elements> Suits = new List<GameConstants.Elements>();

        public enum AgentState
        {
            Walk,
            Idle,
            Dead,
            Fall,
            Jump
        };
        public AgentState _currentStateOfAgent;

        public Player()
        {
            CurrentHealth = MaxHealth;
            CurrentArmour = MaxArmour;
            Suits.Add(GameConstants.Elements.Normal);
        }

        public void AddHealth(int healthtoCollect)
        {
            int maxToAdd = MaxHealth - CurrentHealth;

            int collectAmount = Mathf.Clamp(healthtoCollect, 0, maxToAdd);

            CurrentHealth += collectAmount;
        }

        public void AddAmmo(WeaponData weapon, int ammotoCollect)
        {
            int maxToAdd = weapon.MaxiumumCapacity - Ammo[weapon];

            int collectAmount = Mathf.Clamp(ammotoCollect, 0, maxToAdd);

            Ammo[weapon] += collectAmount;

            Debug.Log(Ammo[weapon]);
        }

        public void AddWeapon(WeaponData weapon)
        {
            Ammo.Add(weapon, weapon.MaxiumumCapacity);
        }

        public bool CurrentGunHasAmmo()
        {
            if (HasCurrentWeapon())
            {
                return Ammo[CurrentWeapon] > 0;
            }

            return false;
        }

        public void FireCurrentWeapon()
        {
            Ammo[CurrentWeapon]--;
        }

        private bool HasCurrentWeapon()
        {
            return CurrentWeapon != null;
        }
    }
}