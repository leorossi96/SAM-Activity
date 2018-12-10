using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleRotate : MonoBehaviour {

    public GameObject dolphin;
    private Vector3 pointCollision;
    private Quaternion actualRotation;
    private Vector3 finalPos;

    private bool upped; 
    

	// Use this for initialization
	void Start () {
        this.enabled = false;
	}
	
	// Update is called once per frame
	void Update () {
        if (!upped)
        {
            dolphin.GetComponent<Animation>().Play("Up"); 
            this.upped = true; 
        }
        this.actualRotation = transform.rotation;
        transform.RotateAround(this.pointCollision, dolphin.transform.right, 20 * Time.deltaTime);

        transform.rotation = this.actualRotation;
        

        if(Vector3.Distance(transform.position, finalPos) <= 0.05)
        {
            transform.position = finalPos; 
        }
	}


    public void setPointCollision(Collider actual, Vector3 direction)
    {

        this.upped = false; 
        this.pointCollision = actual.transform.position;
        this.finalPos = this.transform.position + Vector3.forward * actual.transform.localScale.z; 
        this.enabled = true;
    }
}
