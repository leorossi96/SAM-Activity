using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AssemblyCSharp;

public class AvoidObstacle : MonoBehaviour {



    private Collider colliderActual;
    private Quaternion originalRot;
    private Vector3 axis; 

	// Use this for initialization
	void Start () {
        this.enabled = false; 
	}
	
	// Update is called once per frame
	void Update () {

        transform.RotateAround(colliderActual.gameObject.transform.position, axis, 2.0f);
        transform.rotation = originalRot; 

	}

	



	public void setUpAvoiding(Vector3 axis, Collider colliderActual){

        this.axis = axis; 
        this.originalRot = transform.rotation; 
        this.colliderActual = colliderActual; 
        this.enabled = true; 
    }
}
