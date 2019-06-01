using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Jincom.Agent;
using System;

namespace Jincom.PickUps
{
    public class PickUpSpawner : MonoBehaviour
    {
        public PickUpBase PickUpPrefab;
        public PickUpBase PickUp;

        public PickUpBase Init()
        {
            if (PickUpPrefab != null)
            {
                PickUp = Instantiate(PickUpPrefab, this.transform);
                PickUp.name = PickUpPrefab.name;
                PickUp.Spawn();

                return PickUp;
            }
            else
            {
                Debug.LogError("You dumb ass, No prefab linked");
                return null;
            }
        }

        public void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(this.transform.position, 0.1f);
        }

        internal void ClearPickup()
        {
            PickUp = null;
        }
    }
}
