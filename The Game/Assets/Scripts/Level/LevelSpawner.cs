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
        [SerializeField]
        private Level[] _gameLevels;

        public LevelManager CurrentLevel;

        internal void SpawnLevel(int levelIndex, Player playerData)
        {
            CurrentLevel = Instantiate(_gameLevels[levelIndex].Prefab, this.transform);
            CurrentLevel.Init(playerData);
        }

        public void UpdateSpawner()
        {
            if (CurrentLevel != null)
            {
                CurrentLevel.UpdateManager();
            }
        }
    }
}