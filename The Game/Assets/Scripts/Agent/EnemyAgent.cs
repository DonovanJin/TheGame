using UnityEngine;

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
        }

        public void DistanceToPlayer()
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

        public void LineOfSight()
        {
            //if (Physics.Raycast(transform.position, (GameObject.FindGameObjectWithTag("Player").transform.position - transform.position), out hit, maxRange))
            //{
            //    if (hit.transform == player)
            //    {
            //        // In Range and i can see you!
            //    }
            //}
            if (CloseEnough)
            {
                if (Physics.Raycast(transform.position, (GameObject.FindGameObjectWithTag("Player").transform.position - transform.position), out RayHit, MinDistanceToPlayer))
                {
                    if (RayHit.collider.tag == "Player")
                    {
                        Debug.Log("I see you.");
                    }
                }
            }
        }
    }
}
