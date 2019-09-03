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
        public int MaxHealth = 100;
        public int CurrentArmour;
        public int MaxArmour = 100;
        public float JumpHeight = 450f;
        public float RunAcceleration = 0.2f;
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

        public void SwitchToWeapon(WeaponData nextWeapon)
        {
            if (Ammo.ContainsKey(nextWeapon))
            {
                CurrentWeapon = nextWeapon;
                Debug.Log("Current weapon is now: " + CurrentWeapon.ToString());
            }
            else
            {
                Debug.Log("Player does not have that weapon yet");
            }
        }
    }
}