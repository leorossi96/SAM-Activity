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

    public RotationsSlow virtualDolpihnRotation;

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.tag == "CollectibleArea")
        {
            /*          Camera[] cameras = new Camera[2];
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
                    virtualDolpihnRotation.setUpRotation(new Vector3(0, -180, 0));
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

    private void OnTriggerExit(Collider collider)
    {
        if (collider.tag == "CollectibleArea")
        {
            Image[] images = canvas.GetComponentsInChildren<Image>();
            for (int i = 0; i < images.Length; i++)
            {
                if (images[i].name == "Magnifier")
                {
                    images[i].GetComponent<Image>().enabled = false;
                    virtualDolpihnRotation.setUpRotation(new Vector3(0, -180, 0));
                }
            }
        }
    }

   /* IEnumerator FadeTo(float aValue, float aTime)
    {
        float alpha = transform.renderer.material.color.a;
        for (float t = 0.0f; t < 1.0f; t += Time.deltaTime / aTime)
        {
            Color newColor = new Color(1, 1, 1, Mathf.Lerp(alpha, aValue, t));
            transform.renderer.material.color = newColor;
            yield return null;
        }
    }

    private IEnumerator FadeImage(Image image){
        image.CrossFadeAlpha(1, 2.0f, false);

    }
*/

    void Update()
    {

    }
}
