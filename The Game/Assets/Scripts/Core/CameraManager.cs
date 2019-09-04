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
        
        public Transform FollowTargetTransform;
        public float DefaultFollowDistance = 7f;
        public float CurrentFollowDistance;
        public float CameraHeightRelativeToPlayer = 1f;
        public float AdjustCameraUpDown, AdjustCameraLeftRight; 

        private float CameraFollowSpeed;
        private float HorizontalDifference;

        //  =   =   =   =   =   =   =   =   =   =   =   =

        private void Start()
        {
            StartCoroutine(StartOfLevel());
            DefaultFollowDistance = CurrentFollowDistance;
        }

        IEnumerator StartOfLevel()
        {
            yield return new WaitForSeconds(0.1f);
            InitialZoomOnPlayer();
        }

        private void InitialZoomOnPlayer()
        {
            if (cameraMode == CameraMode.FollowPlayer)
            {
                if ((!FollowTargetTransform) || (FollowTargetTransform.tag != "Player"))
                {
                    //HERMANN - acquire player through manager
                    FollowTargetTransform = GameObject.FindGameObjectWithTag("Player").gameObject.GetComponent<Transform>();
                    AdjustCameraUpDown = CameraHeightRelativeToPlayer;
                }

                //transform.position = new Vector3(TargetTransform.position.x + AdjustCameraLeftRight, TargetTransform.position.y + AdjustCameraUpDown, (-FollowDistance));
                transform.position = new Vector3(FollowTargetTransform.position.x, FollowTargetTransform.position.y + AdjustCameraUpDown, (-CurrentFollowDistance));
            }
        }

        //HERMANN - have Level Manager update Camera Logic?
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
            if ((FollowTargetTransform == null) || (FollowTargetTransform.tag != "Player"))
            {
                //I know I should be able to acquire the player's transform through the managers, but I'm not sure how
                FollowTargetTransform = GameObject.FindGameObjectWithTag("Player").gameObject.GetComponent<Transform>();
                AdjustCameraUpDown = CameraHeightRelativeToPlayer;
            }
        }

        public void AssignNewTarget(Transform newTarget)
        {
            FollowTargetTransform = newTarget;
        }

        //  =   =   =   =   =   =   =   =   =   =   =   =

        private void FollowTarget()
        {
            HorizontalDifference = Mathf.Abs(this.transform.position.y - FollowTargetTransform.position.y) - AdjustCameraUpDown;

            if (HorizontalDifference < 2.5f)
            {
                if (Input.GetAxis("Horizontal") == 0)
                {
                    CameraFollowSpeed = 1f;
                }
                else
                {
                    CameraFollowSpeed = 2f;
                }

                AdjustCameraToOffset();

                if (FollowTargetTransform)
                {
                    Vector3 targetVector3 = new Vector3(FollowTargetTransform.position.x + AdjustCameraLeftRight, FollowTargetTransform.position.y + AdjustCameraUpDown, (-CurrentFollowDistance));

                    transform.position = Vector3.Lerp(this.transform.position, targetVector3, Time.deltaTime * CameraFollowSpeed);
                }
            }
            else
            {
                transform.position = new Vector3(this.transform.position.x, FollowTargetTransform.position.y + 4f, -CurrentFollowDistance);
            }
        }

        private void AdjustCameraToOffset()
        {
            float _newOffset;

            if (FollowTargetTransform.GetComponent<PlayerAgent>().IsPlayerRunning())
            {
                _newOffset = 6f;
            }
            else
            {
                _newOffset = 3f;
            }

            if (cameraMode == CameraMode.FollowPlayer)
            {
                if (!FollowTargetTransform.GetComponent<PlayerAgent>().IsPlayerBackpedalling())
                {
                    if (Input.GetAxis("Horizontal") < 0)
                    {
                        AdjustCameraLeftRight = -_newOffset;
                    }
                    else if (Input.GetAxis("Horizontal") > 0)
                    {
                        AdjustCameraLeftRight = _newOffset;
                    }
                    else
                    {
                        AdjustCameraLeftRight = 0f;
                    }
                }
                else
                {
                    AdjustCameraLeftRight = 0f;
                }
            }
        }
    }
}