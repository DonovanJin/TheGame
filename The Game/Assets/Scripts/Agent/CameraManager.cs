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

        //public enum CameraOffset
        //{
        //    Center,
        //    Left,
        //    Right
        //};

        //public CameraOffset Offset;

        public Transform TargetTransform;
        public float DefaultFollowDistance ,CurrentFollowDistance;
        public float CameraHeightRelativeToPlayer = 1f;
        public float AdjustCameraUpDown, AdjustCameraLeftRight;
        public float CameraFollowSpeed;
        public float HorizontalDifference;

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
                if ((!TargetTransform) || (TargetTransform.tag != "Player"))
                {
                    //I know I should be able to acquire the player's transform through the managers, but I'm not sure how
                    TargetTransform = GameObject.FindGameObjectWithTag("Player").gameObject.GetComponent<Transform>();
                    AdjustCameraUpDown = CameraHeightRelativeToPlayer;
                }

                //transform.position = new Vector3(TargetTransform.position.x + AdjustCameraLeftRight, TargetTransform.position.y + AdjustCameraUpDown, (-FollowDistance));
                transform.position = new Vector3(TargetTransform.position.x, TargetTransform.position.y + AdjustCameraUpDown, (-CurrentFollowDistance));
            }
        }

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
        public void AssignNewTarget(Transform newTarget)
        {
            //TargetTransform = agent.transform;
            TargetTransform = newTarget;
        }

        //  =   =   =   =   =   =   =   =   =   =   =   =

        private void FollowTarget()
        {
            HorizontalDifference = Mathf.Abs(this.transform.position.y - TargetTransform.position.y) - AdjustCameraUpDown;

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

                if (TargetTransform)
                {
                    //transform.position = new Vector3(TargetTransform.position.x + AdjustCameraLeftRight, TargetTransform.position.y + AdjustCameraUpDown, (-FollowDistance));
                    Vector3 targetVector3 = new Vector3(TargetTransform.position.x + AdjustCameraLeftRight, TargetTransform.position.y + AdjustCameraUpDown, (-CurrentFollowDistance));

                    transform.position = Vector3.Lerp(this.transform.position, targetVector3, Time.deltaTime * CameraFollowSpeed);
                }
            }
            else
            {
                //CameraFollowSpeed = 10f;
                transform.position = new Vector3(this.transform.position.x, TargetTransform.position.y + 4f, -CurrentFollowDistance);
            }

            //AdjustCameraToOffset();

            //if (TargetTransform)
            //{
            //    //transform.position = new Vector3(TargetTransform.position.x + AdjustCameraLeftRight, TargetTransform.position.y + AdjustCameraUpDown, (-FollowDistance));
            //    Vector3 targetVector3 = new Vector3(TargetTransform.position.x + AdjustCameraLeftRight, TargetTransform.position.y + AdjustCameraUpDown, (-CurrentFollowDistance));

            //    transform.position = Vector3.Lerp(this.transform.position, targetVector3, Time.deltaTime * CameraFollowSpeed);
            //}
        }

        private void AdjustCameraToOffset()
        {
            float _newOffset;

            if (TargetTransform.GetComponent<PlayerAgent>().IsPlayerRunning())
            {
                _newOffset = 6f;
            }
            else
            {
                _newOffset = 3f;
            }

            if (cameraMode == CameraMode.FollowPlayer)
            {
                if (!TargetTransform.GetComponent<PlayerAgent>().IsPlayerBackpedalling())
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


            //switch (Offset)
            //{
            //    case CameraOffset.Left:
            //        AdjustCameraLeftRight = -3f;
            //        break;
            //    case CameraOffset.Right:
            //        AdjustCameraLeftRight = 3f;
            //        break;
            //    case CameraOffset.Center:
            //        AdjustCameraLeftRight = 0f;
            //        break;
            //    default:
            //        break;
            //}
        }
    }
}