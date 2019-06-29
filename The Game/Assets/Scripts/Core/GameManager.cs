using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Jincom.Agent;
using Jincom.UI;
using Jincom.Level;

namespace Jincom.Core
{
    public class GameManager : MonoBehaviour
    {
        public Player PlayerData;
        public UIManager UImanager;
        public LevelSpawner Levelspawner;
        // Start is called before the first frame update
        void Start()
        {
            Init();
        }

        // Update is called once per frame
        void Update()
        {
            UImanager.UpdateManager();
            Levelspawner.UpdateSpawner();
        }

        private void Init()
        {
            Debug.Assert(UImanager != null, "No UI Manager Found");
            Debug.Assert(Levelspawner != null, "No Level Spawner Found");

            PlayerData = new Player();
            UImanager.Init(PlayerData);
            Levelspawner.SpawnLevel(0, PlayerData);
            
        }
    }
}