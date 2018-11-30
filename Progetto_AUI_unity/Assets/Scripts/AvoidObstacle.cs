using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AssemblyCSharp;
using System;

public class AvoidObstacle : MonoBehaviour {


    public PlayerMovement movement;
    /*private Collider colliderActual;
    private Vector3 axis;
    private bool rectProgressing; 
    private Vector3 originalPosition;*/
    public GameObject playerChar; 
    public GameObject camera;
    //private Vector3 targetVector;
    //private bool readyToRestart;
    //private Vector3 targetPos;
    private Vector3[] camSeq; 
    private Vector3[] posSequence;
    private Quaternion[] rotSequence; 
    private int i;
    public Vector3 ax; 


	// Use this for initialization
	void Start () {
        this.enabled = false; 
	}
	
	// Update is called once per frame
	void Update () {

        this.playerChar.transform.localRotation = Quaternion.RotateTowards(this.playerChar.transform.localRotation, this.playerChar.transform.localRotation*rotSequence[i], 7.0f);

        transform.position = Vector3.Lerp(transform.position, posSequence[i], Time.deltaTime);

        camera.transform.localPosition = Vector3.Lerp(camera.transform.localPosition, camSeq[i], Time.deltaTime*2);

        if(Vector3.Distance(transform.position, posSequence[i])<=0.1){

            transform.position = posSequence[i];
            changePhase(); 
        }

	}

    public void changePhase(){

        i++; 
        if(i>posSequence.Length-1){
            playerChar.transform.localRotation = Quaternion.AngleAxis(0, Vector3.right); 
            this.enabled = false;
            this.movement.enabled = true; 
        }
         
    }

	

	public void setUpAvoiding(Vector3 axis, Collider colliderActual){
        
        this.i = 0;
        this.posSequence = new Vector3[3];
        this.rotSequence = new Quaternion[3];
        this.camSeq = new Vector3[3];
        this.ax = transform.right; 

        this.posSequence[0] = (colliderActual.ClosestPointOnBounds(colliderActual.transform.position) + axis * colliderActual.transform.localScale.);
        this.posSequence[1] = (this.posSequence[0] + transform.forward * (colliderActual.gameObject.transform.localScale.z*2 + Math.Abs(camera.transform.localPosition.z)));
        this.posSequence[2] = (this.posSequence[1] + Vector3.Reflect(this.posSequence[0] - transform.position, -axis));





        this.camSeq[0] = new Vector3(camera.transform.localPosition.x, playerChar.transform.localPosition.y, camera.transform.localPosition.z);
        this.camSeq[1] = new Vector3(camera.transform.localPosition.x, playerChar.transform.localPosition.y, camera.transform.localPosition.z);
        this.camSeq[2] = camera.transform.localPosition; 

        //this.playerChar.GetComponent<Animation>().Play("Up"); 
        this.enabled = true;



    }
}
