using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Jincom.Core;
using Jincom.PickUps;
using System;

namespace Jincom.Agent
{
    abstract public class EnemyAgent : AgentBase
    {

        //public float TEMPcolliderWidth;
        //public float TEMPdistanceToSurface;
        [SerializeField]
        private float endWaitTime = 0f;
        [SerializeField]
        private float TimeToWait = 2f;

        public int CurrentHealth;
        public int MaxHealth = 100;
        public int CurrentArmour;
        public int MaxArmour = 100;
        public float JumpHeight = 650f;
        public GameConstants.Elements ArmourElement;
        public WeaponData Weapon;

        public Transform PlayerTransform;
        public float SpotDistance = 10f;
        private RaycastHit _rayHit;

        private float _horizontalDifference;

        internal void Init()
        {
            CurrentEnemyBehaviour = EnemyBehaviour.Patrolling;
        }

        public EnemyAgent()
        {
            CurrentHealth = MaxHealth;
            CurrentArmour = MaxArmour;
        }

        public enum FacingDirection
        {
            Left,
            Right
        };
        public FacingDirection Facing;

        //This is the action the enemy is currently performing and is mostly to inform the animation
        public enum AgentState
        {
            //Generic actions
            Idle,
            Walking,
            Dead,
            Fall,
            Jump,

            //Worm actions
            Strangling,
            Spitting
        };
        public AgentState CurrentStateOfAgent;

        //The state of the enemy's mind. This informs what type of AI to employ.
        public enum EnemyBehaviour
        {
            Idle,
            Patrolling,
            Suspicious,
            Attacking,
            Chasing,
            Performing
        };

        public EnemyBehaviour CurrentEnemyBehaviour;

        //  =   =   =   =   =   =   =   =   =   =   =   =

        public override void AgentUpdate()
        {            
            AcquirePlayer();
            EnemyAI();
        }

        //  =   =   =   =   =   =   =   =   =   =   =   =

        public abstract void EnemyAI();

        public virtual void AcquirePlayer()
        {
            if (CurrentEnemyBehaviour != EnemyBehaviour.Performing)
            {
                if (!PlayerTransform)
                {
                    PlayerTransform = GameObject.FindGameObjectWithTag("Player").gameObject.GetComponent<Transform>();
                }
            }
        }

        //  =   =   =   =   =   =   =   =   =   =   =   =

        public virtual bool PlayerSpotted()
        {
            _horizontalDifference = transform.position.x - PlayerTransform.position.x;

            if (Physics.Raycast(transform.position, PlayerTransform.position - transform.position, out _rayHit))
            {
                if (_rayHit.transform == PlayerTransform)
                {
                    if (Vector3.Distance(transform.position, PlayerTransform.position) < SpotDistance)
                    {
                        if ((_horizontalDifference > 0) && (Facing == FacingDirection.Left))
                        {
                            return true;
                        }
                        else if ((_horizontalDifference < 0) && (Facing == FacingDirection.Right))
                        {
                            return true;
                        }
                        else
                        {
                            return false;
                        }
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }

        //  =   =   =   =   =   =   =   =   =   =   =   =

        public virtual void Throw()
        {
            Debug.Log("Agent Throws");
        }

        //  =   =   =   =   =   =   =   =   =   =   =   =

        public virtual void Melee()
        {
            Debug.Log("Agent Uses Melee");
        }

        //  =   =   =   =   =   =   =   =   =   =   =   =

        public virtual void Die()
        {
            Debug.Log("Agent Dies");
        }

        //  =   =   =   =   =   =   =   =   =   =   =   =

        private void EnemyShoot()
        {
            if (PlayerSpotted())
            {
                //if Enemy hasn't shot within the last x amount of time, allow the enemy to shoot again
                Debug.Log("Bang, bang, bang!");
            }
        }

        //  =   =   =   =   =   =   =   =   =   =   =   =

        public bool EnemyObstructed()
        {
            //Returns true if there is a collider directly in front of where the enemy is facing
            //Draws a ray if there is a collider in the direction the enemy is facing further on, returns false
            //If there is no collider in front of the enemy, returns false
            if (Facing == FacingDirection.Left)
            {
                if (Physics.Raycast(transform.position, Vector3.left, out _rayHit, Mathf.Infinity))
                {
                    if (_rayHit.distance <= GetComponent<Collider>().bounds.extents.x + 0.1f)
                    {
                        return true;
                    }
                    else
                    {
                        Debug.DrawRay(transform.position, Vector3.left * _rayHit.distance, Color.blue);
                        return false;
                    }
                }
                else
                {
                    return false;
                }

            }
            else
            {
                if (Physics.Raycast(transform.position, Vector3.right, out _rayHit, Mathf.Infinity))                
                {
                    if (_rayHit.distance <= GetComponent<Collider>().bounds.extents.x + 0.1f)
                    {
                        return true;
                    }
                    else
                    {
                        Debug.DrawRay(transform.position, Vector3.right * _rayHit.distance, Color.blue);
                        return false;
                    }
                }
                else
                {
                    return false;
                }
            } 
        }

        public void TurnAround()
        {
            if(Facing == FacingDirection.Left)
            {
                Facing = FacingDirection.Right;
            }
            else
            {
                Facing = FacingDirection.Left;
            }
        }  
        
        public void MoveTowardsPlayer()
        {
            if (Weapon.Range < Vector3.Distance(transform.position, PlayerTransform.position))
            {
                Debug.Log("Weapon outside range");
                //Player is left of enemy
                if (PlayerTransform.position.x < transform.position.x)
                {
                    Move(-MoveSpeed, 0);
                }
                else
                {
                    Move(MoveSpeed, 0);
                }
            }
        }

        protected void LostPlayer()
        {
            Debug.Log("lost Player");
            StartPatrolling();
        }

        protected void PlayerFound()
        {
            Debug.Log("found Player");

            CurrentEnemyBehaviour = EnemyBehaviour.Chasing;
        }

        protected void Waiting()
        {
            if (FinishedWaiting())
            {
                TurnAround();
                StartPatrolling();

            }
        }

        protected void StartPatrolling()
        {
            Debug.Log("Start Patrolling");
            CurrentEnemyBehaviour = EnemyBehaviour.Patrolling;
        }

        protected void Patrolling()
        {
            if (!EnemyObstructed())
            {
                if (Facing == FacingDirection.Left)
                {
                    Move(-MoveSpeed, 0);
                }
                else
                {
                    Move(MoveSpeed, 0);
                }
            }
            else
            {
                StopPatrolling();
            }
        }

        protected void StopPatrolling()
        {
            Debug.Log("Stop Patrolling");
            CurrentEnemyBehaviour = EnemyBehaviour.Idle;
            endWaitTime = Time.unscaledTime + TimeToWait;
        }

        protected bool FinishedWaiting()
        {
            return Time.unscaledTime > endWaitTime;
        }
    }
}
