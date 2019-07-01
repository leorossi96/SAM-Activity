using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VelocityConstant : MonoBehaviour {

    public int velocity; 

	// Use this for initialization
	void Start () {
        
	}
	
	// Update is called once per frame
	void Update () {
        this.GetComponent<Rigidbody>().velocity = this.transform.forward*velocity; 
	}
}
