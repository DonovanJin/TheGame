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
        public float CurrentSpeed;

        public enum AgentState
        {
            Walk,
            Idle,
            Die,
            Fall,
            Jump
        };
        public AgentState CurrentState;

        public enum FacingDirection
        {
            Left,
            Right
        };
        public FacingDirection facing;

        public abstract void AgentUpdate();

        public virtual void Move(float direction)
        {
            transform.Translate((Vector3.right * direction) * MoveSpeed);
            CurrentSpeed = direction;
        }

        public virtual void Fall()
        {
            Debug.Log("Agent Falls");
        }

        public virtual void Jump(float NewJummpHeight)
        {
            // Hermann
            Rigidbody RB = this.GetComponent<Rigidbody>();

            if (RB != null)
            {
                RB.AddForce(new Vector3(0, NewJummpHeight, 0), ForceMode.Impulse);
            }
            else
            {
                Debug.Log("Agent requires a Rigidbody to jump");
            }
            //
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

        //Hermann        

        public virtual void AnimationState(bool Grounded, Rigidbody RB)
        {
            //Prevents agents from tumbling over and keeps them at the correct depth
            transform.position = new Vector3(transform.position.x, transform.position.y, 0f);
            transform.rotation = new Quaternion(0f, 0f, 0f, 0f);

            //Animation State
            if ((Grounded) && (Input.GetAxis("Horizontal") == 0))
            {
                CurrentState = AgentState.Idle;
            }
            else if ((Grounded) && (Input.GetAxis("Horizontal") != 0))
            {
                CurrentState = AgentState.Walk;
            }
            else if ((!Grounded) && (RB.velocity.y < 0f))
            {
                CurrentState = AgentState.Fall;
            }
            else if ((!Grounded) && (RB.velocity.y > 0f))
            {
                CurrentState = AgentState.Jump;
            }

            //Facing direction
            if (CurrentSpeed < 0f)
            {
                //Debug.Log("Left?");
                facing = FacingDirection.Left;
            }
            else if (CurrentSpeed > 0f)
            {
                //Debug.Log("Right?");
                facing = FacingDirection.Right;
            }
        }
        //
    }
}