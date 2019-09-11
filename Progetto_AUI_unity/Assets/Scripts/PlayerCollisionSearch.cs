using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;
using TMPro;
using UnityStandardAssets.Utility;
using System.Net;
using System.Security.Cryptography;

public class PlayerCollisionSearch : MonoBehaviour
{

    public PlayerMovementSearch movement;

    public CollectiblesCounter counter;

    public ArrayList collectiblesFound;

    public LightShifting light_Shift;

    public Transform terrain;

    public Canvas canvasPlayerCamera;
    public Canvas canvasCameraSearch;

    public GameObject dolphin;

    public bool magnifierUsed = false;

    public GameObject magnifierFocus;

    public GameObject cameraSearch;
    public GameObject cameraMain2;


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
            Image[] images = canvasPlayerCamera.GetComponentsInChildren<Image>();
            for (int i = 0; i < images.Length; i++)
            {
                if (images[i].name == "Magnifier")
                {
                    images[i].GetComponent<Image>().enabled = true;
                }
            }
            MagicRoomLightManager.instance.sendColour("#FFC2AAFA", 255);
            exitFromCompletedArea = false;
        }
        if (collider.tag == "Collectible" && !collectiblesFound.Contains(collider))
        {
            StartCoroutine(ShowImageInterval(canvasCameraSearch, "Seastar", 2));

            Debug.Log("Collectible found");

                Vector3 newCollectiblePosition = new Vector3(collider.transform.position.x, terrain.position.y, collider.transform.position.z);
                collider.transform.SetPositionAndRotation(newCollectiblePosition, collider.transform.rotation);

                collectiblesFound.Add(collider);
                
                counter.CollectibleFound(collider.gameObject);
        }
    }
    
    private void OnTriggerStay(Collider collider)
    {
        if (collider.tag == "CollectibleArea")
        {
            int areaCompleted = counter.collectiblesMap[collider.gameObject][2];//integer set to 1 if all the collectibles inside this area are found, 0 otherwise
            Debug.Log("Area Completed : " + areaCompleted);
            if ((Input.anyKey && Input.GetKey(KeyCode.M) || GameObject.Find("Dolphin1").GetComponent<SmartToy>().rfidsensor.cardReader[7].read) && !magnifierUsed && areaCompleted == 0) //if the user uses the Magnifier RFID
            {
                Image[] images = canvasPlayerCamera.GetComponentsInChildren<Image>();
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
                cameraMain2.SetActive(false);
                cameraSearch.SetActive(true);
                cameraSearch.transform.position = new Vector3(collider.transform.position.x, cameraSearch.transform.position.y, collider.transform.position.z);
                SetChildActivation(collider.gameObject, "Container", true);
                magnifierFocus.SetActive(true);
                MagnifierMovement.SetSearchPhase(true);
                //Display.displays[1].Activate();
                dolphin.GetComponent<Animation>().Stop("Idle");
                movement.enabled = false;
                GetComponent<Rigidbody>().velocity = Vector3.zero;
                GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
                GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePosition | RigidbodyConstraints.FreezeRotation;
                dolphin.GetComponent<Animation>().PlayQueued("DolphinWaitingForSearchStart");
                magnifierUsed = true;
            }
            else if (magnifierUsed && !exitFromCompletedArea && counter.collectiblesMap.ContainsKey(collider.gameObject) && areaCompleted == 1 ) //if the user finds all the collectibles in the area
            {
                Image[] images = canvasPlayerCamera.GetComponentsInChildren<Image>();
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
                StartCoroutine(ShowTextInterval(canvasPlayerCamera, "Area Completed Text", 5));
                StartCoroutine(rewardLightInterval("#00c300", 4));
                StartCoroutine(CameraSwitch(cameraSearch, cameraMain2, 3));
                dolphin.GetComponent<Animation>().PlayQueued("DolphinWaitingForSearchEnd");
                GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotation;
                movement.enabled = true;
                exitFromCompletedArea = true;
                //Display.displays[0].Activate();
                SetChildActivation(collider.gameObject, "Container", false);
                magnifierFocus.SetActive(false);
                MagnifierMovement.SetSearchPhase(false);
            }
        }
    }


    private void OnTriggerExit(Collider collider)
    {
        if (collider.tag == "CollectibleArea" && !MagnifierMovement.GetSearchPhase())
        {
            Image[] images = canvasPlayerCamera.GetComponentsInChildren<Image>();
            for (int i = 0; i < images.Length; i++)
            {
                if (images[i].name == "Magnifier")
                {
                    images[i].GetComponent<Image>().enabled = false;
                }
            }
            //dolphin.GetComponent<Animation>().PlayQueued("DolphinWaitingForSearchEnd");
            magnifierUsed = false;
            SetChildActivation(collider.gameObject, "Container", false);
            magnifierFocus.SetActive(false);
            MagnifierMovement.SetSearchPhase(false);
        }
    }


    IEnumerator ShowImageInterval(Canvas canvas, String imageName, int seconds){
    
        Image imageRequested = null;

        Image[] images = canvas.GetComponentsInChildren<Image>();

        for (int i = 0; i < images.Length; i++)
        {
            if (images[i].name == imageName)
            {
                imageRequested = images[i].GetComponent<Image>();
            }
        }

        imageRequested.enabled = true;

        yield return new WaitForSeconds(seconds);

        imageRequested.enabled = false;
    }

    IEnumerator rewardLightInterval(String color, int seconds)
    {
        light_Shift.pause = true;
        MagicRoomLightManager.instance.sendColour("#00c300", 255);

        yield return new WaitForSeconds(seconds);

        light_Shift.pause = false;

    }

    private IEnumerator ShowTextInterval(Canvas canvas, String textName, int seconds)
    {
        TextMeshProUGUI[] texts = canvas.GetComponentsInChildren<TextMeshProUGUI>();

        for (int i = 0; i < texts.Length; i++)
        {
                Debug.Log(texts[i].name + " = " + textName);
                if (texts[i].name == textName)
                {
                    texts[i].fontSize = 150;

                    yield return new WaitForSeconds(seconds);

                    texts[i].fontSize = 0;
                }
           /*
           if (text.name == "Area Completed Text")
           {
               text.fontSize = 150;
               yield return new WaitForSeconds(seconds);
               text.fontSize = 0;

           }
           */
        }
    }


    private void SetChildActivation(GameObject gameobject, string childName, bool value){
        for (int i = 0; i < gameobject.transform.childCount; i++){
            GameObject child = gameobject.transform.GetChild(i).gameObject;
            if (child.name == childName){
                child.SetActive(value);
            }
        }
    }

    private IEnumerator CameraSwitch(GameObject cameraOut, GameObject cameraIn, int seconds)
    {
        yield return new WaitForSeconds(seconds);

        cameraSearch.SetActive(false);
        cameraMain2.SetActive(true);
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
