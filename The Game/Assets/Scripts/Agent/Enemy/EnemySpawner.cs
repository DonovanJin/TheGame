using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Jincom.Agent
{
    public class EnemySpawner : MonoBehaviour
    {
        public EnemyAgent EnemyPrefab;
        public EnemyAgent Enemy;
        public EnemyAgent Init()
        {
            if (EnemyPrefab != null)
            {
                Enemy = Instantiate(EnemyPrefab, this.transform);
                Enemy.transform.localPosition = Vector3.zero;
                Enemy.name = EnemyPrefab.name;
                Enemy.Spawn();

                return Enemy;
            }
            else
            {
                Debug.LogError("No prefab linked");
                return null;
            }
        }

        public void OnDrawGizmos()
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(this.transform.position, 0.1f);
        }

        internal void ClearPickup()
        {
            Enemy = null;
        }
    }
}