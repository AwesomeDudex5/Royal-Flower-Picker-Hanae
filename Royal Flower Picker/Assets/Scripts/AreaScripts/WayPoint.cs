using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WayPoint : MonoBehaviour
{
    public Transform nextWaypoint;
    private Transform spawnpoint;
    private Transform playerTransform;

    private void Start()
    {
        spawnpoint = nextWaypoint.GetChild(0).transform;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            playerTransform = collision.transform;
            playerTransform.position = spawnpoint.position;
        }
    }

}
