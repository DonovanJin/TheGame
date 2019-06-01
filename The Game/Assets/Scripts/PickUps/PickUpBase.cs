using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Jincom.PickUps
{
    public abstract class PickUpBase : MonoBehaviour
    {
        public virtual void Spawn()
        {
            Debug.Log("spawned " +this.name);
        }

        public virtual void Collect()
        {

        }
    }
}