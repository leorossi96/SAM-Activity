﻿using UnityEngine;
using System;
using System.Collections;

public class VoicesOffline : MonoBehaviour
{

    public PlayerMovement movement;

    public RotationsSlow rotate; 


    public Boolean isTriggerLeft = false;

    public Boolean isTriggerRight = false;

    public Boolean isTriggerObstDown = false;

    public Boolean isTriggerObstUp = false;

    public Voices voice = new Voices();

    private IEnumerator fadecolor() {
        MagicRoomLightManager.instance.sendColour("#000088", 100);
        yield return new WaitForSeconds(1f);
        MagicRoomLightManager.instance.sendColour(Color.blue);

    }

    private void OnTriggerEnter(Collider collider)
    {
        StartCoroutine(fadecolor());

        
       
    


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

            case "Obstacle Down":
                {
                    print("ho colliso");

                    isTriggerObstDown = true;

                    movement.enabled = false;

                    GetComponent<Rigidbody>().velocity = Vector3.zero;

                    break;
                }

            case "Obstacle Up":
                {
                    print("ho colliso");

                    isTriggerObstUp = true;

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
            case "Obstacle Down":
                {
                    print("sto uscendo");

                    movement.enabled = false;

                    GetComponent<Rigidbody>().velocity = Vector3.zero;

                    rotate.setUpRotation(new Vector3(-40 ,
                                          0,
                                          0));
                    
                    break;
                }

            case "Obstacle Up":
                {
                    print("sto uscendo");

                    movement.enabled = false;

                    GetComponent<Rigidbody>().velocity = Vector3.zero;

                    rotate.setUpRotation(new Vector3(40 ,
                                          0,
                                          0 ));

                    break;
                }

            default: break;
        }
    }

        


   



    void Update()
    {
        if(Input.GetKeyDown(KeyCode.LeftArrow) && isTriggerLeft == true)    //check if the corner trigger (Left) is active and wait for the input by the user
        {

            rotate.setUpRotation(new Vector3(0,
                                             -90 ,
                                             0));





            isTriggerLeft = false;
        }



       if (Input.GetKeyDown(KeyCode.RightArrow) && isTriggerRight == true)
        {




            rotate.setUpRotation(new Vector3(0,
                                             90 ,
                                             0));




            isTriggerRight = false;
        }



        if (Input.GetKeyDown(KeyCode.DownArrow) && isTriggerObstDown == true)
        {
            rotate.setUpRotation(new Vector3(40 ,
                                            0,
                                            0));


            isTriggerObstDown = false;


        }


        if (Input.GetKeyDown(KeyCode.UpArrow) && isTriggerObstUp == true)
        {
            print("inizio rotazione"); 
            rotate.setUpRotation(new Vector3(-40,
                                            0,
                                            0));


            isTriggerObstUp = false;


        }




    }



	






}
