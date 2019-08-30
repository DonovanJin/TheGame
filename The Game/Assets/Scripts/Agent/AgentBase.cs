﻿using Jincom.Core;
using UnityEngine;
using System.Collections.Generic;

namespace Jincom.Agent
{
    public abstract class AgentBase : MonoBehaviour
    {
        [Header("Base Agent Properties")]
        public GameConstants.Elements CurrentElement;     
        public float MoveSpeed;
        public bool CanShoot = true;
                
        protected Rigidbody RB;                

        //  =   =   =   =   =   =   =   =   =   =   =   =

        public abstract void AgentUpdate();

        //  =   =   =   =   =   =   =   =   =   =   =   =

        private void Start()
        {
            
        }

        //  =   =   =   =   =   =   =   =   =   =   =   =

        public virtual void Move(float direction, float direction2)
        {
            transform.Translate((Vector3.right * direction) * MoveSpeed);
            transform.Translate((Vector3.up * direction2) * MoveSpeed);
        }

        //  =   =   =   =   =   =   =   =   =   =   =   =
                
        public virtual void Jump(float NewJumpHeight)
        {
            if (RB != null)
            {
                RB.AddForce(new Vector3(0, NewJumpHeight, 0), ForceMode.Impulse);
            }
            else
            {
                Debug.Log("Agent requires a Rigidbody to jump");
            }
        }

        //  =   =   =   =   =   =   =   =   =   =   =   =

        public virtual void Spawn()
        {
            Debug.Log("Agent Spawns");
        }

        //  =   =   =   =   =   =   =   =   =   =   =   =

        public virtual void Pause()
        {
            Debug.Log("Agent Pauses Game");
        }

        //  =   =   =   =   =   =   =   =   =   =   =   =

        public virtual void Resume()
        {
            Debug.Log("Agent Resumes Game");
        }

        //  =   =   =   =   =   =   =   =   =   =   =   =
        
        public virtual void AgentShoot(RaycastHit Target, int DamageFromWeapon)
        {
            //Debug.Log(name + " is shooting at " + Target.collider.name);            

            //Destructable Environment
            if (Target.collider.GetComponent<BreakableTerrain>())
            {
                print("Shooting at breakable terrain");
                Target.collider.GetComponent<BreakableTerrain>().SustainDamage(DamageFromWeapon);
            }
            else if (Target.collider.GetComponent<EnemyAgent>())
            {
                print("Shooting at an enemy agent");
                //Target.collider.GetComponent<EnemyAgent>().
            }
            else
            {
                print("Shooting at the environment.");
            }
        }
    }
}