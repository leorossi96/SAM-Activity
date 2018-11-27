using UnityEngine;
using System;
using System.Collections;

public class PlayerCollision : MonoBehaviour
{

    public PlayerMovement movement;

    public RotationsSlow rotate; 

    public Boolean isTriggerLeft = false;

    public Boolean isTriggerRight = false;

    public Boolean isTriggerObstDown = false;

    public Boolean isTriggerObstUp = false; 
   
    public AvoidObstacle awayFromMe;

    public Collider colliderActual;

    public GameObject dolphin;

    public bool turnLeft = false;

    
    

    private IEnumerator fadecolor() {
        MagicRoomLightManager.instance.sendColour("#000088", 100);
        yield return new WaitForSeconds(1f);
        MagicRoomLightManager.instance.sendColour(Color.blue);
       // MagicRoomTextToSpeachManagerOffline.instance.generateAudioFromText("La riprende vecino", MagicRoomTextToSpeachManagerOffline.instance.listofAssociatedNames[3]);

        print("ciao");
    

       // MagicRoomTextToSpeachManagerOffline.instance.generateAudioFromText("ciao", voice);
    }

    private IEnumerator turnLeftAnimation()
    {
        dolphin.GetComponent<Animation>().Play("TurnLeft");
        yield return new WaitForSeconds(1.0f);
        movement.start = true;
        movement.enabled = true;

    }






    private void OnTriggerEnter(Collider colliderActual)
    {


        movement.start = false;
        dolphin.GetComponent<Animation>().Stop("Swimming");
       
    


        this.colliderActual = colliderActual; 


        
        switch (colliderActual.tag)
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
                    

                    isTriggerObstDown = true;

                    movement.enabled = false;



                    GetComponent<Rigidbody>().velocity = Vector3.zero;

                    break;
                }

            case "Obstacle Up":
                {
                    

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
        /*switch (collider.tag)
        {
            case "Obstacle Down":
                {
                    print("sto uscendo");

                    movement.enabled = false;

                    GetComponent<Rigidbody>().velocity = Vector3.zero;

                    rotate.setUpRotation(new Vector3(-40,0,0));
                    
                    break;


                }

            case "Obstacle Up":
                {
                    print("sto uscendo");

                    movement.enabled = false;

                    GetComponent<Rigidbody>().velocity = Vector3.zero;

                    rotate.setUpRotation(new Vector3(40,0,0));

                    break;


                }

            default: break;
        }
        */



    }

        


   



    void Update()
    {
        if(Input.GetKeyDown(KeyCode.LeftArrow) && isTriggerLeft == true)    //check if the corner trigger (Left) is active and wait for the input by the user
        {

            //rotate.setUpRotation(new Vector3(0,-90 ,0));
            
            //dolphin.GetComponent<Animation>().Play("TurnLeft");
            StartCoroutine(turnLeftAnimation());
            
                
            
            
            
          


            isTriggerLeft = false;
        }



       if (Input.GetKeyDown(KeyCode.RightArrow) && isTriggerRight == true)
        {




            rotate.setUpRotation(new Vector3(0,90 ,0));




            isTriggerRight = false;
        }



        if (Input.GetKeyDown(KeyCode.DownArrow) && isTriggerObstDown == true)
        {
            //rotate.setUpRotation(new Vector3(40 ,0,0));

            awayFromMe.setUpAvoiding(-transform.up, colliderActual); 

            isTriggerObstDown = false;


        }


        if (Input.GetKeyDown(KeyCode.UpArrow) && isTriggerObstUp == true)
        {
            
            //rotate.setUpRotation(new Vector3(-40, 0,0));

            awayFromMe.setUpAvoiding(transform.up, colliderActual);
            movement.start = true;


            isTriggerObstUp = false;


        }




    }

	










}
