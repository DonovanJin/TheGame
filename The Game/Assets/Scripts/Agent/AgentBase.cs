using Jincom.Core;
using UnityEngine;
using System.Collections.Generic;

namespace Jincom.Agent
{
    public abstract class AgentBase : MonoBehaviour
    {
        [Header("Base Agent Properties")]
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

        public virtual void Move(float direction)
        {
            transform.Translate((Vector3.right * direction) * MoveSpeed);
        }

        public virtual void Fall()
        {
            Debug.Log("Agent Falls");
        }

        public virtual void Jump()
        {

        }



        public virtual void Spawn()
        {
            Debug.Log("Agent Spawns");
        }

        public virtual void Pause()
        {
            Debug.Log("Agent Pauses Game");
        }

        public virtual void Resume()
        {
            Debug.Log("Agent Resumes Game");
        }
    }
}