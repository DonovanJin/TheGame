using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Jincom.PickUps
{
    public class PickUpSpawner : MonoBehaviour
    {
        public PickUpBase PickUpPrefab;
        public PickUpBase PickUp;

        // Start is called before the first frame update

        private void Start()
        {
            Init();
        }

        public void Init()
        {
            if (PickUpPrefab != null)
            {
                PickUp = Instantiate(PickUpPrefab, this.transform);
                PickUp.name = PickUpPrefab.name;
                PickUp.Spawn();
            }
            else
            {
                Debug.LogError("You dumb ass, No prefab linked");
            }
        }


    }
}
