using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BugCapturing : MonoBehaviour {



	
	public void OnTriggerExit(Collider other)
	{
        this.GetComponent<BoxCollider>().isTrigger = false; 

	}
}
