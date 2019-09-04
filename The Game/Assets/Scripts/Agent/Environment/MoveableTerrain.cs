using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Jincom.Agent;

public class MoveableTerrain : MonoBehaviour
{
    public float MinDistanceToPlayer;
    public bool ThisTerrainIsGrabbed = false;

    private void Start()
    {
    }

    void Update()
    {
        Foo();
    }
    //HERMANN - Acquire player a better way
    private void Foo()
    {
        if (GameObject.FindGameObjectWithTag("Player"))
        {
            if (GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerAgent>().RayHit.collider)
            {
                if (GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerAgent>().RayHit.collider.GetComponent<MoveableTerrain>())
                {
                    if (Input.GetKeyDown(KeyCode.E))
                    {
                        Debug.Log("Grabbing!");
                        Transform playerTransform = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerAgent>().RayHit.collider.transform;
                        PlayerGrabsTerrainPiece(playerTransform);
                    }
                }
            }
        }
    }

    private void PlayerGrabsTerrainPiece(Transform PlayerPosition)
    {
        if ((GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerAgent>().RayHit.distance) < MinDistanceToPlayer)
        {
            if (!ThisTerrainIsGrabbed)
            {
                ThisTerrainIsGrabbed = true;
                Debug.Log("You grabbed me!");
            }
            else
            {
                ThisTerrainIsGrabbed = false;
                Debug.Log("You let me go!");
            }
        }
    }
}
