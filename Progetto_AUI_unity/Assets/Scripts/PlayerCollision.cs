using UnityEngine;
using System;
using System.Collections;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using System.Runtime.Remoting.Lifetime;
using UnityEngine.Networking;
using System.Text;

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

    public bool touchedRight = false; 

    public bool touchedLeft = false; 

    public bool touchedDown = false; 

    public bool touchedUp = false;

    public float max_time;

    public SessionParametersRun param;

    public RunDataSerializable dataSerializable = new RunDataSerializable();

    public bool tutorial=false;

   
    public bool clock_stop = false;
    Vector3 accelerometer;
    public double angle_x = 0;
    public double angle_y = 0;

    public Restarting restarting; 
   
    public AvoidObstacle awayFromMe;

    public Collider colliderActual;

    public GameObject dolphin;

    public bool turnLeft = false;

    public ObstacleRotate obRotate;

    public bool notTutorial;

    public bool overcame=false;
    public bool delfinoFound = false;

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
    SmartToy dolphinController;

    private TextMeshProUGUI timeMesh;




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
            if ((item.name == "Time"))
            {
                timeMesh = item;
                item.fontSize = 75;
                item.text = ((int)Time.timeSinceLevelLoad)/60 + ":" + (((int) Time.timeSinceLevelLoad)%60);
            }
        }




	}

    private IEnumerator ReturnToMenu(){

        dataSerializable.activated_power_up = movement.activated_powerups;
        dataSerializable.min = (int)Time.timeSinceLevelLoad / 60;
        dataSerializable.seconds = ((int)Time.timeSinceLevelLoad) % 60;
        dataSerializable.life_remaining = lifeCount;
        dataSerializable.patient_id = param.levelSet.GetLevelSearch().patient_id;
        dataSerializable.level_completed = "Yes";


        string json = JsonUtility.ToJson(dataSerializable);
        StartCoroutine(SendPost(json, "http://127.0.0.1:5000/save/run"));

        string json2 = JsonUtility.ToJson(param.levelSet);
        StartCoroutine(SendPost(json2, "http://127.0.0.1:5000/unity/save"));
        yield return new WaitForSeconds(4.0f);
        SceneManager.LoadScene("Menu2");
    }

    private IEnumerator SendPost(string json, string url)
    {
        Debug.Log("entro nella coroutine");
        var request = new UnityWebRequest(url, "POST");
        byte[] bodyRaw = Encoding.UTF8.GetBytes(json);
        request.uploadHandler = (UploadHandler)new UploadHandlerRaw(bodyRaw);
        request.downloadHandler = (DownloadHandler)new DownloadHandlerBuffer();
        request.SetRequestHeader("Content-Type", "application/json");
        yield return request.SendWebRequest();

        Debug.Log(request.downloadHandler.text);


        if (request.isHttpError || request.isNetworkError)
        {
            Debug.Log("questo e' l'errore");
        }
    }



	/*private IEnumerator fadecolor() {
        MagicRoomLightManager.instance.sendColour("#000088", 100);
        yield return new WaitForSeconds(1f);
        MagicRoomLightManager.instance.sendColour(Color.blue);
       // MagicRoomTextToSpeachManagerOffline.instance.generateAudioFromText("La riprende vecino", MagicRoomTextToSpeachManagerOffline.instance.listofAssociatedNames[3]);

       
    

       // MagicRoomTextToSpeachManagerOffline.instance.generateAudioFromText("ciao", voice);
    }*/

    private IEnumerator turnLeftAnimation()
    {
        dolphin.GetComponent<Animation>().Play("TurnLeft");
        yield return new WaitForSeconds(1.0f);
        if(!clock_stop){
            movement.start = true;
            movement.enabled = true;
        }else{
            movement.start = false;
            movement.enabled = false;
                
        }



    }

    private IEnumerator turnRightAnimation()
    {
        dolphin.GetComponent<Animation>().Play("TurnRight");
        yield return new WaitForSeconds(1.0f);
        if(!clock_stop){
            movement.start = true;
            movement.enabled = true;

        }else
        {
            movement.start = false;
            movement.enabled = false;

        }
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
        if(!clock_stop){
            movement.start = true;
            movement.enabled = true;

        }else
        {
            movement.start = false;
            movement.enabled = false;

        }

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
                    clock_stop = true;
                    dolphin.GetComponent<Animation>().PlayQueued("UpsideDown");
                    StartCoroutine(deadResetAnimation());

                    dataSerializable.activated_power_up = movement.activated_powerups;
                    dataSerializable.min = (int)Time.timeSinceLevelLoad / 60;
                    dataSerializable.seconds = ((int)Time.timeSinceLevelLoad) % 60;
                    dataSerializable.life_remaining = lifeCount;
                    dataSerializable.patient_id = param.levelSet.GetLevelSearch().patient_id;
                    dataSerializable.level_completed = "No";


                    string json = JsonUtility.ToJson(dataSerializable);
                    StartCoroutine(SendPost(json, "http://127.0.0.1:5000/save/run"));

                    string json2 = JsonUtility.ToJson(param.levelSet);
                    StartCoroutine(SendPost(json2, "http://127.0.0.1:5000/unity/save"));

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
                    StartCoroutine(ReturnToMenu());
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

        if ((Time.timeSinceLevelLoad < max_time + 1) || tutorial)
        {

            timeMesh.text = ((int)Time.timeSinceLevelLoad) / 60 + ":" + (((int)Time.timeSinceLevelLoad) % 60);

        }


        if (Time.timeSinceLevelLoad > max_time && !clock_stop && !tutorial)
        {
            clock_stop = true;
            movement.start = false;
            movement.enabled = false;

            dolphin.GetComponent<Animation>().Play("Stopping");
            dolphin.GetComponent<Animation>().PlayQueued("UpsideDown");
            StartCoroutine(deadResetAnimation());

            dataSerializable.activated_power_up = movement.activated_powerups;
            dataSerializable.min = (int)Time.timeSinceLevelLoad / 60;
            dataSerializable.seconds = ((int)Time.timeSinceLevelLoad) % 60;
            dataSerializable.life_remaining = lifeCount;
            dataSerializable.patient_id = param.levelSet.GetLevelSearch().patient_id;
            dataSerializable.level_completed = "No";

            string json = JsonUtility.ToJson(dataSerializable);
            StartCoroutine(SendPost(json, "http://127.0.0.1:5000/save/run"));

            string json2 = JsonUtility.ToJson(param.levelSet);
            StartCoroutine(SendPost(json2, "http://127.0.0.1:5000/unity/save"));

            restarting.enabled = true;


        }
        
        if (!delfinoFound)
        {
            if (GameObject.Find("Dolphin1") != null)
            {
                
                dolphinController = GameObject.Find("Dolphin1").GetComponent<SmartToy>();
               
                // StartCoroutine(waittoStartGreenLight());*/
                //dolphinController.executeCommandLightController(Color.green, 100, "parthead");
               
                delfinoFound = true;
            }
        } //Input.GetKeyDown(KeyCode.LeftArrow)

        if(dolphinController!=null){


            accelerometer = dolphinController.objectposition.accelerometer[0];
            angle_x = (Mathf.Atan2(accelerometer.y, accelerometer.z) * 180.0f) / Mathf.PI;
            angle_y = -(Mathf.Atan2(accelerometer.x, Mathf.Sqrt(accelerometer.y * accelerometer.y + accelerometer.z * accelerometer.z)) * 180.0f) / Mathf.PI;

            touchedDown = movement.angle_x > 24.0f;
            touchedUp = movement.angle_x < -20.0f;
            touchedLeft = dolphinController.touchsensor.touchpoints[2].touched;
            touchedRight = dolphinController.touchsensor.touchpoints[1].touched; 

        }else {
            touchedLeft = Input.GetKeyDown(KeyCode.LeftArrow);
            touchedRight = Input.GetKeyDown(KeyCode.RightArrow);
            touchedDown = Input.GetKeyDown(KeyCode.DownArrow);
            touchedUp = Input.GetKeyDown(KeyCode.UpArrow); 
        }

        if (touchedLeft && isTriggerLeft == true)    //check if the corner trigger (Left) is active and wait for the input by the user
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



        if (touchedRight && isTriggerRight == true)
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

        if (touchedRight && isTriggerMiddle == true && !touchedLeft){

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


        if (touchedLeft && isTriggerMiddle == true && !touchedRight){

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


        if (touchedDown && isTriggerObstDown == true)
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


        if (touchedUp && isTriggerObstUp == true)
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
