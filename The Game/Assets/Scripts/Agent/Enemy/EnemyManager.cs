using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Jincom.Agent
{
    public class EnemyManager : MonoBehaviour
    {
        public EnemySpawner[] Spawners;
        public PlayerAgent Player;
        public List<EnemyAgent> Enemies = new List<EnemyAgent>();

        //private 
        public EnemyAgent CurrentEenemy;
        public GameObject[] EnemyPrefabs;

        internal void Init(PlayerAgent currentPlayerAgent)
        {
            Player = currentPlayerAgent;

            for (int i = 0; i < Spawners.Length; i++)
            {
                EnemyAgent enemy = Spawners[i].Init();
                enemy.transform.parent = this.transform;
                enemy.Init();

                if (enemy != null)
                {
                    Enemies.Add(enemy);
                }
            }
        }

        internal void UpdateManager()
        {
            if (Enemies.Count > 0)
            {
                for (int i = 0; i < Enemies.Count; i++)
                {
                    Enemies[i].AgentUpdate();
                }
            }
        }
    }
}