using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Jincom.Agent;

namespace Jincom.CameraLogic
{
    public class CameraManager : MonoBehaviour
    {    
        public enum CameraMode
        {
            FollowPlayer,
            FollowSomethingElse,
            Cinematic
        };
        public CameraMode cameraMode;
        public Transform TargetTransform;
        public float FollowDistance;
        public float CameraHeightRelativeToPlayer = 1f;
        public float AdjustCameraUpDown, AdjustCameraLeftRight;

        //  =   =   =   =   =   =   =   =   =   =   =   =

        //Have another Manager update Camera Logic
        private void Update()
        {
            UpdateCameraLogic();
        }

        //  =   =   =   =   =   =   =   =   =   =   =   =

        private void UpdateCameraLogic()
        {
            ChooseWhatToFollow();
        }

        //  =   =   =   =   =   =   =   =   =   =   =   =

        
        private void ChooseWhatToFollow()
        {
            if (cameraMode == CameraMode.FollowPlayer)
            {
                AcquirePlayerLocation();
                FollowTarget();
            }
            else if (cameraMode == CameraMode.FollowSomethingElse)
            {
                FollowTarget();
            }
        }

            //Remove
        private void AcquirePlayerLocation()
        {
            if ((TargetTransform == null) || (TargetTransform.tag != "Player"))
            {
                //I know I should be able to acquire the player's transform through the managers, but I'm not sure how
                TargetTransform = GameObject.FindGameObjectWithTag("Player").gameObject.GetComponent<Transform>();
                AdjustCameraUpDown = CameraHeightRelativeToPlayer;
            }
        }

        //Follow a new target in 'Normal Gameplay Mode'
        //public void AssignNewTarget(Agent.AgentBase agent)
        public void AssignNewTarget(Transform newTarget)
        {
            //TargetTransform = agent.transform;
            TargetTransform = newTarget;
        }

        //  =   =   =   =   =   =   =   =   =   =   =   =

        private void FollowTarget()
        {
            if (TargetTransform != null)
            {
                transform.position = new Vector3(TargetTransform.position.x + AdjustCameraLeftRight, TargetTransform.position.y + AdjustCameraUpDown, (-FollowDistance));
            }
        }
    }
}