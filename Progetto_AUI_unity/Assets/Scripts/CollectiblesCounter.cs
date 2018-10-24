using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectiblesCounter : MonoBehaviour {

    public int counter;
    public int nCollectibles;

    public void Start()
    {
        nCollectibles = GameObject.FindGameObjectsWithTag("Collectible").Length;
        Debug.Log(nCollectibles);
    }

    public void CollectibleFound(){
        counter++;
        Debug.Log(counter);
        if(counter == nCollectibles){
            MagicRoomLightManager.instance.sendColour(Color.red);
            /*Voices aoao = new Voices();
            MagicRoomTextToSpeechManagerOnline.instance.generateAudioFromText("HAI VINTO!", aoao, "");*/
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
