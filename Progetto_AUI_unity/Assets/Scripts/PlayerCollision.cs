using UnityEngine;
using System;
using System.Collections;
using UnityEngine.UI;

public class PlayerCollision : MonoBehaviour
{

    public PlayerMovement movement;

    public RotationsSlow rotate; 

    public bool isTriggerLeft = false;      //Boolean --> bool

    public bool isTriggerRight = false;

    public bool isTriggerObstDown = false;

    public bool isTriggerObstUp = false; 
   
    public AvoidObstacle awayFromMe;

    public Collider colliderActual;

    public GameObject dolphin;

    public bool turnLeft = false;

    public ObstacleRotate obRotate;

    public SlerpUp slU;
    //inizio nuove aggiunte
    public SlerpDown slD;
    public bool GuiOn;
    public string Text = "";
    public Rect BoxSize = new Rect(0, 0, 200, 100);
    public GUISkin customSkin;






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
        dolphin.GetComponent<Animation>().Play("Stopping");
        //dolphin.GetComponent<Animation>().Stop("Swimming");
        //dolphin.GetComponent<Animation>().PlayQueued("Idle"); 
        


        this.colliderActual = colliderActual; 


        

        switch (colliderActual.tag)
        {
            case "TurningPoint Left":
                {
                    Text = "Turn Left";
                    GuiOn = true;
                    isTriggerLeft = true;

                    movement.enabled = false;

                    GetComponent<Rigidbody>().velocity = Vector3.zero;

                    colliderActual.enabled = false; 
                    

                    break;
                }

            case "TurningPoint Right":
                {
                    Text = "Turn Right";
                    GuiOn = true;
                    isTriggerRight = true;
         
                    movement.enabled = false;

                    GetComponent<Rigidbody>().velocity = Vector3.zero;

                    colliderActual.enabled = false; 

                    break;
                }

            case "Obstacle Down":
                {
                    Text = "Go Down To Down The Obstacle";
                    GuiOn = true;
                    Debug.Log("ho colliso OBUP");
                    Debug.Log(dolphin.transform.position);
                    isTriggerObstDown = true;
                    movement.enabled = false;
                    GetComponent<Rigidbody>().velocity = Vector3.zero;
                    slD.SetPlayerPosition(dolphin.transform.position);
                    colliderActual.enabled = false;
                    Debug.Log(slD.initalPlayerPosition);

                    break;
                }

            case "Obstacle Up":
                {
                    Text = "Go Up To Avoid The Obstacle";
                    GuiOn = true;
                    Debug.Log("ho colliso OBUP");
                    //Debug.Log(dolphin.transform.position);
                    isTriggerObstUp = true;
                    movement.enabled = false;
                    GetComponent<Rigidbody>().velocity = Vector3.zero;
                    slU.SetPlayerPosition(dolphin.transform.position);
                    colliderActual.enabled = false;
                    //Debug.Log(sl.initalPlayerPosition);
                    


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
            GuiOn = false;
            rotate.setUpRotation(new Vector3(0,-90 ,0));
            //movement.start = true;
            //movement.enabled = true;
            //dolphin.GetComponent<Animation>().Play("TurnLeft");
            StartCoroutine(turnLeftAnimation());








            isTriggerLeft = false;
        }



       if (Input.GetKeyDown(KeyCode.RightArrow) && isTriggerRight == true)
        {

            GuiOn = false;
            rotate.setUpRotation(new Vector3(0,90 ,0));
            isTriggerRight = false;
        }



        if (Input.GetKeyDown(KeyCode.DownArrow) && isTriggerObstDown == true)
        {
            GuiOn = false;
            //rotate.setUpRotation(new Vector3(40 ,0,0));
            //awayFromMe.setUpAvoiding(-transform.up, colliderActual); 
            slD.enabled = true;
            dolphin.GetComponent<Animation>().Play("IdleAndOvercomeDown");
            dolphin.GetComponent<Animation>().Stop("Idle");
            isTriggerObstDown = false;


        }


        if (Input.GetKeyDown(KeyCode.UpArrow) && isTriggerObstUp == true)
        {

            GuiOn = false;          
            slU.enabled = true;
            dolphin.GetComponent<Animation>().Play("IdleAndOvercome");
            dolphin.GetComponent<Animation>().Stop("Idle");
            Debug.Log("sl abilitato");
            //rotate.setUpRotation(new Vector3(-40, 0,0));
           // colliderActual.enabled = false; 
            //obRotate.setPointCollision(colliderActual, Vector3.up);

            //awayFromMe.setUpAvoiding(transform.up, colliderActual);
           // movement.start = true;
            


            isTriggerObstUp = false;


        }




    }



    void OnGUI()
    {

        if (customSkin != null)
        {
            GUI.skin = customSkin;
        }

        if (GuiOn == true)
        {
            // Make a group on the center of the screen
            GUI.BeginGroup(new Rect((Screen.width - BoxSize.width) / 2, (Screen.height - BoxSize.height) / 2, BoxSize.width, BoxSize.height));
            // All rectangles are now adjusted to the group. (0,0) is the topleft corner of the group.

            GUI.Label(BoxSize, Text);

            // End the group we started above. This is very important to remember!
            GUI.EndGroup();

        }


    }








}
