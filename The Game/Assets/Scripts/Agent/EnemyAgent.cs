using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Jincom.Core;

namespace Jincom.Agent
{
    public class EnemyAgent : AgentBase
    {
        public int CurrentHealth;
        public int MaxHealth;
        public int CurrentArmour;
        public int MaxArmour;
        public GameConstants.Elements CurrentElement;

        public void Update()
        {
            AgentUpdate();
        }

        public override void AgentUpdate()
        {
            Move(Mathf.PingPong(Time.unscaledTime, 4f) - 2f);
        }

        public virtual void Shoot()
        {
            Debug.Log("Agent Shoots");
        }

        public virtual void Throw()
        {
            Debug.Log("Agent Throws");
        }

        public virtual void Melee()
        {
            Debug.Log("Agent Uses Melee");
        }



        public virtual void Die()
        {
            Debug.Log("Agent Dies");
        }
    }
}
