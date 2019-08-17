//#define TESTING
using System;
using System.Collections;
using System.Collections.Generic;
using Jincom.PickUps;
using UnityEngine;
using Jincom.CameraLogic;

namespace Jincom.Agent
{
    public class PlayerAgent : AgentBase
    {
        public Vector3 MouseCoords;
        public Vector3 MousePos;
        //public float MouseSensitivity = 0.1f;
        public bool MouseVisible = true;
        //public bool RegularJump = false;
        private float HorDistToWallRight = 999f;
        private float HorDistToWallLeft = 999f;
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
#if TESTING

        public void Update()
        {
            AgentUpdate();
        }
#endif


        //  =   =   =   =   =   =   =   =   =   =   =   =

        public override void AgentUpdate()
        {
            CalculateMomentum();
            PlayerMovement();
            PlayerShoot();
            AnimationState();
            Fall();
        }

        //  =   =   =   =   =   =   =   =   =   =   =   =

        private void CalculateMomentum()
        {
            if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.right), out RayHit, Mathf.Infinity))
            {
                Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.right) * RayHit.distance, Color.green);
                HorDistToWallRight = RayHit.distance;
            }
            else
            {
                HorDistToWallRight = 999f;
            }

            if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.left), out RayHit, Mathf.Infinity))
            {
                Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.left) * RayHit.distance, Color.green);
                HorDistToWallLeft = RayHit.distance;
            }
            else
            {
                HorDistToWallLeft = 999f;
            }

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
            if (Input.GetAxis("Horizontal") > 0)
            {
                if (HorDistToWallRight > _initialWidth)
                {
                    Move((Input.GetAxis("Horizontal")) * ((Mathf.Abs(_momentum)) + 1));
                }
            }
            else if (Input.GetAxis("Horizontal") < 0)
            {
                if (HorDistToWallLeft > _initialWidth)
                {
                    Move((Input.GetAxis("Horizontal")) * ((Mathf.Abs(_momentum)) + 1));
                }
            }

            if (IsGrounded)
            {
                _doubleJumped = false;
            }

            //if (!MovingUpOrDown)
            //{
            //    RegularJump = false;
            //}

            if (Input.GetButtonDown("Jump"))
            {

                //if ((IsGrounded) || (!MovingUpOrDown))
                //{
                //    if (!RegularJump)
                //    {                        
                //        Jump((JumpHeight + (150f * (Mathf.Abs(_momentum)))));
                //        RegularJump = true;
                //    }
                //}
                if (IsGrounded)
                {
                    Jump((JumpHeight + (150f * (Mathf.Abs(_momentum)))));
                    //RegularJump = true;
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
            if (HorAcceleration < 0f)
            {
                //Debug.Log("Left?");
                Facing = FacingDirection.Left;
            }
            else if (HorAcceleration > 0f)
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
                if (CanPlayerShoot(_playerData.CurrentWeapon))
                {
                    _playerData.FireCurrentWeapon();
                    //print(CurrentWeapon.CurrentCapacity);

                    AgentShoot();

                    //Spawn bullert
                    Debug.DrawLine(transform.position, Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 5f)), Color.red);

                }
            }
        }

        //  =   =   =   =   =   =   =   =   =   =   =   =
        private float _timeLastShot = 0;

        private bool CanPlayerShoot(WeaponData weaponData)
        {
            if (!_playerData.CurrentGunHasAmmo())
            {
                return false;
            }

            if (Time.unscaledTime > (_timeLastShot + weaponData.WaitTimeBetweenShots()))
            {
                _timeLastShot = Time.unscaledTime;
                return true;
            }

            return false;
        }

        //  =   =   =   =   =   =   =   =   =   =   =   =

 

        //  =   =   =   =   =   =   =   =   =   =   =   =
    }
}
