using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Jincom.Core;

namespace Jincom.Agent
{
    public class EnemyAgent : AgentBase
    {
        private float _initialHeight;
        private bool _isGrounded;
        private Rigidbody _rb;        
        public int CurrentHealth;
        public int MaxHealth;
        public int CurrentArmour;
        public int MaxArmour;
        public GameConstants.Elements CurrentElement;

        private void Start()
        {
            _initialHeight = GetComponent<Collider>().bounds.extents.y;
            _rb = GetComponent<Rigidbody>();
        }

        public void Update()
        {
            AgentUpdate();
        }

        public override void AgentUpdate()
        {
            //Move(Mathf.PingPong(Time.unscaledTime, 4f) - 2f);            
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
