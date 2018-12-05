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

    public GameObject dolphin;

    public bool magnifierUsed = false;

    public bool exitFromCompletedArea = false; //boolean to remember to execute the else if part of OnTriggerStay just one time per collectibleArea

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.tag == "CollectibleArea")
        {         
            Image[] images = canvas.GetComponentsInChildren<Image>();
            for (int i = 0; i < images.Length; i++)
            {
                if (images[i].name == "Magnifier")
                {
                    images[i].GetComponent<Image>().enabled = true;
                }
            }
            MagicRoomLightManager.instance.sendColour("#A47C18", 255);
            exitFromCompletedArea = false;
        }
        if (collider.tag == "Collectible")
        {

            Debug.Log("Collectible found");

            Vector3 newCollectiblePosition = new Vector3(collider.transform.position.x, terrain.position.y, collider.transform.position.z);
            collider.transform.SetPositionAndRotation(newCollectiblePosition, collider.transform.rotation);

            counter.CollectibleFound(collider.gameObject);
        }
    }
    
    private void OnTriggerStay(Collider collider)
    {
        if (collider.tag == "CollectibleArea")
        {
            int areaCompleted = counter.collectiblesMap[collider.gameObject][2];
            if (Input.anyKey && Input.GetKey(KeyCode.M) && !magnifierUsed && areaCompleted == 0) //if the user uses the Magnifier RFID
            {
                Image[] images = canvas.GetComponentsInChildren<Image>();
                for (int i = 0; i < images.Length; i++)
                {
                    if (images[i].name == "Magnifier")
                    {
                        images[i].GetComponent<Image>().enabled = false;
                    }
                    if (images[i].name == "SearchIllustration")
                    {
                        images[i].GetComponent<Image>().enabled = true;
                    }
                }
                dolphin.GetComponent<Animation>().Stop("Idle");
                movement.enabled = false;
                dolphin.GetComponent<Animation>().PlayQueued("DolphinWaitingForSearchStart");
                magnifierUsed = true;
            }
            else if (magnifierUsed && !exitFromCompletedArea && counter.collectiblesMap.ContainsKey(collider.gameObject) && (areaCompleted == 1 || (Input.anyKey && Input.GetKey(KeyCode.C)))) //if the user finds all the collectibles in the area
            { 
                Image[] images = canvas.GetComponentsInChildren<Image>();
                for (int i = 0; i < images.Length; i++)
                {
                    if (images[i].name == "Magnifier")
                    {
                        images[i].GetComponent<Image>().enabled = false;
                    }
                    if (images[i].name == "SearchIllustration")
                    {
                        images[i].GetComponent<Image>().enabled = false;
                    }
                }
                dolphin.GetComponent<Animation>().PlayQueued("DolphinWaitingForSearchEnd");
                movement.enabled = true;
                exitFromCompletedArea = true;
            }
        }
    }


    private void OnTriggerExit(Collider collider)
    {
        if (collider.tag == "CollectibleArea")
        {
            /*Image[] images = canvas.GetComponentsInChildren<Image>();
            for (int i = 0; i < images.Length; i++)
            {
                if (images[i].name == "Magnifier")
                {
                    images[i].GetComponent<Image>().enabled = false;
                }
            }
            dolphin.GetComponent<Animation>().PlayQueued("DolphinWaitingForSearchEnd");*/
            magnifierUsed = false;
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
