using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Jincom.Core;

namespace Jincom.Agent
{
    public class EnemyAgent : AgentBase
    {       
        public int CurrentHealth;
        public int MaxHealth;
        public int CurrentArmour;
        public int MaxArmour;
        public GameConstants.Elements CurrentElement;
        public Transform PlayerTransform;
        public float SpotDistance;
        public bool SpottedPlayer;
        public float HorizontalDifference;

        public void Update()
        {
            AgentUpdate();
            SpotThePlayer();
        }

        public override void AgentUpdate()
        {
            //Move(Mathf.PingPong(Time.unscaledTime, 4f) - 2f);            
        }

        public virtual void SpotThePlayer()
        {
            if (PlayerTransform == null)
            {
                PlayerTransform = GameObject.FindGameObjectWithTag("Player").gameObject.GetComponent<Transform>();
            }

            HorizontalDifference = transform.position.x - PlayerTransform.position.x;

            if (Physics.Raycast(transform.position, PlayerTransform.position - transform.position, out RayHit))
            {
                if (RayHit.transform == PlayerTransform)
                {
                    Debug.DrawLine(transform.position, PlayerTransform.position, Color.red);
                    if(Vector3.Distance(transform.position, PlayerTransform.position) < SpotDistance)
                    {
                        //SpottedPlayer = true;
                        if ((HorizontalDifference > 0) && (facing == FacingDirection.Left))
                        {
                            SpottedPlayer = true;
                        }
                        else if ((HorizontalDifference < 0) && (facing == FacingDirection.Right))
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

        public virtual void Shoot()
        {
            Debug.Log("Agent Shoots");
        }

        public virtual void Throw()
        {
            Debug.Log("Agent Throws");
        }

        public virtual void Melee()
        {
            Debug.Log("Agent Uses Melee");
        }

        public virtual void Die()
        {
            Debug.Log("Agent Dies");
        }
    }
}
