using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Jincom.Agent
{
    public class EnemyManager : MonoBehaviour
    {
        public PlayerAgent Player;

        internal void Init(PlayerAgent currentPlayerAgent)
        {
            Player = currentPlayerAgent;
        }

        internal void UpdateManager()
        {
           
        }
    }
}