using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.CompilerServices;

public class RotationsSlow : MonoBehaviour
{

    public PlayerMovement movement;
   
    private Quaternion actual;

   


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
        //transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(rotateDirection), 0.1f); 


        transform.rotation = Quaternion.RotateTowards(transform.rotation, actual, 4.0f); 
         
        if(transform.rotation==actual){
            this.enabled = false;
            this.movement.enabled = true; 
        }


    }

    //Activate the component and set up the rotate direction
    public void setUpRotation(Vector3 direction)
    {



        this.actual = Quaternion.Euler(direction); 

        this.enabled = true; 
    }

}