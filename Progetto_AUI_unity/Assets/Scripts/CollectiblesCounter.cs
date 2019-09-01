using System.Collections;
using System;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;
using UnityEngine;

public class CollectiblesCounter : MonoBehaviour {

    public PlayerMovementSearch movement;
    public int nCollectibles;
    int totalCollectiblesFound;
    public GameObject dolphin;
    public Canvas canvasPlayerCamera;
    public Canvas canvasCameraSearch;
    public PlayerCollisionSearch playerCollisionSearch;
    public SessionParameters sessionParameters;


    SmartToy Dolphin;

    public Dictionary<GameObject, int[]> collectiblesMap = new Dictionary<GameObject, int[]>(); //maps the collectible are with an array of 2 integer: the first is the number of relevant collectibles inside the area while the second is the number of collectibles found in that area

    public void Start()
    {  
        for (int i = 0; i < GameObject.FindGameObjectsWithTag("CollectibleArea").Length; i++){
            GameObject currentCollectibleArea = GameObject.FindGameObjectsWithTag("CollectibleArea")[i];
            int[] currentCollectibles = new int[3];
            currentCollectibles[0] = GetCollectibleChildCount(currentCollectibleArea.transform, "Collectible"); //UPDATE NEEDED: aggiungi filtro per contare solo i figli con un certo tag (non tutti i figli sono collezionabili utili)
            currentCollectibles[1] = 0;
            currentCollectibles[2] = 0; //0 if there're still collectibles to be found in the CollectibleArea, else 1
            collectiblesMap.Add(currentCollectibleArea, currentCollectibles);
        }
        totalCollectiblesFound = 0;
        nCollectibles = GameObject.FindGameObjectsWithTag("Collectible").Length;
        Debug.Log("Total Collectibles in the map "+ nCollectibles);

    }

    public void CollectibleFound(GameObject collider)
    {
        int[] collectiblesCounters = collectiblesMap[collider.transform.parent.gameObject];
        totalCollectiblesFound ++;
        collectiblesCounters[1]++;
        Debug.Log("In this area you found : " + collectiblesCounters[1] + " out of " + collectiblesCounters[0] + " collectibles.");
        /*if(Dolphin == null){
            Dolphin = GameObject.Find("Dolphin1").GetComponent<SmartToy>();
        }
        Dolphin.executeCommandLightController(Color.blue, 10, "parthead");
            Debug.Log("LUCE DELFINO ACCESA");*/

        //MagicRoomSmartToyManager.instance.sendCommandExecuteSmartToy("Dolphin1", "partrightfin");

        //VoicesOffline voice = MagicRoomTextToSpeachManagerOffline.instance.listofAssociatedNames[2];*/
        if (collectiblesCounters[1] < collectiblesCounters[0])
        {
           // MagicRoomTextToSpeachManagerOffline.instance.generateAudioFromText("Hai trovato un collezionabile! Cerca gli altri nella mappa", voice);
        }
        if (collectiblesCounters[1] == collectiblesCounters[0] && totalCollectiblesFound < nCollectibles)
        {
            collectiblesCounters[2] = 1; //0 if there're still collectibles in the CollectibleArea, else 1
            //MagicRoomLightManager.instance.sendColour(Color.red);
            //MagicRoomTextToSpeachManagerOffline.instance.generateAudioFromText("Complimenti, hai trovato tutti gli oggetti in quest'area. Cerhiamone altri in giro per la mappa!", voice);

        }
        else if (totalCollectiblesFound >= nCollectibles){
            sessionParameters.endGame = true;
            Debug.Log("HAI VINTO");
            StartCoroutine(BubbleMachine());
            movement.enabled = false;
            playerCollisionSearch.enabled = false;
            sessionParameters.SetStopChrono(true);
            Debug.Log("TEMPO FINALE " + sessionParameters.GetChrono());
            TextMeshProUGUI text = canvasPlayerCamera.GetComponentInChildren<TextMeshProUGUI>();
            if (text.name == "Level Completed Text")
            {
                text.fontSize = 150;
            }
            StartCoroutine(ShowTextInterval(canvasCameraSearch, "Area Completed Text", 10));

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
            dolphin.GetComponent<Animation>().PlayQueued("Looping");
            
        }
    }

    private int GetCollectibleChildCount(Transform parent, string _tag)
    {
        int collectibleChildCounter = 0;
        for (int i = 0; i < parent.childCount; i++)
        {
            Transform child = parent.GetChild(i);
            if (child.tag == _tag)
            {
                collectibleChildCounter++;
            }
        }
        return collectibleChildCounter;
    }


    private IEnumerator BubbleMachine(){
        MagicRoomAppliancesManager.instance.sendChangeCommand("Macchina delle Bolle", "ON");
        yield return new WaitForSeconds(5f);
        MagicRoomAppliancesManager.instance.sendChangeCommand("Macchina delle Bolle", "OFF");
    }

    private IEnumerator ShowTextInterval(Canvas canvas, String textName, int seconds)
    {
        TextMeshProUGUI text = canvas.GetComponentInChildren<TextMeshProUGUI>();

        if (text.name == "Area Completed Text")
        {
            text.fontSize = 150;
            yield return new WaitForSeconds(seconds);
            text.fontSize = 0;

        }
    }
}
 