using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;
using UnityStandardAssets.Utility;
using System.Net;
using System.Security.Cryptography;

public class PlayerCollisionSearch : MonoBehaviour
{

    public PlayerMovementSearch movement;

    public CollectiblesCounter counter;

    public Transform terrain;

    public Canvas canvas;

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.tag == "CollectibleArea")
        {
            /*            Camera[] cameras = new Camera[2];
                        Camera.GetAllCameras(cameras);
                        for (int i = 0; i < cameras.Length; i++)
                            if (cameras[i].name == "Camera")
                            {
                                Debug.Log(cameras.ToString());
                                Camera.SetupCurrent(cameras[i]);
                            }
            */

            Image[] images = canvas.GetComponentsInChildren<Image>();
            for (int i = 0; i < images.Length; i++)
            {
                if (images[i].name == "Magnifier")
                {
                    images[i].GetComponent<Image>().enabled = true;
                }
            }
        }

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
