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
        public float TimeBetweenEachShotInSeconds;
        public float TimeFalling;
        public bool HitFloor;
        //public bool MovingUpOrDown = false;
        public bool CanShoot = true;

        //Temp
        public float CaptureTime;
        //

        protected RaycastHit RayHit;
        protected Rigidbody RB;
        protected bool IsGrounded;
        protected float HorAcceleration = 0f;
        //public float VertAcceleration = 0f;
        protected float _initialWidth;

        protected float _oldHorPos = 0f;
        protected float _newHorPos = 0f;
        protected float _oldVertPos = 0f;
        protected float _newVertPos = 0f;
        private float _initialHeight;        

        public enum AgentState
        {
            Walk,
            Idle,
            Dead,
            Fall,
            Jump
        };
        public AgentState _currentState;

        public enum FacingDirection
        {
            Left,
            Right
        };
        public FacingDirection Facing;

        //  =   =   =   =   =   =   =   =   =   =   =   =

        public abstract void AgentUpdate();

        //  =   =   =   =   =   =   =   =   =   =   =   =

        private void Start()
        {
            _initialHeight = GetComponent<Collider>().bounds.extents.y;
            _initialWidth = GetComponent<Collider>().bounds.extents.x;

            RB = this.GetComponent<Rigidbody>();

            if (RB != null)
            {
                RB.constraints = RigidbodyConstraints.FreezeRotation | RigidbodyConstraints.FreezePositionZ;                
            }

            _oldHorPos = transform.position.x;
            _newHorPos = transform.position.x;

            _oldVertPos = transform.position.y;
            _newVertPos = transform.position.y;

            //This should be in another script
            Cursor.visible = false;
        }

        //  =   =   =   =   =   =   =   =   =   =   =   =

        /// <summary>
        /// Move agents allong the world x axis.
        /// </summary>
        /// <param name="direction"> Pass through a float value ranging from -1 to 1 </param>
        public virtual void Move(float direction)
        {
            if (CurrentHealth > 0f)
            {
                transform.Translate((Vector3.right * direction) * MoveSpeed);
                CurrentSpeed = direction;
            }
        }

        //  =   =   =   =   =   =   =   =   =   =   =   =

        public virtual void Fall()
        {
            _newVertPos = transform.position.y;

            //if (_newVertPos != _oldVertPos)
            //{
            //    MovingUpOrDown = true;
            //}
            //else
            //{
            //    MovingUpOrDown = false;
            //}

            if (_oldVertPos > _newVertPos)
            {                
                if (!IsGrounded)
                {
                    TimeFalling += Time.deltaTime;
                    HitFloor = false;
                }                
            }
            else if (_oldVertPos == _newVertPos)
            {                
                if (IsGrounded)
                {
                    if (!HitFloor)
                    {
                        CaptureTime = TimeFalling;
                        TimeFalling = 0f;
                        HitFloor = true;

                        if (CaptureTime > 1f)
                        {
                            Debug.Log("Fall Damage modifier: " + (CaptureTime - 1f));
                        }
                    }
                }
            }

            _oldVertPos = _newVertPos;
        }

        //  =   =   =   =   =   =   =   =   =   =   =   =

        /// <summary>
        /// Propell agent into the air.
        /// </summary>
        /// <param name="NewJumpHeight"> Pass a float value </param>
        public virtual void Jump(float NewJumpHeight)
        {
            if (CurrentHealth > 0f)
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
        }

        //  =   =   =   =   =   =   =   =   =   =   =   =

        public virtual void Spawn()
        {
            Debug.Log("Agent Spawns");
        }

        //  =   =   =   =   =   =   =   =   =   =   =   =

        public virtual void Pause()
        {
            Debug.Log("Agent Pauses Game");
        }

        //  =   =   =   =   =   =   =   =   =   =   =   =

        public virtual void Resume()
        {
            Debug.Log("Agent Resumes Game");
        }

        //  =   =   =   =   =   =   =   =   =   =   =   =

        public virtual void AnimationState()
        {
            // Calculates if agent is currently travelling left/right or standing still
            _newHorPos = transform.position.x;

            if (_oldHorPos != _newHorPos)
            {
                HorAcceleration = _newHorPos - _oldHorPos;
                _oldHorPos = _newHorPos;
            }
            else
            {
                HorAcceleration = 0f;
            }
            // 

            

            IsGrounded = Physics.Raycast(transform.position, -Vector3.up, _initialHeight + 0.1f);
            
            // Main Animation States:
            // Still Alive
            if (CurrentHealth > 0f)
            {
                if (IsGrounded)
                {
                    //Idle
                    if (HorAcceleration == 0)
                    {
                        _currentState = AgentState.Idle;
                    }
                    //Walking/Runninng
                    else
                    {
                        _currentState = AgentState.Walk;
                    }
                }

                else
                {
                    if (RB != null)
                    {
                        //Falling
                        if (RB.velocity.y < 0f)
                        {
                            _currentState = AgentState.Fall;
                        }
                        //Jumping
                        else if (RB.velocity.y > 0f)
                        {
                            _currentState = AgentState.Jump;
                        }
                    }
                }
            }
            else
            {
                _currentState = AgentState.Dead;
            }
            //
        }

        //  =   =   =   =   =   =   =   =   =   =   =   =

        public virtual void AgentShoot()
        {
            Debug.Log(name + " is shooting. Pew pew!");
        }
    }
}