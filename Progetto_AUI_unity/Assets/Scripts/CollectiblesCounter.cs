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
            Debug.Log("HAI VINTO");
        }
    }
}
