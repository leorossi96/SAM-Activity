using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bounding : MonoBehaviour {

    public Collider originator;
    private Collider thisCollider;
    public Collider nextCurve; 

	// Use this for initialization
	void Start () {
        thisCollider = this.GetComponent<BoxCollider>(); 
        //this.enabled = false; 
	}

	public void Update()
	{
        if(originator.enabled==false){
            thisCollider.isTrigger = false;
            nextCurve.enabled = false; 
            this.enabled = false; 
        }
	}

}
