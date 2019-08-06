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
        public WeaponData CurrentWeapon;
        public List<WeaponData> UnlockedWeapons;
        public GameObject Cursor;
        public Vector3 MouseCoords;
        public Vector3 MousePos;
        public float MouseSensitivity = 0.1f;

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
            Fall();
            UpdateCrossHairTarget();
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
                    if (CurrentWeapon.CurrentCapacity > 0)
                    {
                        CurrentWeapon.CurrentCapacity--;
                        //print(CurrentWeapon.CurrentCapacity);

                        AgentShoot();
                        CanShoot = false;
                        Debug.DrawLine(GameObject.FindGameObjectWithTag("Player").transform.position, Cursor.transform.position, Color.red);
                        StartCoroutine(ResetCanShoot());
                    }
                }
            }
        }

        //  =   =   =   =   =   =   =   =   =   =   =   =

        IEnumerator ResetCanShoot()
        {            
            yield return new WaitForSeconds(TimeBetweenEachShotInSeconds);
            CanShoot = true;
        }

        //  =   =   =   =   =   =   =   =   =   =   =   =

        private void UpdateCrossHairTarget()
        {
            if (GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CameraManager>().cameraMode == CameraManager.CameraMode.NormalGameplay)
            {
                if (Cursor != null)
                {
                    MouseCoords = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 5f));
                    MousePos = Input.mousePosition;
                    //Cursor.transform.position = Vector2.Lerp(Cursor.transform.position, MouseCoords, MouseSensitivity);
                    Cursor.transform.position = new Vector3(MouseCoords.x, MouseCoords.y, 0f);
                }
                else
                {
                    Cursor = GameObject.Find("Cursor");
                }
            }
        }

        //  =   =   =   =   =   =   =   =   =   =   =   =

        private void AddWeapon(WeaponData NewWeapon)
        {
            if (!UnlockedWeapons.Contains(NewWeapon))
            {
                UnlockedWeapons.Add(NewWeapon);
            }
        }
    }
}
