using UnityEngine;
using System;
using System.Collections;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using System.Runtime.Remoting.Lifetime;

public class PlayerCollision : MonoBehaviour
{

    public PlayerMovement movement;

    public int lifeCount;  

    public RotationsSlow rotate; 

    public bool isTriggerLeft = false;      //Boolean --> bool

    public bool isTriggerRight = false;

    public bool isTriggerObstDown = false;

    public bool isTriggerObstUp = false;

    public bool isTriggerMiddle = false;

    public Restarting restarting; 
   
    public AvoidObstacle awayFromMe;

    public Collider colliderActual;

    public GameObject dolphin;

    public bool turnLeft = false;

    public ObstacleRotate obRotate;

    public bool notTutorial;

    public bool overcame=false; 

    public float fadeAmount; 

    public SlerpUp slU;
    //inizio nuove aggiunte
    public SlerpDown slD;
    public bool GuiOn;
    public string Text = "";
    public Rect BoxSize = new Rect(0, 0, 200, 100);
    public GUISkin customSkin;
    public int refAx;
    public int direction;
    public Canvas canvas;




	public void Start()
	{
        TextMeshProUGUI[] text = canvas.GetComponentsInChildren<TextMeshProUGUI>();
        foreach (var item in text)
        {
            if ((item.name == "LifeCount") )
            {
                item.fontSize = 75;
                item.text = "Lives x" + lifeCount; 
            }
        }
	}



	private IEnumerator fadecolor() {
        MagicRoomLightManager.instance.sendColour("#000088", 100);
        yield return new WaitForSeconds(1f);
        MagicRoomLightManager.instance.sendColour(Color.blue);
       // MagicRoomTextToSpeachManagerOffline.instance.generateAudioFromText("La riprende vecino", MagicRoomTextToSpeachManagerOffline.instance.listofAssociatedNames[3]);

       
    

       // MagicRoomTextToSpeachManagerOffline.instance.generateAudioFromText("ciao", voice);
    }

    private IEnumerator turnLeftAnimation()
    {
        dolphin.GetComponent<Animation>().Play("TurnLeft");
        yield return new WaitForSeconds(1.0f);
        movement.start = true;
        movement.enabled = true;

    }

    private IEnumerator turnRightAnimation()
    {
        dolphin.GetComponent<Animation>().Play("TurnRight");
        yield return new WaitForSeconds(1.0f);
        movement.start = true;
        movement.enabled = true;

    }

    private IEnumerator deadResetAnimation(){
        Image[] images = canvas.GetComponentsInChildren<Image>();
        yield return new WaitForSeconds(1.0f);
        TextMeshProUGUI[] text = canvas.GetComponentsInChildren<TextMeshProUGUI>();
        foreach (var item in text)
        {
            if ((item.name == "Restart") || (item.name=="Restart1"))
            {
                item.fontSize = 150;
            }
        }
            
    }

    private IEnumerator reduceLife()
    {

        lifeCount--; 
        TextMeshProUGUI[] text = canvas.GetComponentsInChildren<TextMeshProUGUI>();
        foreach (var item in text)
        {
            if ((item.name == "LifeCount"))
            {
                item.fontSize = 75;
                item.text = "Lives x" + lifeCount;
            }
        }

        yield return new WaitForSeconds(2.0f);
        movement.powerUp("hit"); 
        movement.start = true;
        movement.enabled = true;

    }





	private void OnCollisionEnter(Collision collision)
	{
        
        if(collision.collider.tag=="Obstacle_Reset" && !overcame)        {

            if(!movement.indestructible){
                movement.start = false;
                movement.enabled = false;
                dolphin.GetComponent<Animation>().Play("Stopping");
                this.GetComponent<Rigidbody>().mass = 2.0f * this.GetComponent<Rigidbody>().mass;
                this.GetComponent<Rigidbody>().drag = 1;
                if (collision.rigidbody != null)
                    this.GetComponent<Rigidbody>().AddForce((collision.collider.transform.position - this.transform.position) * collision.rigidbody.mass, ForceMode.Impulse);
                else
                    this.GetComponent<Rigidbody>().AddForce((collision.collider.transform.position - this.transform.position), ForceMode.Impulse);


                if (lifeCount == 0)
                {
                    dolphin.GetComponent<Animation>().PlayQueued("UpsideDown");
                    StartCoroutine(deadResetAnimation());

                    restarting.enabled = true;
                }
                if (lifeCount > 0)
                {
                    StartCoroutine(reduceLife());

                } 
            }

                 

             





        }else{
            if (collision.collider.tag == "chest_food1")
            {
                movement.start = false;
                movement.enabled = false;
                this.GetComponent<Rigidbody>().velocity = Vector3.zero; 
                dolphin.GetComponent<Animation>().Play("Stopping");
                Image[] images = canvas.GetComponentsInChildren<Image>();
                for (int i = 0; i < images.Length; i++)
                {
                    if (images[i].name == "chest_food1")
                    {
                        images[i].GetComponent<Image>().enabled = true;
                    }
                }

               



            }
        }
	}

	private void OnTriggerEnter(Collider colliderActual)
    {



        //dolphin.GetComponent<Animation>().Stop("Swimming");
        //dolphin.GetComponent<Animation>().PlayQueued("Idle"); 
        



        this.colliderActual = colliderActual; 

        if(this.colliderActual.tag!="NonOvercomeLimit" && this.colliderActual.tag != "ResetOvercome" && this.colliderActual.tag != "bug_protection"){

            movement.start = false;
            dolphin.GetComponent<Animation>().Play("Stopping");
        }
                


        

        switch (colliderActual.tag)
        {

           

            case "TurningPoint Left":
                {
                    //Text = "Turn Left";
                    //GuiOn = true;
                    Image[] images = canvas.GetComponentsInChildren<Image>();
                    for (int i = 0; i < images.Length; i++)
                    {
                        if (images[i].name == "leftFin")
                        {
                            images[i].GetComponent<Image>().enabled = true;
                        }
                    }
                    isTriggerLeft = true;
                    movement.enabled = false;
                    GetComponent<Rigidbody>().velocity = Vector3.zero;
                    colliderActual.enabled = false;                    
                    break;
                }

            case "TurningPoint Right":
                {
                    //Text = "Turn Right";
                    // GuiOn = true;
                    Image[] images = canvas.GetComponentsInChildren<Image>();
                    for (int i = 0; i < images.Length; i++)
                    {
                        if (images[i].name == "rightFin")
                        {
                            images[i].GetComponent<Image>().enabled = true;
                        }
                    }

                    isTriggerRight = true;        
                    movement.enabled = false;
                    GetComponent<Rigidbody>().velocity = Vector3.zero;
                    colliderActual.enabled = false;
                    slD.enabled = false;
                    break;
                }

            case "TurningPoint Middle":
                {
                    isTriggerMiddle = true;
                    movement.enabled = false; 
                    GetComponent<Rigidbody>().velocity = Vector3.zero;
                    colliderActual.enabled = false;
                    slD.enabled = false;
                    break;
                }

            case "Obstacle Down X":
                {
                    //Text = "Go Down To Down The Obstacle";
                    //GuiOn = true;
                    Image[] images = canvas.GetComponentsInChildren<Image>();
                    for (int i = 0; i < images.Length; i++)
                    {
                        if (images[i].name == "DownAdvice")
                        {
                            images[i].GetComponent<Image>().enabled = true;
                        }
                    }
                    isTriggerObstDown = true;
                    movement.enabled = false;
                    refAx = 1;
                    direction = 1;
                    GetComponent<Rigidbody>().velocity = Vector3.zero;
                    slD.SetPlayerPosition(dolphin.transform.position, refAx, direction);
                    colliderActual.enabled = false;                   
                    break;
                }


            case "Obstacle Down mX":
                {
                    //Text = "Go Down To Down The Obstacle";
                    // GuiOn = true;
                    Image[] images = canvas.GetComponentsInChildren<Image>();
                    for (int i = 0; i < images.Length; i++)
                    {
                        if (images[i].name == "DownAdvice")
                        {
                            images[i].GetComponent<Image>().enabled = true;
                        }
                    }
                    isTriggerObstDown = true;
                    movement.enabled = false;
                    refAx = 1;
                    direction = 0;
                    GetComponent<Rigidbody>().velocity = Vector3.zero;
                    slD.SetPlayerPosition(dolphin.transform.position, refAx, direction);
                    Debug.Log(dolphin.transform.position);
                    colliderActual.enabled = false;                    
                    break;
                }

            case "Obstacle Down Z":
                {
                    //Text = "Go Down To Down The Obstacle";
                    //GuiOn = true;        
                    Image[] images = canvas.GetComponentsInChildren<Image>();
                    for (int i = 0; i < images.Length; i++)
                    {
                        if (images[i].name == "DownAdvice")
                        {
                            images[i].GetComponent<Image>().enabled = true;
                        }
                    }
                    isTriggerObstDown = true;
                    movement.enabled = false;
                    refAx = 0;
                    direction = 1;
                    GetComponent<Rigidbody>().velocity = Vector3.zero;
                    slD.SetPlayerPosition(dolphin.transform.position, refAx, direction);
                    colliderActual.enabled = false;                   
                    break;
                }


            case "Obstacle Down mZ":
                {
                    //Text = "Go Down To Down The Obstacle";
                    //GuiOn = true;   
                    Image[] images = canvas.GetComponentsInChildren<Image>();
                    for (int i = 0; i < images.Length; i++)
                    {
                        if (images[i].name == "DownAdvice")
                        {
                            images[i].GetComponent<Image>().enabled = true;
                        }
                    }
                    isTriggerObstDown = true;
                    movement.enabled = false;
                    refAx = 0;
                    direction = 0;
                    GetComponent<Rigidbody>().velocity = Vector3.zero;
                    slD.SetPlayerPosition(dolphin.transform.position, refAx, direction);
                    colliderActual.enabled = false;                  
                    break;
                }

            case "Obstacle Up X":
                {
                    //Text = "Go Up To Avoid The Obstacle";
                    //GuiOn = true;    
                    Image[] images = canvas.GetComponentsInChildren<Image>();
                    for (int i = 0; i < images.Length; i++)
                    {
                        if (images[i].name == "UpAdvice")
                        {
                            images[i].GetComponent<Image>().enabled = true;
                        }
                    }
                    isTriggerObstUp = true;
                    movement.enabled = false;
                    GetComponent<Rigidbody>().velocity = Vector3.zero;
                    refAx = 1;
                    direction = 1;
                    slU.SetPlayerPosition(dolphin.transform.position, refAx, direction);
                    colliderActual.enabled = false;
                    break;
                }


            case "Obstacle Up mX":
                {
                    //Text = "Go Up To Avoid The Obstacle";
                    //GuiOn = true;
                    Image[] images = canvas.GetComponentsInChildren<Image>();
                    for (int i = 0; i < images.Length; i++)
                    {
                        if (images[i].name == "UpAdvice")
                        {
                            images[i].GetComponent<Image>().enabled = true;
                        }
                    }
                    isTriggerObstUp = true;
                    movement.enabled = false;
                    GetComponent<Rigidbody>().velocity = Vector3.zero;
                    refAx = 1;
                    direction = 0;
                    slU.SetPlayerPosition(dolphin.transform.position, refAx, direction);
                    colliderActual.enabled = false;
                    break;
                }

            case "Obstacle Up Z":
                {
                    //Text = "Go Up To Avoid The Obstacle";
                    //GuiOn = true;  
                    Image[] images = canvas.GetComponentsInChildren<Image>();
                    for (int i = 0; i < images.Length; i++)
                    {
                        if (images[i].name == "UpAdvice")
                        {
                            images[i].GetComponent<Image>().enabled = true;
                        }
                    }
                    isTriggerObstUp = true;
                    movement.enabled = false;
                    GetComponent<Rigidbody>().velocity = Vector3.zero;
                    refAx = 0;
                    direction = 1;
                    //Debug.Log("Posizione iniziale");
                    //Debug.Log(dolphin.transform.position);
                    slU.SetPlayerPosition(dolphin.transform.position, refAx, direction);
                    colliderActual.enabled = false;                   
                    break;
                }


            case "Obstacle Up mZ":
                {
                    //Text = "Go Up To Avoid The Obstacle";
                    //GuiOn = true;
                    Image[] images = canvas.GetComponentsInChildren<Image>();
                    for (int i = 0; i < images.Length; i++)
                    {
                        if (images[i].name == "UpAdvice")
                        {
                            images[i].GetComponent<Image>().enabled = true;
                        }
                    }
                    isTriggerObstUp = true;
                    movement.enabled = false;
                    GetComponent<Rigidbody>().velocity = Vector3.zero;
                    refAx = 0;
                    direction = 0;
                    slU.SetPlayerPosition(dolphin.transform.position, refAx, direction);
                    colliderActual.enabled = false;
                    break;
                }

            case "Finish":
                {
                    //Text = "Level Finish";
                    //GuiOn = true;
                    TextMeshProUGUI text = canvas.GetComponentInChildren<TextMeshProUGUI>();
                    if (text.name == "Finish Level")
                    {
                        text.fontSize = 150;
                    }
                    movement.enabled = false;
                    GetComponent<Rigidbody>().velocity = Vector3.zero;
                    dolphin.GetComponent<Animation>().Play("DolphinWaitingForSearchStart");
                    dolphin.GetComponent<Animation>().PlayQueued("Clapping");
                    colliderActual.enabled = false;
                    break;
                }

            /*case "Obstacle_Reset":
                {
                    movement.enabled = false;
                    GetComponent<Rigidbody>().velocity = Vector3.zero;

                    StartCoroutine(deadResetAnimation()); 


                    break; 
                }*/
            case "NonOvercomeLimit":
                {
                    this.overcame = true;
                    break;
                }

                case "ResetOvercome":
                {
                    this.overcame = false;
                    break;
                }
            

            default: break;
                
               

        }
                 
    }

	private void OnTriggerExit(Collider other)
	{
        if(other.CompareTag("ResetOvercome"))
        {
            this.overcame = false;

        }
	}






	void Update()
    {
        if(Input.GetKeyDown(KeyCode.LeftArrow) && isTriggerLeft == true)    //check if the corner trigger (Left) is active and wait for the input by the user
        {
            //GuiOn = false;
            Image[] images = canvas.GetComponentsInChildren<Image>();
            for (int i = 0; i < images.Length; i++)
            {
                if (images[i].name == "leftFin")
                {
                    images[i].GetComponent<Image>().enabled = false;
                }
            }
            rotate.setUpRotation(new Vector3(0,-90 , 0));
            //movement.start = true;
            //movement.enabled = true;
            //dolphin.GetComponent<Animation>().Play("TurnLeft");
            StartCoroutine(turnLeftAnimation());
            isTriggerLeft = false;
        }



       if (Input.GetKeyDown(KeyCode.RightArrow) && isTriggerRight == true)
        {
            //GuiOn = false;
            Image[] images = canvas.GetComponentsInChildren<Image>();
            for (int i = 0; i < images.Length; i++)
            {
                if (images[i].name == "rightFin")
                {
                    images[i].GetComponent<Image>().enabled = false;
                }
            }
            rotate.setUpRotation(new Vector3(0,90 ,0));
            StartCoroutine(turnRightAnimation());
            isTriggerRight = false;
        }

        if (Input.GetKeyDown(KeyCode.RightArrow) && isTriggerMiddle == true && !Input.GetKeyDown(KeyCode.LeftArrow)){

            BoxCollider[] sons = colliderActual.gameObject.GetComponentsInChildren<BoxCollider>(true);
            for (int i = 0; i < sons.Length; i++){
                if(sons[i].gameObject.name.Equals(colliderActual.gameObject.name + "_right")){
                    sons[i].enabled = true; 
                }
            }
            rotate.setUpRotation(new Vector3(0, 90, 0));
            StartCoroutine(turnRightAnimation());
            isTriggerMiddle = false;
        }


        if (Input.GetKeyDown(KeyCode.LeftArrow) && isTriggerMiddle == true && !Input.GetKeyDown(KeyCode.RightArrow)){

            BoxCollider[] sons = colliderActual.gameObject.GetComponentsInChildren<BoxCollider>(true);
            for (int i = 0; i < sons.Length; i++)
            {
                if (sons[i].gameObject.name.Equals(colliderActual.gameObject.name + "_left"))
                {
                    sons[i].enabled = true;
                }
            }
            rotate.setUpRotation(new Vector3(0, -90, 0));
            StartCoroutine(turnLeftAnimation());
            isTriggerMiddle = false;
        }


        if (Input.GetKeyDown(KeyCode.DownArrow) && isTriggerObstDown == true)
        {
            //GuiOn = false;
            //rotate.setUpRotation(new Vector3(40 ,0,0));
            //awayFromMe.setUpAvoiding(-transform.up, colliderActual); 
            Image[] images = canvas.GetComponentsInChildren<Image>();
            for (int i = 0; i < images.Length; i++)
            {
                if (images[i].name == "DownAdvice")
                {
                    images[i].GetComponent<Image>().enabled = false;
                }
            }
            slD.enabled = true;
            dolphin.GetComponent<Animation>().Play("IdleAndOvercomeDown");
            dolphin.GetComponent<Animation>().Stop("Idle");
            isTriggerObstDown = false;


        }


        if (Input.GetKeyDown(KeyCode.UpArrow) && isTriggerObstUp == true)
        {

            //GuiOn = false;    
            Image[] images = canvas.GetComponentsInChildren<Image>();
            for (int i = 0; i < images.Length; i++)
            {
                if (images[i].name == "UpAdvice")
                {
                    images[i].GetComponent<Image>().enabled = false;
                }
            }
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
