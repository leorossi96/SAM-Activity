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


    public bool delfinoFound = false;
    SmartToy dolphinController;




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
    }
  

	// Update is called once per frame
	void FixedUpdate () {


        //rb.AddRelativeForce(Vector3.forward);
        //rb.AddRelativeForce(0, 0, forwardForce * Time.deltaTime);
        transform.position = Vector3.MoveTowards(transform.position, transform.position + transform.forward, speed*multiplier); 
        //Debug.Log("Swimming");
        //Debug.Log(forwardForce * Time.deltaTime);
        if (start)
        {
            dolphin.GetComponent<Animation>().Play("Swimming");
        }

        
        // If use keyboard (ex. as in Unity or Standalone built)
        rightArrow = Input.GetKey(KeyCode.RightArrow);
        leftArrow = Input.GetKey(KeyCode.LeftArrow);
        downArrow = Input.GetKey(KeyCode.DownArrow);
        upArrow = Input.GetKey(KeyCode.UpArrow);


        if (Input.anyKey)
        {
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
        /*       if ((rb.position.y <= -1 || rb.position.y.ToString().Equals("NaN")) && false)
                {
                    FindObjectOfType<GameManager>().EndGame();
               }
               */

        if (!delfinoFound)
        {
            if (GameObject.Find("Dolphin1") != null)
            {
                MagicRoomSmartToyManager.instance.openEventChannelSmartToy("Dolphin1");
                MagicRoomSmartToyManager.instance.openStreamSmartToy("Dolphin1", 10f);
                dolphinController = GameObject.Find("Dolphin1").GetComponent<SmartToy>();
                Vector3[] gyroscope = dolphinController.objectposition.gyroscope;
                Debug.Log("GIROSCOPIO " + gyroscope[0]);
                // StartCoroutine(waittoStartGreenLight());*/
                //dolphinController.executeCommandLightController(Color.green, 100, "parthead");
                Debug.Log("Light On.");
                delfinoFound = true;
            }
        }




    }

    public void powerUp(string chest_name){
        switch (chest_name)
        {
            case "chest_food1":
                manager.powerUp("power_up_speed");
                StartCoroutine(powerDown("power_up_speed")); 
                multiplier = 2.0f;
                break; 

            case "chest_food2":
                manager.powerUp("power_up_ind");
                StartCoroutine(powerDown("power_up_ind")); 
                indestructible = true; 
                break;

            case "hit":
                manager.powerUp("power_up_ind");
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

        
    }

    private IEnumerator powerDownAfterHit(string power){

        yield return new WaitForSeconds(3.0f);

        if (power.Equals("power_up_ind"))
        {
            indestructible = false;

        }

        manager.powerDown(power);

    }


}
