using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Jincom.Camera
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

        private void Update()
        {
            UpdateCameraLogic();
        }

        private void UpdateCameraLogic()
        {
            AcquireTarget();
            FollowTarget();
        }

        private void AcquireTarget()
        {
            if (cameraMode == CameraMode.NormalGameplay)
            {
                TargetTransform = GameObject.FindGameObjectWithTag("Player").gameObject.GetComponent<Transform>();
            }
        }

        private void FollowTarget()
        {
            if (cameraMode == CameraMode.NormalGameplay)
            {
                transform.position = new Vector3(TargetTransform.position.x, TargetTransform.position.y, (-FollowDistance));
            }
        }
    }
}