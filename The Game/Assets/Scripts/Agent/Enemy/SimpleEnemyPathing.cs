using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Jincom.Agent
{
    public class SimpleEnemyPathing : MonoBehaviour
    {
        public EnemyAgent enemyAgent;
        public RaycastHit raycastHit;

        public bool StartedWaiting = false;
        public float WaitTime = 1f;
        public float TimeStampEndWait;

        void Start()
        {

        }

        void Update()
        {
            PathLogic();
        }

        private void PathLogic()
        {
            if (enemyAgent.enemyBehaviour == EnemyAgent.EnemyBehaviour.Patroling)
            {
                if (!IsEnemyObstructed())
                {
                    MovingLeftOrRight();
                }
                else
                {
                    //TurnAround();
                    WaitForAMoment();
                }
            }
            else
            {
                print("Enemy not patrolling");
            }
        }

        private bool IsEnemyObstructed()
        {
            bool _answer;

            //GetComponent<Collider>().bounds.extents.x
            if (enemyAgent.Facing == EnemyAgent.FacingDirection.Left)
            {
                if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.left), out raycastHit, 999f))
                {
                    Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.left) * raycastHit.distance, Color.green);
                }
                else
                {
                    Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.left) * 999f, Color.red);
                }
            }
            else
            {
                if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.right), out raycastHit, 999f))
                {
                    Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.right) * raycastHit.distance, Color.green);
                }
                else
                {
                    Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.right) * 999f, Color.red);
                }
            }

            if (raycastHit.distance > GetComponent<Collider>().bounds.extents.x)
            {
                _answer = false;
            }
            else
            {
                _answer = true;
            }

            return _answer;
        }

        private void MovingLeftOrRight()
        {
            if (enemyAgent.Facing == EnemyAgent.FacingDirection.Left)
            {
                enemyAgent.Move(-1, 0);
            }
            else
            {
                enemyAgent.Move(1, 0);
            }
        }

        private void WaitForAMoment()
        {
            if (!StartedWaiting)
            {
                TimeStampEndWait = Time.unscaledTime + WaitTime;
                StartedWaiting = true;
            }

            else
            {
                if (TimeStampEndWait <= Time.unscaledTime)
                {
                    TurnAround();
                    StartedWaiting = false;
                }
            }
        }

        private void TurnAround()
        {
            if (enemyAgent.Facing == EnemyAgent.FacingDirection.Left)
            {
                enemyAgent.Facing = EnemyAgent.FacingDirection.Right;
            }
            else
            {
                enemyAgent.Facing = EnemyAgent.FacingDirection.Left;
            }
        }
    }
}
