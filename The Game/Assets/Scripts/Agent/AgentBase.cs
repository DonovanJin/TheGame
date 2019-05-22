using Jincom.Core;
using Jincom.PickUps;
using UnityEngine;
using System.Collections.Generic;

namespace Jincom.Agent
{
    public abstract class AgentBase : MonoBehaviour
    {
        //How to read health without changing it (read only)
        //public int CurrentHealth
        //{
        //    get { return _currentHealth; }
        //}

        [Header("Base Agent Properties")]
        public int CurrentHealth;        
        public int MaxHealth;
        public int CurrentArmour;
        public int MaxArmour;
        public int CurrentShield;
        public int MaxShield;
        public GameConstants.Elements CurrentElement;
        public WeaponData Weapon;
        public float MoveSpeed;
        public float JumpHeight;
        public bool Attacking = false;
        public bool CanShoot = true;
        public Rigidbody Rb;

        //NOTE: Attacking is a State that informs the animation. CanShoot and AgentShoot() is the act itself.
        
        public enum AgentState
        {
            Walk,
            Idle,
            Die,            
            Fall,
            Jump
        };
        public AgentState CurrentState;

        public enum AgentFacingDirection
        {
            Left,
            Right
        };
        public AgentFacingDirection FacingLeftOrRight;

        public abstract void AgentUpdate();

        private void Start()
        {
            Rb = GetComponent<Rigidbody>();
        }

        public virtual void StateOfAgent()
        {
            //Grounded
            if (Physics.Raycast(transform.position, -Vector3.up, GetComponent<Collider>().bounds.extents.y + 0.1f))
            {
                if (Input.GetAxis("Horizontal") != 0)
                {
                    CurrentState = AgentState.Walk;
                }
                else
                {
                    CurrentState = AgentState.Idle;
                }
            }
            //Not Grounded
            else
            {
                if (Rb.velocity.y < 0)
                {
                    CurrentState = AgentState.Fall;
                }
                else 
                {
                    CurrentState = AgentState.Jump;
                }
            }
        }

        public virtual void AgentStayUpright()
        {
            this.gameObject.transform.position = new Vector3(this.gameObject.transform.position.x, this.gameObject.transform.position.y, 0);
            this.gameObject.transform.eulerAngles = new Vector3(0, 0, 0);
        }

        public virtual void MoveAgent(float direction)
        {
            transform.Translate((Vector3.right * direction) * MoveSpeed);    
            
            if (direction < 0)
            {
                FacingLeftOrRight = AgentFacingDirection.Left;
            }
            else if (direction > 0)
            {
                FacingLeftOrRight = AgentFacingDirection.Right;
            }
        }

        public virtual void FallAgent()
        {
            Debug.Log("Agent Falls");
        }

        public virtual void JumpAgent(float NewJumpJeight)
        {
            //Rb.velocity = new Vector3(0f, NewJumpJeight, 0f);
            Rb.AddForce(Vector3.up * NewJumpJeight, ForceMode.Impulse);
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

        public virtual void AgentAttack(bool YesNo)
        {
            if (YesNo)
            {
                Attacking = true;
            }
            else
            {
                Attacking = false;
            }
        }

        public virtual void AgentShoot()
        {            
            Debug.Log(name + " is Shooting");            
        }
    }
}