//#define TESTING
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Jincom.Agent
{   
    public class Worm : EnemyAgent
    {       

        //Placeholder, this script will get updated by the enemy manager

#if TESTING
        private void Update()
        {
            AgentUpdate();
        }
#endif
        public void DecideWhichbehaviour()
        {

        }
        
        public override void EnemyAI()
        {
            if (PlayerSpotted())
            {
                PlayerFound();
            }

            switch (CurrentEnemyBehaviour)
            {
                case EnemyBehaviour.Patrolling:
                    Patrolling();                    
                    break;
                case EnemyBehaviour.Chasing:
                    if (!PlayerSpotted())
                    {
                        LostPlayer();
                    }
                    else
                    {
                        MoveTowardsPlayer();
                    }
                    break;
                case EnemyBehaviour.Idle:
                    Waiting();
                    break;
                default:
                    break;
            }            
        }        
    }
}
