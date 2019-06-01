using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Jincom.Agent;

namespace Jincom.PickUps
{
    public abstract class PickUpBase : MonoBehaviour
    {
        protected float _checkDistance = 0.3f;  

        public virtual void Spawn()
        {
            Debug.Log("spawned " +this.name);
        }

        public bool CanCollect(PlayerAgent player)
        {
            float distance = Vector3.Distance(this.transform.position, player.transform.position);

            if (distance < _checkDistance)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public virtual void Collect(PlayerAgent player)
        {
            Debug.Log("Collected " + this.name);
        }
    }
}