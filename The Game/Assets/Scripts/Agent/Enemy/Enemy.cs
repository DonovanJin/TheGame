using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Jincom.Core;
using Jincom.PickUps;

namespace Jincom.Agent
{
    public class Enemy
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
            //Generic actions
            Idle,
            Walking,
            Dead,
            Fall,
            Jump,

            //Worm actions
            Strangling,
            Spitting
        };
        public AgentState _currentStateOfAgent;

        public enum EnemyBehaviour
        {
            Idle,
            Suspicious,
            Attacking,
            Chasing,
            Performing
        };

        public Enemy()
        {
            CurrentHealth = MaxHealth;
            CurrentArmour = MaxArmour;
        }
    }
}
