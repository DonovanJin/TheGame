using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Jincom.Level
{
    public class LevelSpawner : MonoBehaviour
    {
        [SerializeField]
        private LevelManager _prefab;

        [SerializeField]
        private LevelManager _levelManager;
    }
}