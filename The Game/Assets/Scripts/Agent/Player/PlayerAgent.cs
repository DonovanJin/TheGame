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
        }

        internal void Init(Player playerData)
        {
            _playerData = playerData;
        }

        private void Shoot()
        {
            Debug.Log("Agent Shoots. Pew pew!");
        }

        private void Climb()
        {
            Debug.Log("Agent Climbs");
        }
    }
}
