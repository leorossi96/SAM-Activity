using UnityEngine;
using System;
using UnityStandardAssets.Utility;
using System.Net;
using System.Security.Cryptography;

public class PlayerCollision : MonoBehaviour
{

    public PlayerMovement movement;

    public RotationsSlow rotate; 


    public Boolean isTriggerLeft = false;

    public Boolean isTriggerRight = false;

    public Boolean isTriggerObst = false;



    private void OnTriggerEnter(Collider collider)
    {

        switch (collider.tag)
        {
            case "TurningPoint Left":
                {
                    isTriggerLeft = true;

                    movement.enabled = false;

                    GetComponent<Rigidbody>().velocity = Vector3.zero;

                    break;
                }

            case "TurningPoint Right":
                {
                    isTriggerRight = true;
         
                    movement.enabled = false;

                    GetComponent<Rigidbody>().velocity = Vector3.zero;

                    break;
                }

            case "Obstacle":
                {
                    print("ho colliso");

                    isTriggerObst = true;

                    movement.enabled = false;

                    GetComponent<Rigidbody>().velocity = Vector3.zero;

                    break;
                }

            default: break;

               

        }
                 
    }



    void OnTriggerExit(Collider collider)
    {
        switch (collider.tag)
        {
            case "Obstacle":
                {
                    print("sto uscendo");

                    movement.enabled = false;

                    GetComponent<Rigidbody>().velocity = Vector3.zero;

                    rotate.setUpRotation(new Vector3(-40 + transform.rotation.eulerAngles.x,
                                          0 + transform.rotation.eulerAngles.y,
                                          0 + transform.rotation.eulerAngles.z));
                    
                    break;
                }

            default: break;
        }
    }

        


   



    void Update()
    {
        if(Input.GetKeyDown(KeyCode.LeftArrow) && isTriggerLeft == true)    //check if the corner trigger (Left) is active and wait for the input by the user
        {
            rotate.setUpRotation(new Vector3(0 + transform.rotation.eulerAngles.x,
                                             -90 + transform.rotation.eulerAngles.y,
                                             0 + transform.rotation.eulerAngles.z));





            isTriggerLeft = false;
        }



       else if (Input.GetKeyDown(KeyCode.RightArrow) && isTriggerRight == true)
        {




            rotate.setUpRotation(new Vector3(0 + transform.rotation.eulerAngles.x,
                                             90 + transform.rotation.eulerAngles.y,
                                             0 + transform.rotation.eulerAngles.z));




            isTriggerRight = false;
        }



        else if (Input.GetKeyDown(KeyCode.DownArrow) && isTriggerObst == true)
        {
            rotate.setUpRotation(new Vector3(40 + transform.rotation.eulerAngles.x,
                                            0 + transform.rotation.eulerAngles.y,
                                            0 + transform.rotation.eulerAngles.z));


            isTriggerObst = false;


        }




    }



	






}
