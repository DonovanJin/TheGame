using System.Collections;
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
    }
}