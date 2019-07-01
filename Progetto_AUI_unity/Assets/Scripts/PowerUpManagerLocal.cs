using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpManagerLocal : MonoBehaviour {

    private Dictionary<string, ParticleSystem> powerUps;
    private ParticleSystem active;
    

	// Use this for initialization
	void Start () {
        powerUps = new Dictionary<string, ParticleSystem>(); 

        foreach (var item in this.GetComponentsInChildren<ParticleSystem>())
        {
            item.enableEmission = false; 
            powerUps.Add(item.gameObject.name, item);


        }
	}
	
    public void powerUp(string power){
        powerUps[power].enableEmission=true;
        active = powerUps[power]; 
    }

    public void powerDown(){
        active.enableEmission = false; 
    }
}
