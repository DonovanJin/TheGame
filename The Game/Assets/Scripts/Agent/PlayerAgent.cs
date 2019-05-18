using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Jincom.Agent
{
    public class PlayerAgent : AgentBase
    {
        public void Update()
        {
            AgentUpdate();
        }

        public override void AgentUpdate()
        {
            MoveAgent(Input.GetAxis("Horizontal"));
        }
    }
}
