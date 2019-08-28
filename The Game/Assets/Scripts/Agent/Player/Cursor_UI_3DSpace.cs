using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Jincom.CameraLogic;

public class Cursor_UI_3DSpace : MonoBehaviour
{
    private Vector3 point;
    private Vector3 CanvasCursorPosition;
    private Vector3 WorldSpaceTargetPosition;    
    public CameraManager cameraManager;

    //Cursor is the cross hair and is a UI element
    //Target is the point in 3D space that the player actually aims at
    public enum CanvasSpaceOrWorldSpace
    {
        CanvasCursor,
        WorldTarget
    };

    public CanvasSpaceOrWorldSpace CanvasOrWorld;

    private void Start()
    {
        cameraManager = Camera.main.GetComponent<CameraManager>();
    }

    void Update()
    {
        if (Camera.main.pixelRect.Contains(Input.mousePosition))
        {
            point = Camera.main.ScreenToViewportPoint(Input.mousePosition);
        }

        CanvasCursorPosition = new Vector3(Screen.width * point.x, Screen.height * point.y, 0f);
        WorldSpaceTargetPosition = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, cameraManager.FollowDistance));

        if (CanvasOrWorld == CanvasSpaceOrWorldSpace.CanvasCursor)
        {
            transform.position = CanvasCursorPosition;
        }
        else
        {
            transform.position = WorldSpaceTargetPosition;
        }
    }
}

