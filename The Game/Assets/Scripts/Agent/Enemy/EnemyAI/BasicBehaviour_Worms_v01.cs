#define TESTING
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Jincom.Agent
{   
    public class BasicBehaviour_Worms_v01 : EnemyAgent
    {
        //Placeholder, this script will get updated by the enemy manager
#if TESTING
        private void Update()
        {
            AgentUpdate();
        }
#endif

        public override void EnemyAI()
        {
            switch (CurrentEnemyBehaviour)
            {
                case EnemyBehaviour.Patrolling:
                    Patrolling();
                    break;
                default:
                    break;
            }            
        }

        private void Patrolling()
        {            
            //if (EnemyObstructed())
            //{
            //    TurnAround();
            //}

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
                TurnAround();
            }
        }
    }
}
