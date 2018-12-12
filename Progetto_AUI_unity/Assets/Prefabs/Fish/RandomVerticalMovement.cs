using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.CompilerServices;

using System;

public class RandomVerticalMovement : MonoBehaviour {


    public float distance;
    public float range;
    public float rangeinit; 
    public bool run; 


	// Use this for initialization
	void Start () {
        if (run)
            distance = UnityEngine.Random.RandomRange(5.0f, 6.0f);
        else {
            distance = UnityEngine.Random.RandomRange(0.5f, 1.0f);
        }
            
	}
	
	// Update is called once per frame
	void Update () {


        transform.localPosition = Vector3.MoveTowards(transform.localPosition, new Vector3(transform.localPosition.x, distance, transform.localPosition.z), 0.2f);
        if (Math.Abs(transform.localPosition.y - distance)<0.01){
            distance = -distance; 
        } 
	}
}
