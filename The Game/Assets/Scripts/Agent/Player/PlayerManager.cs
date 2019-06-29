using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Jincom.Agent
{
    public class PlayerManager : MonoBehaviour
    {
        private Player _playerData;
        public PlayerAgent PlayerPrefab;
        public PlayerAgent CurrentPlayerAgent;
        public PlayerCheckpoint[] PlayerCheckpoints;
        public PlayerCheckpoint LastCheckpoint; //The last checkpoint the player walked passed. Updates every time a player the walks passed a new one

        /// <summary>
        /// Use this to initialize the Player Manager
        /// </summary>
        /// <param name="playerData"> Pass through player data from Game Manager</param>
        internal void Init(Player playerData)
        {
            _playerData = playerData;
            CurrentPlayerAgent = Instantiate(PlayerPrefab, this.transform);
            LastCheckpoint = PlayerCheckpoints[0];
            CurrentPlayerAgent.transform.position = LastCheckpoint.transform.position;
            CurrentPlayerAgent.Init(_playerData);
        }

        internal void UpdateManager()
        {
            
        }
    }
}