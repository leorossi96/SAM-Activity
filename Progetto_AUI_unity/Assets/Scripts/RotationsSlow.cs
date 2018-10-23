using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotationsSlow : MonoBehaviour
{

    public PlayerMovement movement;
    private Quaternion rotateDirection;

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

        transform.rotation = Quaternion.RotateTowards(transform.rotation, rotateDirection, 1.0f);
        if (transform.rotation == rotateDirection)
        {
            print("finished rotation"); 
            this.enabled = false;
            movement.enabled = true;

        }
    }

    //Activate the component and set up the rotate direction
    public void setUpRotation(Vector3 direction)
    {
        
        this.rotateDirection = transform.rotation * Quaternion.Euler(direction);
        print(transform.localRotation.eulerAngles);
        this.enabled = true;
        //print(this.rotateDirection.eulerAngles); 


    }

}