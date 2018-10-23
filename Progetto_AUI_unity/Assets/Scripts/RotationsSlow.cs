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
        transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.Euler(rotateDirection), 100.0f*Time.deltaTime);
        if (transform.rotation.Equals(Quaternion.Euler(rotateDirection)) || transform.rotation == Quaternion.Euler(rotateDirection))
        {
            this.enabled = false;
            movement.enabled = true;
            print("Cacca");
            // Quaternion prova = GetComponent<Transform>().rotation;
            //  Vector3 cacca = Vector3.RotateTowards(new Vector3(prova.x, prova.y, prova.z), rotateDirection, 1.0f, 0.0f);


        }



        /* if (test(transform.rotation.eulerAngles, rotateDirection))
         {
             this.enabled = false;
             movement.enabled = true;
             print("Cacca");
             // Quaternion prova = GetComponent<Transform>().rotation;
             //  Vector3 cacca = Vector3.RotateTowards(new Vector3(prova.x, prova.y, prova.z), rotateDirection, 1.0f, 0.0f)
         }
         else
         {
             transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.Euler(rotateDirection), 180.0f * Time.deltaTime);
         }




     }


     private bool test(Vector3 uno, Vector3 due)
     {
         const float kEpsilon = 1E-05F;

         if (Mathf.Abs(Mathf.Abs(uno.x - due.x) % (Mathf.PI)) > kEpsilon)
         {
             return false;
         }
         if (Mathf.Abs(Mathf.Abs(uno.y - due.y) % (Mathf.PI)) > kEpsilon)
         {
             return false;
         }
         if (Mathf.Abs(Mathf.Abs(uno.z - due.z) % (Mathf.PI)) > kEpsilon)
         {
             return false;
         }



         return true;
     }*/

    }

    //Activate the component and set up the rotate direction
    public void setUpRotation(Vector3 direction)
    {
        
        this.rotateDirection = direction;
        this.enabled = true;
      //  Quaternion prova = GetComponent<Transform>().rotation;
      //  Vector3 cacca = Vector3.RotateTowards(new Vector3(prova.x, prova.y, prova.z), rotateDirection, 1.0f, 0.0f);
        print("finita rotazione e mov");
    }

}