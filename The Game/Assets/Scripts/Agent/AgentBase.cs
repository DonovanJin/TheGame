using Jincom.Core;
using UnityEngine;
using System.Collections.Generic;

namespace Jincom.Agent
{
    public abstract class AgentBase : MonoBehaviour
    {
        [Header("Base Agent Properties")]
        public GameConstants.Elements CurrentElement;     
        public float MoveSpeed;
        public bool CanShoot = true;
                
        protected Rigidbody RB;               

        //public enum AgentState
        //{
        //    Walk,
        //    Idle,
        //    Dead,
        //    Fall,
        //    Jump
        //};
        //public AgentState _currentState;
               

        //  =   =   =   =   =   =   =   =   =   =   =   =

        public abstract void AgentUpdate();

        //  =   =   =   =   =   =   =   =   =   =   =   =

        private void Start()
        {
            
        }

        //  =   =   =   =   =   =   =   =   =   =   =   =

        public virtual void Move(float direction, float direction2)
        {
            transform.Translate((Vector3.right * direction) * MoveSpeed);
            transform.Translate((Vector3.up * direction2) * MoveSpeed);
        }

        //  =   =   =   =   =   =   =   =   =   =   =   =

        //public virtual void Fall()
        //{
        //    _newVertPos = transform.position.y;

        //    //Player is descending
        //    if (_oldVertPos > _newVertPos)
        //    {           
        //        //Player is NOT on the ground
        //        if (!Physics.Raycast(transform.position, -Vector3.up, GetComponent<Collider>().bounds.extents.y + 0.1f))
        //        {
        //            TimeFalling += Time.deltaTime;
        //            HitFloor = false;
        //        }                
        //    }
        //    //Player is NOT descending
        //    else if (_oldVertPos == _newVertPos)
        //    {
        //        //Player is on the ground
        //        if (Physics.Raycast(transform.position, -Vector3.up, GetComponent<Collider>().bounds.extents.y + 0.1f))
        //        {
        //            if (!HitFloor)
        //            {                      
        //                HitFloor = true;

        //                if ((TimeFalling > 1f) && (TimeFalling < 3f))
        //                {
        //                    Debug.Log("Fall Damage modifier: " + (TimeFalling - 1f));
        //                }
        //                else if (TimeFalling > 3f)
        //                {
        //                    Debug.Log("Player dies from falling too far.");
        //                }

        //                TimeFalling = 0f;
        //            }
        //        }
        //    }

        //    _oldVertPos = _newVertPos;
        //}

        //  =   =   =   =   =   =   =   =   =   =   =   =
                
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

        //public virtual void AnimationState()
        //{            
        //    if (CurrentHealth > 0f)
        //    {
        //        if (Physics.Raycast(transform.position, -Vector3.up, GetComponent<Collider>().bounds.extents.y + 0.1f))
        //        {
        //            //Idle
        //            if (Input.GetAxis("Horizontal") == 0)
        //            {
        //                _currentState = AgentState.Idle;
        //            }
        //            //Walking/Runninng
        //            else
        //            {
        //                _currentState = AgentState.Walk;
        //            }
        //        }

        //        else
        //        {
        //            if (RB != null)
        //            {
        //                //Falling
        //                if (RB.velocity.y < 0f)
        //                {
        //                    _currentState = AgentState.Fall;
        //                }
        //                //Jumping
        //                else if (RB.velocity.y > 0f)
        //                {
        //                    _currentState = AgentState.Jump;
        //                }
        //            }
        //        }
        //    }
        //    else
        //    {
        //        _currentState = AgentState.Dead;
        //    }           
        //}

        //  =   =   =   =   =   =   =   =   =   =   =   =

        public virtual void AgentShoot()
        {
            Debug.Log(name + " is shooting. Pew pew!");
        }
    }
}