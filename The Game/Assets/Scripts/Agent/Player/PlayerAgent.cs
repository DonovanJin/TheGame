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
        private RaycastHit _rayCastHit;
        private float _initialHeight;
        private Rigidbody _rb;
        private float _distanceFromGround;
        private bool _isGrounded;
        private bool _doubleJumped;

        public Player PlayerData
        {
            get
            {
                return _playerData;
            }
        }

        private void Start()
        {
            _initialHeight = GetComponent<Collider>().bounds.extents.y;
            _rb = GetComponent<Rigidbody>();
        }

        public void Update()
        {
            AgentUpdate();
        }

        public override void AgentUpdate()
        {
            PlayerMovement();
            AnimationState(_isGrounded, _rb);
        }

        private void PlayerMovement()
        {

            if (Input.GetAxis("Horizontal") != 0)
            {
                Move(Input.GetAxis("Horizontal"));
            }

            // Hermann
            if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.down), out _rayCastHit, Mathf.Infinity))
            {
                Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.down) * _rayCastHit.distance, Color.green);
                _distanceFromGround = (_rayCastHit.distance - _initialHeight);
            }

            if (_isGrounded)
            {
                _doubleJumped = false;
            }            

            _isGrounded = Physics.Raycast(transform.position, -Vector3.up, _initialHeight + 0.1f);

            if (Input.GetButtonDown("Jump"))
            {
                if (_isGrounded)
                {
                    Jump(JumpHeight);
                }
                else
                {
                    if(_rb.velocity.y > 0f)
                    {
                        if (!_doubleJumped)
                        {
                            _rb.velocity = new Vector3(_rb.velocity.x, 0f, _rb.velocity.z);
                            Jump(JumpHeight * 0.75f);
                            _doubleJumped = true;
                        }
                    }
                }
            }
            //
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
