using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Jincom.Camera
{
    public class CameraLogic : MonoBehaviour
    {
        public GameObject CameraTarget;
        public float DistanceToTarget;

        public enum TargetType
        {
            Player,
            Other
        };
        public TargetType CameraTargetType;

        void Update()
        {
            DecideOnTarget();
            FollowTarget();
        }

        private void DecideOnTarget()
        {
            if (CameraTargetType == TargetType.Player)
            {
                if ((CameraTarget == null) || (CameraTarget.tag != "Player"))
                {
                    CameraTarget = GameObject.FindGameObjectWithTag("Player");
                }
            }
            else
            {
                if (CameraTarget != null)
                {
                    if (CameraTarget.tag == "Player")
                    {
                        CameraTarget = null;
                    }
                }
            }
        }

        private void FollowTarget()
        {
            if (CameraTarget != null)
            {
                transform.position = new Vector3(CameraTarget.transform.position.x, CameraTarget.transform.position.y, CameraTarget.transform.position.z - DistanceToTarget);
            }
        }
    }
}
