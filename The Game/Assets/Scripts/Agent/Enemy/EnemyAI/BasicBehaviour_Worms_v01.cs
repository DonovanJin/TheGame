#define TESTING
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Jincom.Agent
{   
    public class BasicBehaviour_Worms_v01 : EnemyAgent
    {
        public float OldTimeStamp, NewTimeStamp, CurrentTimeStamp;
        public bool Stopped = false;
        public float TimeToWait;

        //Placeholder, this script will get updated by the enemy manager

        private void Start()
        {
            CurrentEnemyBehaviour = EnemyBehaviour.Patrolling;
        }

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
                CurrentEnemyBehaviour = EnemyBehaviour.Chasing;
            }
            else
            {
                CurrentEnemyBehaviour = EnemyBehaviour.Patrolling;
            }

            switch (CurrentEnemyBehaviour)
            {
                case EnemyBehaviour.Patrolling:
                    Patrolling();                    
                    break;
                case EnemyBehaviour.Chasing:
                    MoveTowardsPlayer();
                    break;
                default:
                    break;
            }            
        }

        private void Patrolling()
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
                //TurnAround();
                StopEnemy();
            }
        }

        private void StopEnemy()
        {
            if (!Stopped)
            {
                Stopped = true;
                OldTimeStamp = Time.unscaledTime;
                NewTimeStamp = Time.unscaledTime + TimeToWait;
            }
            else
            {
                //CurrentTimeStamp = Time.unscaledTime;
                if (Time.unscaledTime < NewTimeStamp)
                {
                    CurrentTimeStamp = Time.unscaledTime;
                }
                else
                {
                    TurnAround();
                    Stopped = false;
                }
            }
        }
    }
}
