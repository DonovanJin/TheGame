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

        private RaycastHit RayHit;

        public void Update()
        {
            AgentUpdate();            
        }

        public override void AgentUpdate()
        {
            //MoveAgent(Mathf.PingPong(Time.unscaledTime, 4f) - 2f);
            DistanceToPlayer();
            LineOfSight();
            FacingRightDirection();
            DetectPlayer();
            AttackPlayer();
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
                if (CanShoot)
                {
                    CanShoot = false;
                    Debug.Log("Shooting at player.");
                    StartCoroutine(ResetCanShoot());
                }
            }
        }

        IEnumerator ResetCanShoot()
        {
            yield return new WaitForSeconds(0.25f);
            CanShoot = true;
        }
    }
}
