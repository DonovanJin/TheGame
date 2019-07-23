using System;
using System.Collections;
using System.Collections.Generic;
using Jincom.PickUps;
using UnityEngine;

namespace Jincom.Agent
{
    public class PlayerAgent : AgentBase
    {
        private Player _playerData;
        private bool _doubleJumped;

        public Player PlayerData
        {
            get
            {
                return _playerData;
            }
        }

        public void Update()
        {
            AgentUpdate();
        }

        public override void AgentUpdate()
        {
            PlayerMovement();
            PlayerShoot();
            AnimationState();
        }

        private void PlayerMovement()
        {

            if (Input.GetAxis("Horizontal") != 0)
            {
                Move(Input.GetAxis("Horizontal"));
            }

            if (IsGrounded)
            {
                _doubleJumped = false;
            }

            if (Input.GetButtonDown("Jump"))
            {
                if (IsGrounded)
                {
                    Jump(JumpHeight);
                }
                else
                {
                    if(RB.velocity.y > 0f)
                    {
                        if (!_doubleJumped)
                        {
                            RB.velocity = new Vector3(RB.velocity.x, 0f, RB.velocity.z);
                            Jump(JumpHeight * 0.75f);
                            _doubleJumped = true;
                        }
                    }
                }
            }

            //Facing direction
            if (Acceleration < 0f)
            {
                //Debug.Log("Left?");
                facing = FacingDirection.Left;
            }
            else if (Acceleration > 0f)
            {
                //Debug.Log("Right?");
                facing = FacingDirection.Right;
            }
        }

        internal void Init(Player playerData)
        {
            _playerData = playerData;
        }        

        private void Climb()
        {
            Debug.Log("Agent Climbs");
        }

        private void PlayerShoot()
        {
            if (Input.GetButton("Fire1"))
            {
                if (CanShoot)
                {
                    AgentShoot();
                    CanShoot = false;
                    StartCoroutine(ResetCanShoot());
                }
            }
        }

        IEnumerator ResetCanShoot()
        {            
            yield return new WaitForSeconds(TimeBetweenEachShotInSeconds);
            CanShoot = true;
        }
    }
}
