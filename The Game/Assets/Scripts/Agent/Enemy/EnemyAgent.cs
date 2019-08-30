using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Jincom.Core;

namespace Jincom.Agent
{
    abstract public class EnemyAgent : AgentBase
    {
        private Enemy _enemyData;
        public Transform PlayerTransform;
        public float SpotDistance = 10f;
        public bool SpottedPlayer;
        private RaycastHit _rayHit;

        private float _horizontalDifference;

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
            Suspicious,
            Attacking,
            Chasing,
            Performing
        };

        public EnemyBehaviour CurrentEnemyBehaviour;

        //  =   =   =   =   =   =   =   =   =   =   =   =

        public override void AgentUpdate()
        {
            SpotThePlayer();
            EnemyShoot();
        }

        //  =   =   =   =   =   =   =   =   =   =   =   =

        public virtual void SpotThePlayer()
        {
            //Performing means it's probably a cutscene of some sort
            if(CurrentEnemyBehaviour != EnemyBehaviour.Performing)
            {
                if (!PlayerTransform)
                {
                    PlayerTransform = GameObject.FindGameObjectWithTag("Player").gameObject.GetComponent<Transform>();
                }

                else
                {
                    _horizontalDifference = transform.position.x - PlayerTransform.position.x;

                    if (Physics.Raycast(transform.position, PlayerTransform.position - transform.position, out _rayHit))
                    {
                        if (_rayHit.transform == PlayerTransform)
                        {
                            if (Vector3.Distance(transform.position, PlayerTransform.position) < SpotDistance)
                            {
                                //SpottedPlayer = true;
                                if ((_horizontalDifference > 0) && (Facing == FacingDirection.Left))
                                {
                                    SpottedPlayer = true;
                                }
                                else if ((_horizontalDifference < 0) && (Facing == FacingDirection.Right))
                                {
                                    SpottedPlayer = true;
                                }
                                else
                                {
                                    SpottedPlayer = false;
                                }
                            }
                            else
                            {
                                SpottedPlayer = false;
                            }
                        }
                        else
                        {
                            SpottedPlayer = false;
                        }
                    }
                }
            }

            if (SpottedPlayer)
            {
                Debug.DrawLine(transform.position, PlayerTransform.position, Color.red);
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
            Debug.Log("Agent is dead");

            //TEMP
            Destroy(this.gameObject);
        }

        //  =   =   =   =   =   =   =   =   =   =   =   =

        private void EnemyShoot()
        {
            if (SpottedPlayer)
            {
                //if Enemy hasn't shot within the last x amount of time, allow the enemy to shoot again
                Debug.Log("Bang, bang, bang!");
            }
        }

        public void ReceiveDamage(int Damage)
        {
            _enemyData.CurrentHealth -= Damage;

        }
    }
}
