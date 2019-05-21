using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Jincom.Agent
{
    public class EnemyAgent : AgentBase
    {
        public void Update()
        {
            AgentUpdate();
        }

        public override void AgentUpdate()
        {
            MoveAgent(Mathf.PingPong(Time.unscaledTime, 4f) - 2f);
        }
    }
}
