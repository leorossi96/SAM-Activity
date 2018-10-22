using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotationsSlow : MonoBehaviour
{

    public PlayerMovement movement;
    private Vector3 rotateDirection;

    private void Start()
    {

        //By default is not active, only when we call the 
        //@method setUpRotation is activated
        this.enabled = false;
    }
    // Update is called once per frame
    void Update()
    {

        //Rotate slowly towards the rotateDirection, when the rotation is over the 
        //movement is stopped and this component is deactivated
        transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.Euler(rotateDirection), 2.0f);
        if (transform.rotation == Quaternion.Euler(rotateDirection))
        {
            this.enabled = false;
            movement.enabled = true;

        }
    }

    //Activate the component and set up the rotate direction
    public void setUpRotation(Vector3 direction)
    {
        
        this.rotateDirection = direction;
        this.enabled = true;

    }

}