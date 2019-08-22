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
        private Player _playerData;
        private bool _doubleJumped;  
        private RaycastHit _rayHit;
        private float _timeFalling;
        private bool _hitFloor;
        private float _oldVertPos = 0f;
        private float _newVertPos = 0f;

        [Range(-1f, 1f)]
        private float _momentum;

        //TESTING
        public int CurrentHealth, MaxHealth, CurrentArmour, MaxArmour /*,CurrentAmmo*/;
        //public WeaponData CurrentWeapon;

        //Facing direction is used to inform graphics and animation
        public enum FacingDirection
        {
            Left,
            Right
        };
        public FacingDirection Facing;        
        
        public string StateOfThisAgent;

        public Transform ShootAtTarget;

#if TESTING
        public float PlayerJumpHeight;
#endif
        //  =   =   =   =   =   =   =   =   =   =   =   =

        public Player PlayerData
        {
            get
            {
                return _playerData;
            }
        } 

        //  =   =   =   =   =   =   =   =   =   =   =   =

        private void Start()
        {
            InitiatePlayerProperties();
        }

        private void InitiatePlayerProperties()
        {
            RB = this.GetComponent<Rigidbody>();

            if (RB != null)
            {
                RB.constraints = RigidbodyConstraints.FreezeRotation | RigidbodyConstraints.FreezePositionZ;
            }

            _oldVertPos = transform.position.y;
            _newVertPos = transform.position.y;
        }

        public override void AgentUpdate()
        {
#if TESTING
            UpdateJumpHeight();
#endif            
            GetPlayerInfo();

            ResetDoubleJump();
            PlayerInput(); 
            
            AnimationState();
            Fall();

            WhatStateIsAgentIn();
        }

        //  =   =   =   =   =   =   =   =   =   =   =   =

        private void GetPlayerInfo()
        {
            CurrentHealth = _playerData.CurrentHealth;
            MaxHealth = _playerData.MaxHealth;
            CurrentArmour = _playerData.CurrentArmour;
            MaxArmour = _playerData.MaxArmour;
            //CurrentWeapon = _playerData.CurrentWeapon;
            //CurrentAmmo = _playerData.Ammo[_playerData.CurrentWeapon];
        }

        private void PlayerInput()
        {
            if (_playerData._currentStateOfAgent != Player.AgentState.Dead)
            {
                if (Input.anyKey)
                {
                    if (Input.GetAxis("Horizontal") != 0)
                    {
                        FacingLeftOrRight();

                        if (CanGoForward())
                        {
                            CalculateMomentum();
                            PlayerMovesForward();
                        }
                    }

                    if (Input.GetButtonDown("Jump"))
                    {
                        PlayerJumps();
                    }

                    if (Input.GetButton("Fire1"))
                    {
                        PlayerShoot();
                    }
                }
            }
        }

        //  =   =   =   =   =   =   =   =   =   =   =   =

        private void FacingLeftOrRight()
        {
            //Facing direction
            if (Input.GetAxis("Horizontal") < 0f)
            {
                Facing = FacingDirection.Left;
            }
            else if (Input.GetAxis("Horizontal") > 0f)
            {
                Facing = FacingDirection.Right;
            }
        }

        //  =   =   =   =   =   =   =   =   =   =   =   =

        //Measure distance ahead of the player and ask if there is any obstructions
        private bool CanGoForward()
        {
            bool _answer;

            if (Facing == FacingDirection.Left)
            {
                if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.left), out _rayHit, 999f))
                {
                    Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.left) * _rayHit.distance, Color.green);
                }
                else
                {
                    Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.left) * 999f, Color.red);
                }
            }
            else
            {
                if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.right), out _rayHit, 999f))
                {
                    Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.right) * _rayHit.distance, Color.green);
                }
                else
                {
                    Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.right) * 999f, Color.red);
                }
            }

            if (_rayHit.distance < 999f)
            {
                if (_rayHit.distance > GetComponent<Collider>().bounds.extents.x)
                {
                    _answer = true;
                }
                else
                {
                    _answer = false;
                }
            }
            else
            {
                Debug.LogError("There seems to be a gap in the level");
                _answer = true;
            }

            return _answer;
        }

        //  =   =   =   =   =   =   =   =   =   =   =   =

        //As soon as 'player' turns from one direction to another, she looses accumilated momentum
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

        private void PlayerMovesForward()
        {
            Move((Input.GetAxis("Horizontal")) * ((Mathf.Abs(_momentum)) + 1), 0f);
        }

        //  =   =   =   =   =   =   =   =   =   =   =   =

        private void ResetDoubleJump()
        {
            //On the ground, resets double jump
            if (Physics.Raycast(transform.position, -Vector3.up, GetComponent<Collider>().bounds.extents.y + 0.1f))
            {
                _doubleJumped = false;
            }
        }

        private void PlayerJumps()
        {
            //On ground, normal jump
            if (Physics.Raycast(transform.position, -Vector3.up, GetComponent<Collider>().bounds.extents.y + 0.1f))
            {
                Jump((_playerData.JumpHeight + (150f * (Mathf.Abs(_momentum)))));
            }
            //In air
            else
            {
                if (RB != null)
                {
                    //Double jump
                    if (RB.velocity.y > 0f)
                    {
                        if (!_doubleJumped)
                        {
                            RB.velocity = new Vector3(RB.velocity.x, 0f, RB.velocity.z);
                            Jump(_playerData.JumpHeight * 0.75f);
                            _doubleJumped = true;
                        }
                    }
                }
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

        private bool CanPlayerShoot(WeaponData weaponData)
        {
            if (_playerData.CurrentGunHasAmmo())
            {
                //total time > (time stamp when gun was last shot + time between each shot)
                if (Time.unscaledTime > (_timeLastShot + weaponData.WaitTimeBetweenShots()))
                {
                    _timeLastShot = Time.unscaledTime;
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                Debug.Log("Out of ammunition.");
                return false;
            }
        }

        //  =   =   =   =   =   =   =   =   =   =   =   =

        private float _timeLastShot = 0;

        //  =   =   =   =   =   =   =   =   =   =   =   =

        private void PlayerShoot()
        {
            if (CanPlayerShoot(_playerData.CurrentWeapon))
            {
                _playerData.FireCurrentWeapon();

                AgentShoot();
            }

            //Testing
            Debug.DrawLine(transform.position, ShootAtTarget.transform.position, Color.red);
        }

        //  =   =   =   =   =   =   =   =   =   =   =   =

        public virtual void AnimationState()
        {
            if (_playerData.CurrentHealth > 0f)
            {
                if (Physics.Raycast(transform.position, -Vector3.up, GetComponent<Collider>().bounds.extents.y + 0.1f))
                {
                    //Idle
                    if (Input.GetAxis("Horizontal") == 0)
                    {
                        _playerData._currentStateOfAgent = Player.AgentState.Idle;
                    }
                    //Walking/Runninng
                    else
                    {
                        _playerData._currentStateOfAgent = Player.AgentState.Walk;
                    }
                }

                else
                {
                    if (RB != null)
                    {
                        //Falling
                        if (RB.velocity.y < 0f)
                        {
                            _playerData._currentStateOfAgent = Player.AgentState.Fall;
                        }
                        //Jumping
                        else if (RB.velocity.y > 0f)
                        {
                            _playerData._currentStateOfAgent = Player.AgentState.Jump;
                        }
                    }
                }
            }
            else
            {
                _playerData._currentStateOfAgent = Player.AgentState.Dead;
            }
        }

        //  =   =   =   =   =   =   =   =   =   =   =   =

#if TESTING
        //Temp developer function
        public void UpdateJumpHeight()
        {
            _playerData.JumpHeight = PlayerJumpHeight;
        }
#endif

        public virtual void Fall()
        {
            _newVertPos = transform.position.y;

            //Player is NOT on the ground
            if (!Physics.Raycast(transform.position, -Vector3.up, GetComponent<Collider>().bounds.extents.y + 0.1f))
            {
                //Player is descending
                if (_oldVertPos > _newVertPos)
                {
                    _timeFalling += Time.deltaTime;
                    if (_hitFloor)
                    {
                        _hitFloor = false;
                    }
                }
            }
            //Player is on the ground
            else
            {
                //Player is NOT descending
                if (!_hitFloor)                
                {
                    if ((_timeFalling > 1f) && (_timeFalling < 3f))
                    {
                        Debug.Log("Fall Damage modifier: " + (_timeFalling - 1f));
                    }
                    else if (_timeFalling > 3f)
                    {
                        Debug.Log("Player dies from falling too far.");
                    }

                    _hitFloor = true;

                    _timeFalling = 0f;
                }
            }
            _oldVertPos = _newVertPos;
        }

        private void WhatStateIsAgentIn()
        {
            StateOfThisAgent = _playerData._currentStateOfAgent.ToString();
        }
    }
}
