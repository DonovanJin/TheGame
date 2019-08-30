#define TESTING
using System;
using System.Collections;
using System.Collections.Generic;
using Jincom.PickUps;
using UnityEngine;
using Jincom.CameraLogic;
using System.Linq;

namespace Jincom.Agent
{
    public class PlayerAgent : AgentBase
    {
        //public string TrevelingLeftOrRight = "Stopped";

        public float DistanceToCursorTarget;
        public GameObject VisibleMesh;

        //private float _lastHorizontalPosition;
        private Player _playerData;
        private bool _doubleJumped;  
        private RaycastHit _rayHit;
        private float _timeFalling;
        private bool _hitFloor;
        private float _oldVertPos = 0f;
        private float _newVertPos = 0f;

        [Range(-1f, 1f)]
        public float _momentum;
                
        public int CurrentHealth, MaxHealth, CurrentArmour, MaxArmour, CurrentAmmo;
        public float JumpHeight;
        public WeaponData CurrentWeapon;

        //Facing direction is used to inform graphics and animation
        public enum FacingDirection
        {
            Left,
            Right
        };
        public FacingDirection Facing;

        public enum AgentState
        {
            Walk,
            Idle,
            Dead,
            Fall,
            Jump
        };
        public AgentState _currentStateOfAgent;

        public string StateOfThisAgent;

        public Transform CursorTarget;

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

            //_lastHorizontalPosition = transform.position.x;
        }

        public override void AgentUpdate()
        {
            //DisplayPlayerInfo is used in development time as substitute for a UI
            //MeshDirection is a temporary solution for turning the player around
#if TESTING
            DisplayPlayerInfo();
            MeshDirection();
#endif
            FacingLeftOrRight();
            ResetDoubleJump();

            CalculateMomentum();
            PlayerInput();             
            AnimationState();
            Fall();
            WhatStateIsAgentIn();
            CalculateDistanceToCursorTarget();
        }

        //  =   =   =   =   =   =   =   =   =   =   =   =

#if TESTING
        private void DisplayPlayerInfo()
        {
            CurrentHealth = _playerData.CurrentHealth;
            MaxHealth = _playerData.MaxHealth;
            CurrentArmour = _playerData.CurrentArmour;
            MaxArmour = _playerData.MaxArmour;
            JumpHeight = _playerData.JumpHeight;
            if (PlayerData.CurrentWeapon != null)
            {
                CurrentWeapon = PlayerData.CurrentWeapon;
                CurrentAmmo = PlayerData.Ammo[PlayerData.CurrentWeapon];
            }
        }
#endif

        private void PlayerInput()
        {
            if (_currentStateOfAgent != AgentState.Dead)
            {
                if (Input.anyKey)
                {
                    if (Input.GetAxis("Horizontal") != 0)
                    {                        
                        PlayerMovesForward();
                    }

                    if (Input.GetButtonDown("Jump"))
                    {
                        PlayerJumps();
                    }

                    if (Input.GetButton("Fire1"))
                    {
                        PlayerShoot();
                    }
#if TESTING
                    if (Input.GetKeyDown(KeyCode.Alpha1))
                    {                      
                        PlayerData.SwitchToWeapon(GetComponentInChildren<ListOfWeapons>().WeaponList[0]);                        
                    }
#endif
                }
            }
        }

        //  =   =   =   =   =   =   =   =   =   =   =   =

        private void FacingLeftOrRight()
        {
            //if (GetComponentInChildren<Cursor_UI_3DSpace>().CanvasCursorPosition.x < (Screen.width * 0.5f))

            //If target that player shoots at is left or right of them
            if (CursorTarget.transform.position.x < this.transform.position.x)
            {
                if (Facing != FacingDirection.Left)
                {
                    Facing = FacingDirection.Left;
                }
            }
            else if (CursorTarget.transform.position.x > this.transform.position.x)
            {
                if (Facing != FacingDirection.Right)
                {
                    Facing = FacingDirection.Right;
                }
            }
        }

        //  =   =   =   =   =   =   =   =   =   =   =   =

        //As soon as 'player' turns from one direction to another, she looses accumilated momentum
        private void CalculateMomentum()
        {
            //if player is on the ground
            if (Physics.Raycast(transform.position, -Vector3.up, GetComponent<Collider>().bounds.extents.y + 0.1f))
            {
                if (IsPlayerRunning())
                {
                    if (Input.GetAxis("Horizontal") != 0)
                    {
                        if (!IsPlayerBackpedalling())
                        {
                            if (Input.GetAxis("Horizontal") < 0)
                            {
                                if ((_momentum > -1f) && (_momentum <= 0f))
                                {
                                    _momentum -= PlayerData.RunAcceleration;
                                }
                                else if (_momentum > 0f)
                                {
                                    _momentum = -PlayerData.RunAcceleration;
                                }
                            }
                            else
                            {
                                if ((_momentum < 1f) && (_momentum >= 0f))
                                {
                                    _momentum += PlayerData.RunAcceleration;
                                }
                                else if (_momentum < 0f)
                                {
                                    _momentum = PlayerData.RunAcceleration;
                                }
                            }
                        }
                        else
                        {
                            _momentum = 0f;
                        }
                    }
                }
                else
                {
                    _momentum = 0f;
                }
            }
        }

        public bool IsPlayerRunning()
        {
            if (Input.GetKey(KeyCode.LeftShift))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        //  =   =   =   =   =   =   =   =   =   =   =   =

        private void PlayerMovesForward()
        {
            float _input;

            if (IsPlayerBackpedalling())
            {
                _input = ((Input.GetAxis("Horizontal")) * ((Mathf.Abs(_momentum)) + 1)) * 0.5f;
            }
            else
            {
                _input = (Input.GetAxis("Horizontal")) * ((Mathf.Abs(_momentum)) + 1);
            }

            Move(_input, 0f);
        }

        public bool IsPlayerBackpedalling()
        {
            //Player is one direction, yet moving the oppisite
            if ((Input.GetAxis("Horizontal") < 0f) && (Facing == FacingDirection.Right))
            {
                return true;
            }
            else if ((Input.GetAxis("Horizontal") > 0f) && ((Facing == FacingDirection.Left)))
            {
                return true;
            }
            //Player is moving in same direction as he/she is facing
            else
            {
                return false;
            }
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
                            Jump(_playerData.JumpHeight);
                            _doubleJumped = true;
                        }
                    }
                }
            }
        }     

        //  =   =   =   =   =   =   =   =   =   =   =   =

        internal void Init(Player playerData)
        {
            this._playerData = playerData;
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
            if (_playerData.CurrentWeapon)
            {
                ShootAtTheTarget();
                _playerData.FireCurrentWeapon();
            }            
        }

        private void ShootAtTheTarget()
        {
            Vector3 Origin;

            Origin = new Vector3(transform.position.x, transform.position.y + (GetComponent<CapsuleCollider>().height/2), transform.position.z);

            if (CanPlayerShoot(_playerData.CurrentWeapon))
            {
                //Player shot something (A surface or an agent)
                if (Physics.Raycast(Origin, CursorTarget.transform.position - Origin, out _rayHit, _playerData.CurrentWeapon.Range))
                {
                    Debug.DrawRay(Origin, _rayHit.point - Origin, Color.red);
                    //print(_playerData.CurrentWeapon.ToString() + _playerData.Ammo[_playerData.CurrentWeapon].ToString() + " " + _playerData.CurrentWeapon.Range);
                    AgentShoot(_rayHit, PlayerData.CurrentWeapon.Damage);
                }
                //Player shot into the void.
                else
                {
                    Debug.DrawRay(Origin, CursorTarget.transform.position - Origin, Color.blue);
                }
            }
        }

        //private void WhatDidIHit(RaycastHit _rayHit)
        //{
        //    if (_rayHit.collider.GetComponent<BreakableTerrain>())
        //    {
        //        _rayHit.collider.GetComponent<BreakableTerrain>().SustainDamage(25);
        //    }
        //    else
        //    {
                
        //    }
        //}

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
                        _currentStateOfAgent = AgentState.Idle;
                    }
                    //Walking/Runninng
                    else
                    {
                        _currentStateOfAgent = AgentState.Walk;
                    }
                }

                else
                {
                    if (RB != null)
                    {
                        //Falling
                        if (RB.velocity.y < 0f)
                        {
                            _currentStateOfAgent = AgentState.Fall;
                        }
                        //Jumping
                        else if (RB.velocity.y > 0f)
                        {
                            _currentStateOfAgent = AgentState.Jump;
                        }
                    }
                }
            }
            else
            {
                _currentStateOfAgent = AgentState.Dead;
            }
        }

        //  =   =   =   =   =   =   =   =   =   =   =   =

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
            StateOfThisAgent = _currentStateOfAgent.ToString();
        }
           
#if TESTING
        private void MeshDirection()
        {
            if(Facing == FacingDirection.Left)
            {
                VisibleMesh.transform.eulerAngles = new Vector3(transform.rotation.x, -90, transform.rotation.z);
            }
            else
            {
                VisibleMesh.transform.eulerAngles = new Vector3(transform.rotation.x, 90, transform.rotation.z);
            }
        }
#endif

        //private void IsPlayerTravellingLeftorRight()
        //{
        //    if (_lastHorizontalPosition < transform.position.x)
        //    {
        //        TrevelingLeftOrRight
        //    }
        //}

        private void CalculateDistanceToCursorTarget()
        {
            //DistanceToCursorTarget = Vector3.Distance(transform.position, CursorTarget.position);
            DistanceToCursorTarget = Mathf.Abs(transform.position.x - CursorTarget.position.x);

            //target difference 10
            //cam difference 7            

            if ((DistanceToCursorTarget > 5) && (DistanceToCursorTarget < 13))
            {
                float _oldFollowDistance = Camera.main.GetComponent<CameraManager>().DefaultFollowDistance; ;
                float _newFollowDistance = _oldFollowDistance + (_oldFollowDistance * ((DistanceToCursorTarget - 5) / 10));

                Camera.main.GetComponent<CameraManager>().CurrentFollowDistance = _newFollowDistance;
            }
        }
    }    
}
