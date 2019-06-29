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
        }

        private void PlayerMovement()
        {
            //Movement
            if (Input.GetAxis("Horizontal") != 0)
            {
                Move(Input.GetAxis("Horizontal"));
            }
        }

        internal void Init(Player playerData)
        {
            _playerData = playerData;
        }

        private void Shoot()
        {

        }

        public void Climb()
        {
            Debug.Log("Agent Climbs");
        }

    }
}
