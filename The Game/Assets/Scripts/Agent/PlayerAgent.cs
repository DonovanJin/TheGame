using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Jincom.Agent
{
    public class PlayerAgent : AgentBase
    {
        public void Start()
        {
            MoveAgent();
        }

        public override void MoveAgent()
        {
            base.MoveAgent();
            Debug.Log("Move Player");
        }
    }
}
