using UnityEngine;
using System;
using UnityStandardAssets.Utility;
using System.Net;
using System.Security.Cryptography;

public class PlayerCollisionSearch : MonoBehaviour
{

    public PlayerMovementSearch movement;

    public CollectiblesCounter counter;

    public Transform terrain;



    private void OnTriggerEnter(Collider collider)
    {
        if (collider.tag == "Collectible")
        {

            Debug.Log("Collectible found");

            Vector3 newCollectiblePosition = new Vector3(collider.transform.position.x, terrain.position.y, collider.transform.position.z);
            collider.transform.SetPositionAndRotation(newCollectiblePosition, collider.transform.rotation);

            counter.CollectibleFound();
        }
    }



    void Update()
    {

    }










}
