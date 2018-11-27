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
    private Vector3 oldPosition; 


	// Use this for initialization
	void Start () {
        this.enabled = false; 
	}
	
	// Update is called once per frame
	void Update () {

        //this.playerChar.transform.localRotation = Quaternion.RotateTowards(this.playerChar.transform.localRotation, this.playerChar.transform.localRotation*rotSequence[i], 7.0f);



        transform.position = Vector3.MoveTowards(transform.position, posSequence[i], Time.deltaTime*10) ;

        for (int j = 0; j < posSequence.Length - 1; j++)
            posSequence[j] = Vector3.Lerp(posSequence[j], posSequence[j + 1], Time.deltaTime* (float)1.5);
        
             


        camera.transform.localPosition = Vector3.Lerp(camera.transform.localPosition, camSeq[i], Time.deltaTime*2);
        for (int j = 0; j < camSeq.Length - 1; j++)
            camSeq[j] = Vector3.Lerp(camSeq[j], camSeq[j + 1], Time.deltaTime*2);

        Quaternion targetRotation= Quaternion.FromToRotation(this.playerChar.transform.forward, this.playerChar.transform.position - oldPosition);
        this.playerChar.transform.localRotation = Quaternion.RotateTowards(this.playerChar.transform.localRotation, targetRotation, (float)0.1); 
        oldPosition = this.playerChar.transform.position;


        if(Vector3.Distance(transform.position, posSequence[posSequence.Length - 1])<=0.075){

            transform.position = posSequence[i+1];
            this.enabled = false;
            this.movement.enabled = true; 
        }

	}

    /*public void changePhase(){

        i++; 
        if(i>posSequence.Length-1){
            playerChar.transform.localRotation = Quaternion.AngleAxis(0, Vector3.right); 
            this.enabled = false;
            this.movement.enabled = true; 
        }
         
    }*/

	

	public void setUpAvoiding(Vector3 axis, Collider colliderActual){
        
        this.i = 0;
        this.posSequence = new Vector3[3];
        this.rotSequence = new Quaternion[3];
        this.camSeq = new Vector3[3];
        this.ax = transform.right;
        this.oldPosition = this.playerChar.transform.position; 

        this.posSequence[0] = (colliderActual.ClosestPointOnBounds(colliderActual.gameObject.transform.position) + axis * 6);
        this.posSequence[1] = (this.posSequence[0] + transform.forward * ((colliderActual.gameObject.transform.localScale.z) + Math.Abs(camera.transform.localPosition.z)));
        this.posSequence[2] = (this.posSequence[1] + Vector3.Reflect(this.posSequence[0] - transform.position, -axis));


        /*this.rotSequence[0] = new Quaternion(); 
        this.rotSequence[0] = Quaternion.AngleAxis(45, ax);
        this.rotSequence[1] = new Quaternion(); 
        this.rotSequence[1] = Quaternion.AngleAxis(0, ax); 
        this.rotSequence[2] = new Quaternion(); 
        this.rotSequence[2] = Quaternion.AngleAxis(-45, ax);*/
         


        this.camSeq[0] = new Vector3(camera.transform.localPosition.x, playerChar.transform.localPosition.y, camera.transform.localPosition.z);
        this.camSeq[1] = new Vector3(camera.transform.localPosition.x, playerChar.transform.localPosition.y, camera.transform.localPosition.z);
        this.camSeq[2] = camera.transform.localPosition; 
        this.playerChar.GetComponent<Animation>().Play("Up"); 
        this.enabled = true;



    }
}
