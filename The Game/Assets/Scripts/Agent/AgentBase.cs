using Jincom.Core;
using UnityEngine;
using System.Collections.Generic;

namespace Jincom.Agent
{
    public abstract class AgentBase : MonoBehaviour
    {
        [Header("Base Agent Properties")]
        public int CurrentHealth;
        //public int CurrentHealth
        //{
        //    get { return _currentHealth; }
        //}
        public int MaxHealth;
        public int CurrentArmour;
        public int MaxArmour;
        public GameConstants.Elements CurrentElement;
        public float MoveSpeed;
        public float JumpHeight;

        public enum AgentState
        {
            Walk,
            Idle,
            Die,
            Fall,
            Jump
        };
        public AgentState CurrentState;

        public abstract void AgentUpdate();

        public virtual void MoveAgent(float direction)
        {
            transform.Translate((Vector3.right * direction) * MoveSpeed);
        }

        public virtual void FallAgent()
        {
            Debug.Log("Agent Falls");
        }

        public virtual void AgentJump()
        {

        }

        public virtual void AgentShoot()
        {
            Debug.Log("Agent Shoots");
        }

        public virtual void AgentThrow()
        {
            Debug.Log("Agent Throws");
        }

        public virtual void AgentMelee()
        {
            Debug.Log("Agent Uses Melee");
        }

        public virtual void AgentClimb()
        {
            Debug.Log("Agent Climbs");
        }

        public virtual void AgentDie()
        {
            Debug.Log("Agent Dies");
        }

        public virtual void AgentSpawn()
        {
            Debug.Log("Agent Spawns");
        }

        public virtual void AgentPickup()
        {
            Debug.Log("Agent Picks Up Something");
        }

        public virtual void AgentPause()
        {
            Debug.Log("Agent Pauses Game");
        }

        public virtual void AgentResume()
        {
            Debug.Log("Agent Resumes Game");
        }
    }
}