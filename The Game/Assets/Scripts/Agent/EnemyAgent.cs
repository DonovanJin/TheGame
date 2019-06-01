using UnityEngine;
using System.Collections.Generic;
using System.Collections;

namespace Jincom.Agent
{
    public class EnemyAgent : AgentBase
    {
        [Header("Enemy Properties")]
        public float CurrentDistanceToPlayer;
        public float MinDistanceToPlayer = 1f;
        public bool CloseEnough = false;
        public bool SeePlayer = false;
        public bool CorrectDirection = false;
        public bool AwareOfPlayer = false;        
        //public float AttackRange;

        private RaycastHit RayHit;

        //private void Start()
        //{
        //    //if (AttackRange == 0)
        //    //{
        //    //    AttackRange = (MinDistanceToPlayer * 0.5f);
        //    //}
        //}

        public enum TypeOfAttck
        {
            ShootOrMelee,
            GrabPlayerFromDistance,
            GrabPlayerUpClose
        };

        public TypeOfAttck AttackType;

        public enum WeightClass
        {
            VeryEasyMinion,
            EasyMinion,
            MediumMinion,
            HardMinion,
            EasyBoss,
            MediumBoss,
            HardBoss
        };
        public WeightClass EnemyWeightClass;

        public void Update()
        {
            AgentUpdate();            
        }

        public override void AgentUpdate()
        {
            //MoveAgent(Mathf.PingPong(Time.unscaledTime, 4f) - 2f);
            AgentStayUpright();
            DistanceToPlayer();
            LineOfSight();
            FacingRightDirection();
            DetectPlayer();
            AttackPlayer();
            MoveTowardsPlayer();
            AgentDie();
        }

        private void DistanceToPlayer()
        {
            CurrentDistanceToPlayer = Vector3.Distance(transform.position, GameObject.FindGameObjectWithTag("Player").transform.position);

            if(CurrentDistanceToPlayer <= MinDistanceToPlayer)
            {
                CloseEnough = true;
            }
            else
            {
                CloseEnough = false;
            }
        }

        private void LineOfSight()
        {
            if (CloseEnough)
            {
                if (Physics.Raycast(transform.position, (GameObject.FindGameObjectWithTag("Player").transform.position - transform.position), out RayHit, MinDistanceToPlayer))
                {
                    if (RayHit.collider.tag == "Player")
                    {
                        SeePlayer = true;
                    }
                    else
                    {
                        SeePlayer = false;
                    }
                }
            }
            else
            {
                SeePlayer = false;
            }
        }

        private void FacingRightDirection()
        {
            if (GameObject.FindGameObjectWithTag("Player").transform.position.x < transform.position.x)
            {
                if (FacingLeftOrRight == AgentFacingDirection.Left)
                {
                    CorrectDirection = true;
                }
                else
                {
                    CorrectDirection = false;
                }
            }
            else if (GameObject.FindGameObjectWithTag("Player").transform.position.x > transform.position.x)
            {
                if (FacingLeftOrRight == AgentFacingDirection.Right)
                {
                    CorrectDirection = true;
                }
                else
                {
                    CorrectDirection = false;
                }
            }
        }

        private void DetectPlayer()
        {
            if(CloseEnough && SeePlayer && CorrectDirection)
            {
                AwareOfPlayer = true;
            }
            else
            {
                AwareOfPlayer = false;
            }
        }

        private void AttackPlayer()
        {
            if ((AwareOfPlayer))
            {
                if (CurrentDistanceToPlayer <= Weapon.Range)
                {
                    AgentAttack(true);
                    if (CanShoot)
                    {
                        CanShoot = false;
                        //Debug.Log("Shooting at player.");

                        if (AttackType == TypeOfAttck.ShootOrMelee)
                        {
                            AgentShoot();
                        }
                        else if (AttackType == TypeOfAttck.GrabPlayerUpClose)
                        {
                            Debug.Log("Grabbing Player at close range");
                        }
                        else
                        {
                            Debug.Log("Grabbing Player from afar");
                        }
                    }
                }
                else
                {
                    AgentAttack(false);
                }
            }
            else
            {
                AgentAttack(false);
            }
        }

        private void MoveTowardsPlayer()
        {
            if (AwareOfPlayer)
            {
                if (CurrentDistanceToPlayer > Weapon.Range)
                {
                    //Debug.Log("Moving towards Player");
                    if (GameObject.FindGameObjectWithTag("Player").transform.position.x < transform.position.x)
                    {
                        MoveAgent(-1f);
                    }
                    else
                    {
                        MoveAgent(1f);
                    }
                }
            }
        }
    }
}
