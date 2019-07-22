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
        public RaycastHit RayHit;
        public float InitialHeight;
        public bool IsGrounded;
        public Rigidbody RB;

        public float DistanceFromGround;


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

        private void Start()
        {
            InitialHeight = GetComponent<Collider>().bounds.extents.y;
            RB = this.GetComponent<Rigidbody>();

            if (RB != null)
            {
                RB.constraints = RigidbodyConstraints.FreezeRotation | RigidbodyConstraints.FreezePositionZ;
            }
        }

        public virtual void Move(float direction)
        {
            transform.Translate((Vector3.right * direction) * MoveSpeed);
            CurrentSpeed = direction;
        }

        public virtual void Fall()
        {
            Debug.Log("Agent Falls");
        }

        public virtual void Jump(float NewJumpHeight)
        {
            if (RB != null)
            {
                RB.AddForce(new Vector3(0, NewJumpHeight, 0), ForceMode.Impulse);
            }
            else
            {
                Debug.Log("Agent requires a Rigidbody to jump");
            }            
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
      

        public virtual void AnimationState()
        {
            IsGrounded = Physics.Raycast(transform.position, -Vector3.up, InitialHeight + 0.1f);

            if (RB != null)
            {
                if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.down), out RayHit, Mathf.Infinity))
                {
                    Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.down) * RayHit.distance, Color.green);
                    DistanceFromGround = (RayHit.distance - InitialHeight);
                }

                //Main Animation States:
                //Idle
                if ((IsGrounded) && (CurrentSpeed == 0))
                {
                    CurrentState = AgentState.Idle;
                }
                //Walking/Runninng
                else if ((IsGrounded) && (CurrentSpeed != 0))
                {
                    CurrentState = AgentState.Walk;
                }
                //Falling
                else if ((!IsGrounded) && (RB.velocity.y < 0f))
                {
                    CurrentState = AgentState.Fall;
                }
                //Jumping
                else if ((!IsGrounded) && (RB.velocity.y > 0f))
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
        }        
    }
}