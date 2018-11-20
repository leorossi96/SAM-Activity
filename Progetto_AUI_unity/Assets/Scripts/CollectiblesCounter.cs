﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectiblesCounter : MonoBehaviour {

    public int counter;
    public int nCollectibles;
    SmartToy Dolphin;

    public void Start()
    {
        nCollectibles = GameObject.FindGameObjectsWithTag("Collectible").Length;
        Debug.Log(nCollectibles);
    }

    public void CollectibleFound()
    {
        counter++;
        Debug.Log(counter);
        if(Dolphin == null){
            Dolphin = GameObject.Find("Dolphin1").GetComponent<SmartToy>();
        }
            Dolphin.executeCommandLightController(Color.black, 0, "parthead");
            Debug.Log("LUCE DELFINO ACCESA");

        //MagicRoomSmartToyManager.instance.sendCommandExecuteSmartToy("Dolphin1", "partrightfin");

        VoicesOffline voice = MagicRoomTextToSpeachManagerOffline.instance.listofAssociatedNames[2];
        while (counter < nCollectibles)
        {
            MagicRoomTextToSpeachManagerOffline.instance.generateAudioFromText("Hai trovato un collezionabile! Cerca gli altri nella mappa", voice);
        }
        if (counter == nCollectibles)
        {
            MagicRoomLightManager.instance.sendColour(Color.red);
            MagicRoomTextToSpeachManagerOffline.instance.generateAudioFromText("Complimenti, hai vinto!", voice);
            Debug.Log("HAI VINTO");
            StartCoroutine(BubbleMachine());
        }
    }

    private IEnumerator BubbleMachine(){
        MagicRoomAppliancesManager.instance.sendChangeCommand("Macchina delle Bolle", "ON");
        yield return new WaitForSeconds(2f);
        MagicRoomAppliancesManager.instance.sendChangeCommand("Macchina delle Bolle", "OFF");
    }
}
