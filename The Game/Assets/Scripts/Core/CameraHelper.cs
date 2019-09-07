using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Jincom.CameraLogic
{
    public static class CameraHelper
    {
        public static Vector3 GetCanvasPosition()
        {
            Vector3 point = Vector3.zero;
            if (Camera.main.pixelRect.Contains(Input.mousePosition))
            {
                point = Camera.main.ScreenToViewportPoint(Input.mousePosition);
            }

            return new Vector3(Screen.width * point.x, Screen.height * point.y, 0f);
        }

        public static Vector3 GetWorldPosition(float followDistance)
        {
            Vector3 point = Vector3.zero;
            if (Camera.main.pixelRect.Contains(Input.mousePosition))
            {
                point = Camera.main.ScreenToViewportPoint(Input.mousePosition);
            }

            return Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, followDistance));
        }

        public static bool IsLeftOfScreen()
        {
            return GetCanvasPosition().x < (Screen.width * 0.5f);
        }

        public static float DistanceToCamera(Transform transform)
        {
            return Mathf.Abs(transform.position.z - Camera.main.transform.position.z);
        }
    }
}