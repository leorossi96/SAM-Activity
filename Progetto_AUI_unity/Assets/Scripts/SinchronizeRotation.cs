using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SinchronizeRotation : MonoBehaviour {

    public GameObject obj1;
    public GameObject obj2; 

	// Use this for initialization
	void Start () {
        this.enabled = false; 
	}
	
	// Update is called once per frame
	void Update () {

        obj1.transform.rotation = obj1.transform.rotation * obj2.transform.localRotation; 
	}
}
