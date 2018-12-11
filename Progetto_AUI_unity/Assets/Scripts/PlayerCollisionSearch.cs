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

    public ArrayList collectiblesFound;

    public Transform terrain;

    public Canvas canvas;

    public GameObject dolphin;

    public bool magnifierUsed = false;

    public GameObject magnifierFocus;

    public GameObject cameraSearch;

    public bool exitFromCompletedArea = false; //boolean to remember to execute the else if part of OnTriggerStay just one time per collectibleArea

    void Start()
    {
        collectiblesFound = new ArrayList();
        magnifierFocus.SetActive(false);
    }

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.tag == "CollectibleArea" && counter.collectiblesMap[collider.gameObject][2] == 0)
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
        if (collider.tag == "Collectible" && !collectiblesFound.Contains(collider))
        {
                Debug.Log("Collectible found");

                Vector3 newCollectiblePosition = new Vector3(collider.transform.position.x, terrain.position.y, collider.transform.position.z);
                collider.transform.SetPositionAndRotation(newCollectiblePosition, collider.transform.rotation);

                collectiblesFound.Add(collider);
                
                counter.CollectibleFound(collider.gameObject);
            
        }
    }
    
    private void OnTriggerStay(Collider collider)
    {
        Debug.Log("ONTRIGGERSTAY");
        if (collider.tag == "CollectibleArea")
        {
            int areaCompleted = counter.collectiblesMap[collider.gameObject][2];//integer set to 1 if all the collectibles inside this area are found, 0 otherwise
            Debug.Log("Area Completed : " + areaCompleted);
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
                cameraSearch.transform.position = new Vector3(collider.transform.position.x, cameraSearch.transform.position.y, collider.transform.position.z);
                magnifierFocus.SetActive(true);
                MagnifierMovement.SetSearchPhase(true);
                //Display.displays[1].Activate();
                dolphin.GetComponent<Animation>().Stop("Idle");
                movement.enabled = false;
                dolphin.GetComponent<Animation>().PlayQueued("DolphinWaitingForSearchStart");
                magnifierUsed = true;
            }
            else if (magnifierUsed && !exitFromCompletedArea && counter.collectiblesMap.ContainsKey(collider.gameObject) && (areaCompleted == 1 || (Input.anyKey && Input.GetKey(KeyCode.C)))) //if the user finds all the collectibles in the area
            {
                Debug.Log("AIAOAOAOAOAOAOAOAO");
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
                //Display.displays[0].Activate();
                magnifierFocus.SetActive(false);
                MagnifierMovement.SetSearchPhase(false);
            }
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
                }
            }
            //dolphin.GetComponent<Animation>().PlayQueued("DolphinWaitingForSearchEnd");
            magnifierUsed = false;
            magnifierFocus.SetActive(false);
            MagnifierMovement.SetSearchPhase(false);
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
