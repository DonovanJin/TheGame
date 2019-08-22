using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cursor_UI_3DSpace : MonoBehaviour
{
    private Vector3 point;
    private Vector3 CanvasCursorPosition;
    private Vector3 WorldSpaceTargetPosition;
    private float DistanceBetweenTargetAndCamera;

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
        //DistanceBetweenTargetAndCamera = Vector3.Distance(transform.position, Camera.main.gameObject.transform.position);
        DistanceBetweenTargetAndCamera = Mathf.Abs(transform.position.z - Camera.main.gameObject.transform.position.z);
    }

    void Update()
    {
        if (Camera.main.pixelRect.Contains(Input.mousePosition))
        {
            point = Camera.main.ScreenToViewportPoint(Input.mousePosition);
        }

        CanvasCursorPosition = new Vector3(Screen.width * point.x, Screen.height * point.y, 0f);
        WorldSpaceTargetPosition = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, DistanceBetweenTargetAndCamera));
        //WorldSpaceTargetPosition = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0f));

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

