using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Jincom.Core;

namespace Jincom.Agent
{
    public class EnemyAgent : AgentBase
    {     
        public Transform PlayerTransform;
        public float SpotDistance;
        public bool SpottedPlayer;
        private RaycastHit RayHit;

        private float _horizontalDifference;

        public enum FacingDirection
        {
            Left,
            Right
        };
        public FacingDirection Facing;

        //  =   =   =   =   =   =   =   =   =   =   =   =

        public void Update()
        {
            AgentUpdate();
            SpotThePlayer();
            EnemyShoot();
        }

        //  =   =   =   =   =   =   =   =   =   =   =   =

        public override void AgentUpdate()
        {
            //Move(Mathf.PingPong(Time.unscaledTime, 4f) - 2f);  
            //AnimationState();
        }

        //  =   =   =   =   =   =   =   =   =   =   =   =

        public virtual void SpotThePlayer()
        {
            if (PlayerTransform == null)
            {
                PlayerTransform = GameObject.FindGameObjectWithTag("Player").gameObject.GetComponent<Transform>();
            }

            if (PlayerTransform)
            { 
            _horizontalDifference = transform.position.x - PlayerTransform.position.x;

                if (Physics.Raycast(transform.position, PlayerTransform.position - transform.position, out RayHit))
                {
                    if (RayHit.transform == PlayerTransform)
                    {
                        Debug.DrawLine(transform.position, PlayerTransform.position, Color.red);
                        if (Vector3.Distance(transform.position, PlayerTransform.position) < SpotDistance)
                        {
                            //SpottedPlayer = true;
                            if ((_horizontalDifference > 0) && (Facing == FacingDirection.Left))
                            {
                                SpottedPlayer = true;
                            }
                            else if ((_horizontalDifference < 0) && (Facing == FacingDirection.Right))
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
        }

        //  =   =   =   =   =   =   =   =   =   =   =   =

        public virtual void Throw()
        {
            Debug.Log("Agent Throws");
        }

        //  =   =   =   =   =   =   =   =   =   =   =   =

        public virtual void Melee()
        {
            Debug.Log("Agent Uses Melee");
        }

        //  =   =   =   =   =   =   =   =   =   =   =   =

        public virtual void Die()
        {
            Debug.Log("Agent Dies");
        }

        //  =   =   =   =   =   =   =   =   =   =   =   =

        private void EnemyShoot()
        {
            if (SpottedPlayer)
            {
                if (CanShoot)
                {
                    AgentShoot();
                    CanShoot = false;
                    //StartCoroutine(ResetCanShoot());
                }
            }
        }

        //  =   =   =   =   =   =   =   =   =   =   =   =

        //replace with better reset
        //IEnumerator ResetCanShoot()
        //{
        //    yield return new WaitForSeconds(TimeBetweenEachShotInSeconds);
        //    CanShoot = true;
        //}
    }
}
