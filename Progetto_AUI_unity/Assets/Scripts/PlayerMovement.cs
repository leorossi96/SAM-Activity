using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerMovement : MonoBehaviour {

    public Rigidbody rb;

    public float forwardForce = 2000f;      //2000f valore vero
    public float movementForce = 500f;
    public float speed; 
    public bool rightArrow;
    public bool leftArrow;
    public bool downArrow;
    public bool upArrow;
    public GameObject dolphin;
    public bool start = false;
    public bool isIdle = false;
    public PowerUpManagerLocal manager;
    public float multiplier; 
    public bool indestructible = false;
    public int activated_powerups = 0;
    public PlayerCollision collision;

    public bool delfinoFound = false;
    SmartToy dolphinController;
    Vector3 accelerometer;
    public double angle_x = 0;
    public double angle_y = 0;




    private IEnumerator AnimationSet()
    {
        dolphin.GetComponent<Animation>().Play("New Animation");
        yield return new WaitForSeconds(3.5f);
        start = true;
    }

    private void Awake()
	{
        for (int i = 1; i < Display.displays.Length; i++)
        {
            Display.displays[i].Activate();
        }
        multiplier = 1.0f; 
	}

    void Start()
    {
        StartCoroutine(AnimationSet());
        if (!delfinoFound)
        {
            if (GameObject.Find("Dolphin1") != null)
            {
                MagicRoomSmartToyManager.instance.openEventChannelSmartToy("Dolphin1");
                MagicRoomSmartToyManager.instance.openStreamSmartToy("Dolphin1", 10f);
                dolphinController = GameObject.Find("Dolphin1").GetComponent<SmartToy>();
                accelerometer = dolphinController.objectposition.accelerometer[0];
                Debug.Log("ACCELEROMETRO " + accelerometer);
                // StartCoroutine(waittoStartGreenLight());*/
                //dolphinController.executeCommandLightController(Color.green, 100, "parthead");
                Debug.Log("Light On.");
                delfinoFound = true;
            }
        }
    }
  

	// Update is called once per frame
	void FixedUpdate () {

        if (!delfinoFound)
        {
            if (GameObject.Find("Dolphin1") != null)
            {
                //UDPListenerForMagiKRoom.instance.StartReceiver(10);
                MagicRoomSmartToyManager.instance.openEventChannelSmartToy("Dolphin1");
                MagicRoomSmartToyManager.instance.openStreamSmartToy("Dolphin1", 10f);
                dolphinController = GameObject.Find("Dolphin1").GetComponent<SmartToy>();
                accelerometer = dolphinController.objectposition.accelerometer[0];
                // StartCoroutine(waittoStartGreenLight());*/
                //dolphinController.executeCommandLightController(Color.green, 100, "parthead");
                Debug.Log("Light On.");
                delfinoFound = true;
            }
        }
        if (dolphinController != null)
        {

            accelerometer = dolphinController.objectposition.accelerometer[0];
            angle_x = (Mathf.Atan2(accelerometer.y, accelerometer.z) * 180.0f) / Mathf.PI;
            angle_y = -(Mathf.Atan2(accelerometer.x, Mathf.Sqrt(accelerometer.y * accelerometer.y + accelerometer.z * accelerometer.z)) * 180.0f) / Mathf.PI;
            Debug.Log("ANGLE_X: " + angle_x + "ANGLE_Y: "+ angle_y);

            rightArrow = angle_y < -24.0f;
            leftArrow = angle_y > 15.0f;
            downArrow = angle_x > 24.0f;
            upArrow = angle_x < -20.0f;
        }else {
            rightArrow = Input.GetKey(KeyCode.RightArrow);
            leftArrow = Input.GetKey(KeyCode.LeftArrow);
            downArrow = Input.GetKey(KeyCode.DownArrow);
            upArrow = Input.GetKey(KeyCode.UpArrow);
        }
        //rb.AddRelativeForce(Vector3.forward);
        //rb.AddRelativeForce(0, 0, forwardForce * Time.deltaTime);
        if (!(collision.clock_stop))    
            transform.position = Vector3.MoveTowards(transform.position, transform.position + transform.forward, speed*multiplier); 
        //Debug.Log("Swimming");
        //Debug.Log(forwardForce * Time.deltaTime);
        if (start)
        {
            dolphin.GetComponent<Animation>().Play("Swimming");
        }


        // If use keyboard (ex. as in Unity or Standalone built)
        /*rightArrow = Input.GetKey(KeyCode.RightArrow);
        leftArrow = Input.GetKey(KeyCode.LeftArrow);
        downArrow = Input.GetKey(KeyCode.DownArrow);
        upArrow = Input.GetKey(KeyCode.UpArrow);*/



        //int x, y, z;                        //three axis acceleration data
        //double roll = 0.00, pitch = 0.00;       //Roll & Pitch are the angles which rotate by the axis X and y


        if(!(collision.clock_stop)){

            if (rightArrow)
            {
                // rb.AddRelativeForce(movementForce * Time.deltaTime, 0, 0, ForceMode.VelocityChange);
                //rb.AddRelativeForce(Vector3.right,  ForceMode.VelocityChange);
                transform.position = Vector3.MoveTowards(transform.position, transform.position + transform.right, speed);
            }
            if (leftArrow)
            {
                //rb.AddRelativeForce(-movementForce * Time.deltaTime, 0, 0, ForceMode.VelocityChange);
                transform.position = Vector3.MoveTowards(transform.position, transform.position - transform.right, speed);
            }
            if (downArrow)
            {
                //rb.AddRelativeForce(0, -movementForce * Time.deltaTime, 0, ForceMode.VelocityChange);
                transform.position = Vector3.MoveTowards(transform.position, transform.position - transform.up, speed);
            }
            if (upArrow)
            {
                transform.position = Vector3.MoveTowards(transform.position, transform.position + transform.up, speed);
            }
        }

        if (transform.position.y > 60f) //Dolphin Upper Bound
        {
            transform.position = new Vector3(transform.position.x, 60f, transform.position.z);
        }
        if (transform.position.y < 6f) //Dolphin Lower Bound
        {
            transform.position = new Vector3(transform.position.x, 6f, transform.position.z);
        }


        /*       if ((rb.position.y <= -1 || rb.position.y.ToString().Equals("NaN")) && false)
                {
                    FindObjectOfType<GameManager>().EndGame();
               }
               */




    }

    public void powerUp(string chest_name){

        switch (chest_name)
        {
            case "chest_food1":
                activated_powerups++;
                manager.powerUp("power_up_speed");
                if(dolphinController!=null)
                    dolphinController.executeCommandLightController(Color.yellow, 100, "parthead");
                StartCoroutine(powerDown("power_up_speed")); 
                multiplier = 1.3f;
                break; 

            case "chest_food2":
                activated_powerups++;
                manager.powerUp("power_up_ind");
                if (dolphinController != null)
                    dolphinController.executeCommandLightController(Color.green, 100, "parthead");
                StartCoroutine(powerDown("power_up_ind")); 

                indestructible = true; 
                break;

            case "hit":
                manager.powerUp("power_up_ind");
                if (dolphinController != null)
                    dolphinController.executeCommandLightController(Color.green, 100, "parthead");
                StartCoroutine(powerDownAfterHit("power_up_ind"));
                indestructible = true;
                break;

            default:
                break;
        }


    }

    private IEnumerator powerDown(string power)
    {
        yield return new WaitForSeconds(20.0f);

        if(power.Equals("power_up_ind")){
            indestructible = false;
             
        }

        if (power.Equals("power_up_speed"))
        {
            multiplier = 1.0f;
        }

        manager.powerDown(power);
        if (dolphinController != null)
            dolphinController.executeCommandLightController(Color.black, 0, "parthead");

        
    }

    private IEnumerator powerDownAfterHit(string power){

        yield return new WaitForSeconds(3.0f);

        if (power.Equals("power_up_ind"))
        {
            indestructible = false;

        }

        manager.powerDown(power);
        if (dolphinController != null)
            dolphinController.executeCommandLightController(Color.black, 0, "parthead");

    }



}
