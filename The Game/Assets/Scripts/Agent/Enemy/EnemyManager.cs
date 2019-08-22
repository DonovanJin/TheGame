using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Jincom.Agent
{
    public class EnemyManager : MonoBehaviour
    {
        //HERMANN - Jeff, I am not sure why the Enemy Manager needs a refernce to the player agent script. Is there a reason?
        public PlayerAgent Player;

        //private 
        public EnemyAgent CurrentEenemy;
        public GameObject[] EnemyPrefabs;

        internal void Init(PlayerAgent currentPlayerAgent)
        {
            Player = currentPlayerAgent;
        }

        //internal void Init(EnemyAgent )
        //{
            
        //}

        internal void UpdateManager()
        {
           
        }
    }
}