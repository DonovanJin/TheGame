using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Jincom.Core;
using Jincom.PickUps;

namespace Jincom.Agent
{
    public class Worm
    {
        public int CurrentHealth;
        public int MaxHealth = 100;
        public int CurrentArmour;
        public int MaxArmour = 100;
        public float JumpHeight = 650f;
        public GameConstants.Elements CurrentElement;
        public WeaponData CurrentWeapon;

        public enum AgentState
        {
            Idle,
            Slithering,            
            Dead,
            Fall,
            Jump,
            Strangling,
            Spitting
        };
        public AgentState _currentStateOfAgent;

        public enum EnemyBehaviour
        {
            Idle,

        };

        public Worm()
        {
            CurrentHealth = MaxHealth;
            CurrentArmour = MaxArmour;
        }
    }
}
