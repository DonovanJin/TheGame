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
        [Range(-1f, 1f)]
        private float _momentum;

        //  =   =   =   =   =   =   =   =   =   =   =   =

        public Player PlayerData
        {
            get
            {
                return _playerData;
            }
        }

        //  =   =   =   =   =   =   =   =   =   =   =   =

        public void Update()
        {
            AgentUpdate();
        }

        //  =   =   =   =   =   =   =   =   =   =   =   =

        public override void AgentUpdate()
        {
            CalculateMomentum();
            PlayerMovement();
            PlayerShoot();
            AnimationState();
        }

        //  =   =   =   =   =   =   =   =   =   =   =   =

        private void CalculateMomentum()
        {
            if (Input.GetAxis("Horizontal") < 0)
            {
                if ((_momentum > -1f) && (_momentum <= 0f))
                {
                    _momentum -= 0.01f;
                }
                else if (_momentum > 0f)
                {
                    _momentum = -0.01f;
                }
            }
            else if (Input.GetAxis("Horizontal") > 0)
            {
                if ((_momentum < 1f) && (_momentum >= 0f))
                {
                    _momentum += 0.01f;
                }
                else if (_momentum < 0f)
                {
                    _momentum = 0.01f;
                }
            }
            else
            {
                _momentum = 0f;
            }
        }

        //  =   =   =   =   =   =   =   =   =   =   =   =

        private void PlayerMovement()
        {      
            if (Input.GetAxis("Horizontal") != 0)
            {
                Move((Input.GetAxis("Horizontal")) * ((Mathf.Abs(_momentum)) +1) );               
            }

            if (IsGrounded)
            {
                _doubleJumped = false;
            }

            if (Input.GetButtonDown("Jump"))
            {
                if (IsGrounded)
                {
                    Jump((JumpHeight + (150f * (Mathf.Abs(_momentum)))));
                }
                else
                {
                    if (RB != null)
                    {
                        if (RB.velocity.y > 0f)
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

            //Facing direction
            if (Acceleration < 0f)
            {
                //Debug.Log("Left?");
                Facing = FacingDirection.Left;
            }
            else if (Acceleration > 0f)
            {
                //Debug.Log("Right?");
                Facing = FacingDirection.Right;
            }
        }

        //  =   =   =   =   =   =   =   =   =   =   =   =

        internal void Init(Player playerData)
        {
            _playerData = playerData;
        }

        //  =   =   =   =   =   =   =   =   =   =   =   =

        private void Climb()
        {
            Debug.Log("Agent Climbs");
        }

        //  =   =   =   =   =   =   =   =   =   =   =   =

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

        //  =   =   =   =   =   =   =   =   =   =   =   =

        IEnumerator ResetCanShoot()
        {            
            yield return new WaitForSeconds(TimeBetweenEachShotInSeconds);
            CanShoot = true;
        }
    }
}
