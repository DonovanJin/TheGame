using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Jincom.Agent;
using System;

namespace Jincom.PickUps
{
    public class PickUpManager : MonoBehaviour
    {
        public PickUpSpawner[] Spawners;
        public PlayerAgent Player;
        public List<PickUpBase> PickUps = new List<PickUpBase>();
        public List<PickUpBase> CollectedPickups = new List<PickUpBase>();

        internal void Init(PlayerAgent currentPlayerAgent)
        {
            Player = currentPlayerAgent;

            for (int i = 0; i < Spawners.Length; i++)
            {
                PickUpBase pickup = Spawners[i].Init();

                if (pickup != null)
                {
                    PickUps.Add(pickup);
                }
            }
        }

        internal void UpdateManager()
        {
            //Check to see if player can collect any pickup
            for (int i = 0; i < PickUps.Count; i++)
            {
                if (PickUps[i].CanCollect(Player))
                {
                    CollectedPickups.Add(PickUps[i]);
                }
            }

            //Process collect pickups
            if (CollectedPickups.Count > 0)
            {
                for (int i = 0; i < CollectedPickups.Count; i++)
                {
                    PlayerCollectPickup(Player, CollectedPickups[i]);
                }

                //Clear list
                CollectedPickups.Clear();
            }
        }

        private void PlayerCollectPickup(PlayerAgent player, PickUpBase pickup)
        {
            PickUps.Remove(pickup);
            pickup.Collect(player);

            for (int i = 0; i < Spawners.Length; i++)
            {
                if (Spawners[i].PickUp == pickup)
                {
                    Spawners[i].ClearPickup();
                }
            }


            Destroy(pickup.gameObject);
            Debug.Log("Picked up object");
        }
    }
}
