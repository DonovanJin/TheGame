﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Jincom.PickUps;
using Jincom.Agent;

namespace Jincom.Level
{
    public class LevelManager : MonoBehaviour
    {
        [SerializeField]
        private PlayerManager _playerManager;

        [SerializeField]
        private EnemyManager _enemyManager;

        [SerializeField]
        private PickUpManager _pickUpManager;

        public void Init(Player playerData)
        {
            _playerManager.Init(playerData);
            _enemyManager.Init(_playerManager.CurrentPlayerAgent);
            _pickUpManager.Init(_playerManager.CurrentPlayerAgent);
        }
    }
}