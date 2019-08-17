using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Jincom.CameraLogic
{
    public class CameraManager : MonoBehaviour
    {    
        public enum CameraMode
        {
            NormalGameplay,
            Cinematic
        };
        public CameraMode cameraMode;
        public Transform TargetTransform;
        public float FollowDistance;

        //  =   =   =   =   =   =   =   =   =   =   =   =

        private void Update()
        {
            UpdateCameraLogic();
        }

        //  =   =   =   =   =   =   =   =   =   =   =   =

        private void UpdateCameraLogic()
        {
            AcquireTarget();
            FollowTarget();            
        }

        //  =   =   =   =   =   =   =   =   =   =   =   =

            //Remove
        private void AcquireTarget()
        {
            if (cameraMode == CameraMode.NormalGameplay)
            {
                TargetTransform = GameObject.FindGameObjectWithTag("Player").gameObject.GetComponent<Transform>();
            }
        }

        public void AssignTarget(Agent.AgentBase agent)
        {
            TargetTransform = agent.transform;
        }

        //  =   =   =   =   =   =   =   =   =   =   =   =

        private void FollowTarget()
        {
            if (cameraMode == CameraMode.NormalGameplay)
            {
                transform.position = new Vector3(TargetTransform.position.x, TargetTransform.position.y, (-FollowDistance));
            }
        }

        //  =   =   =   =   =   =   =   =   =   =   =   =

        /// <summary>
        /// Allow camera to follow a specific target if not in normal gameplay mode
        /// </summary>
        /// <param name="NewTarget"> Pass the Transform component of new camera target </param>
        private void AssignNewTarget(Transform NewTarget)
        {
            if (cameraMode != CameraMode.NormalGameplay)
            {
                TargetTransform = NewTarget;
            }
        }
    }
}