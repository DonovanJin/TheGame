using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Jincom.Agent
{
    public class PlayerAgent : AgentBase
    {
        [Header("Player Agent Properties")]
        public int JumpCount;
        public int JumpLimit;
        public float OriginalMoveSpeed;
        public float Momentum = 0.5f;
        public float MomentumRate = 0.2f;

        private void Start()
        {
            OriginalMoveSpeed = MoveSpeed;
            Rb = GetComponent<Rigidbody>();
        }

        public void Update()
        {
            AgentUpdate();
        }

        public override void AgentUpdate()
        {
            AgentStayUpright();
            StateOfAgent();
            PlayerAttack();
            PlayerMovement();
            PlayerJump();
        }

        private void PlayerMovement()
        {
            //Calculate momentum
            if (CurrentState == AgentState.Idle)
            {
                Momentum = 0.5f;
            }
            else if (CurrentState == AgentState.Walk)
            {
                if (Momentum < 1)
                {
                    Momentum += MomentumRate * Time.deltaTime;
                }
            }

            //Adjust speed based on momentum (if applicable)
            //if (Physics.Raycast(transform.position, -Vector3.up, GetComponent<Collider>().bounds.extents.y + 0.1f))
            //{
            //    MoveSpeed = OriginalMoveSpeed;
            //}
            //else
            //{
            //    MoveSpeed = OriginalMoveSpeed * Momentum;
            //}

            MoveSpeed = OriginalMoveSpeed * Momentum;

            //Movement
            if (Input.GetAxis("Horizontal") != 0)
            {
                MoveAgent(Input.GetAxis("Horizontal"));
            }
        }

        private void PlayerJump()
        {
            //Grounded
            if (Physics.Raycast(transform.position, -Vector3.up, GetComponent<Collider>().bounds.extents.y + 0.1f))
            {
                JumpCount = 0;
            }

            //Jump and Multi Jump
            if (Input.GetButtonDown("Jump"))
            {
                if (JumpCount == 0)
                {
                    if (Physics.Raycast(transform.position, -Vector3.up, GetComponent<Collider>().bounds.extents.y + 0.1f))
                    {
                        JumpAgent(JumpHeight);
                        JumpCount++;
                    }
                }
                else if (JumpCount < JumpLimit)
                {
                    if (Rb.velocity.y <= 0f)
                    {
                        JumpAgent(JumpHeight);
                        JumpCount++;
                    }
                }
            }
        }

        private void PlayerAttack()
        {
            if (Input.GetButton("Fire1"))
            {
                AgentAttack(true);

                if (CanShoot)
                {
                    CanShoot = false;
                    AgentShoot();
                    StartCoroutine(ResetCanShoot());
                }
            }
            else if (Input.GetButtonUp("Fire1"))
            {
                AgentAttack(false);
            }
        }

        IEnumerator ResetCanShoot()
        {            
            yield return new WaitForSeconds(0.25f);
            CanShoot = true;
        }
    }
}
