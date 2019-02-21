using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.CompilerServices;

using System;

public class RandomVerticalMovement : MonoBehaviour {


    public float distance;
    public Vector3 targetPoint;
    public bool goodModel;
    public Vector3 originalPos; 



	// Use this for initialization
	void Start () {

        distance = UnityEngine.Random.RandomRange(5.0f, 6.0f);

        if(goodModel)
            targetPoint = distance * this.transform.up + transform.localPosition;
        else targetPoint = -distance * this.transform.up + transform.localPosition;

        originalPos = transform.localPosition; 


            
	}
	
	// Update is called once per frame
	void Update () {


        transform.localPosition = Vector3.MoveTowards(transform.localPosition, targetPoint, 0.2f);
        if (Vector3.Distance(transform.localPosition, targetPoint )<0.01){
            //Quaternion euler makes a rotation with the respect of the axis of the object, so cannot use it since every
            //little fish has a different orientation of the axis 
            this.transform.rotation = this.transform.rotation * Quaternion.Euler(this.transform.right*180); 
            if (goodModel)
                targetPoint = distance * this.transform.up + originalPos;
            else targetPoint = -distance * this.transform.up + originalPos;
        } 
	}
}
