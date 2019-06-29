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

        internal void Init(Player playerData)
        {
            _playerData = playerData;
            //Get checkpoint 1
            //instantiate player prefab there
            CurrentPlayerAgent.Init(_playerData);
        }

        internal void UpdateManager()
        {
            
        }
    }
}