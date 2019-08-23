using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Jincom.Agent
{
    public class SimpleEnemyPathing : MonoBehaviour
    {
        public EnemyAgent enemyAgent;
        public RaycastHit raycastHit;

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
                MovingLeftOrRight();
            }
            else
            {
                print("Enemy not patrolling");
            }
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
    }
}
