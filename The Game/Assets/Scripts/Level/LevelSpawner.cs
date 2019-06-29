using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Jincom.UI;
using Jincom.Agent;
using System;

namespace Jincom.Level
{
    [System.Serializable]
    public struct Level
    {
        public string LevelName;
        public LevelManager Prefab;
    }

    public class LevelSpawner : MonoBehaviour
    {
        public Level[] GameLevels;

        public LevelManager CurrentLevel;

        internal void SpawnLevel(int levelIndex, Player playerData)
        {
            throw new NotImplementedException();
        }
    }
}