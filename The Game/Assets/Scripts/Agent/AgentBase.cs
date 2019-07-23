using Jincom.Core;
using UnityEngine;
using System.Collections.Generic;

namespace Jincom.Agent
{
    public abstract class AgentBase : MonoBehaviour
    {
        [Header("Base Agent Properties")]
        public GameConstants.Elements CurrentElement;
        public int CurrentHealth;
        public int MaxHealth;
        public int CurrentArmour;
        public int MaxArmour;        
        public float MoveSpeed;
        public float JumpHeight;
        public float CurrentSpeed;
        public RaycastHit RayHit;               
        public Rigidbody RB;
        public bool IsGrounded;
        public float Acceleration = 0f;
        public bool CanShoot;
        public float TimeBetweenEachShotInSeconds;

        private float _oldHorPos = 0f;
        private float _newHorPos = 0f;                
        private float _initialHeight;

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
            _initialHeight = GetComponent<Collider>().bounds.extents.y;
            RB = this.GetComponent<Rigidbody>();

            if (RB != null)
            {
                RB.constraints = RigidbodyConstraints.FreezeRotation | RigidbodyConstraints.FreezePositionZ;                
            }

            _oldHorPos = transform.position.x;
            _newHorPos = transform.position.x;
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
            _newHorPos = transform.position.x;

            if (_oldHorPos != _newHorPos)
            {
                Acceleration = _newHorPos - _oldHorPos;
                _oldHorPos = _newHorPos;
            }
            else
            {
                Acceleration = 0f;
            }

            IsGrounded = Physics.Raycast(transform.position, -Vector3.up, _initialHeight + 0.1f);

            
                if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.down), out RayHit, Mathf.Infinity))
                {
                    Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.down) * RayHit.distance, Color.green);
                    DistanceFromGround = (RayHit.distance - _initialHeight);
                }

            //Main Animation States:

            if (IsGrounded)
            {
                //Idle
                if (Acceleration == 0)
                {
                    CurrentState = AgentState.Idle;
                }
                //Walking/Runninng
                else 
                {
                    CurrentState = AgentState.Walk;
                }
            }

            else
            {
                if (RB != null)
                {
                    //Falling
                    if (RB.velocity.y < 0f)
                    {
                        CurrentState = AgentState.Fall;
                    }
                    //Jumping
                    else if (RB.velocity.y > 0f)
                    {
                        CurrentState = AgentState.Jump;
                    }
                }
            }             
        } 
        
        public virtual void AgentShoot()
        {
            Debug.Log(name + " is shooting. Pew pew!");
        }
    }
}