using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementSearch : MonoBehaviour
{

    public Rigidbody rb;
    public Transform tf;

    public float velocityApplied = 10f;
    public float rotationSpeed = 3f;
    public float lerpSpeed = 0.5f;

    public bool turnRight;
    public bool turnLeft;
    public bool goDown;
    public bool goUp;
    public bool goForward;
    public bool goBackward;
    public bool startUp;
    public bool startDown;

    public GameObject dolphin;
    public bool start = false;
    public bool stop = false;




    public bool delfinoFound = false;
    SmartToy dolphinController;
    Vector3 accelerometer;
    public double angle_x = 0;
    public double angle_y = 0;

    public bool moving;

    

    private void Awake()
    {
        for (int i = 1; i < Display.displays.Length; i++)
        {
            Display.displays[i].Activate();
        }
    }



    // Update is called once per frame
    void FixedUpdate()
    {
        if (!delfinoFound)
        {
            if (GameObject.Find("Dolphin1") != null)
            {
                UDPListenerForMagiKRoom.instance.StartReceiver(10);
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
            Debug.Log("ANGLE_X: " + angle_x + "ANGLE_Y: " + angle_y);


        }

        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;

        // If use keyboard (ex. as in Unity or Standalone built)
        turnRight = angle_y < -24.0f;
        turnLeft = angle_y > 15.0f;
        goDown = angle_x > 24.0f;
        goUp = angle_x < -40.0f;
        goForward = dolphinController.touchsensor.touchpoints[1].touched && dolphinController.touchsensor.touchpoints[2].touched;
        /*turnRight = Input.GetKey(KeyCode.RightArrow);
        turnLeft = Input.GetKey(KeyCode.LeftArrow);
        goDown = Input.GetKey(KeyCode.DownArrow);
        goUp = Input.GetKey(KeyCode.UpArrow);
        goForward = Input.GetKey(KeyCode.Tab);
        goBackward = Input.GetKey(KeyCode.LeftShift);*/
        //goBackward = Input.GetKey(KeyCode.LeftShift);



            Vector3 position = tf.position;

            if (turnRight)
            {
                tf.Rotate(0, rotationSpeed * Time.deltaTime, 0);
            }
            if (turnLeft)
            {
                tf.Rotate(0, -rotationSpeed * Time.deltaTime, 0); //Usa quaternioni
            }
            if (goDown)
            {
                position = position - tf.up * velocityApplied * Time.deltaTime;
                tf.position = position;
                if (start)
                {
                    dolphin.GetComponent<Animation>().PlayQueued("Swimming");
                }
                else
                {
                    dolphin.GetComponent<Animation>().Play("StartSwimSearch");
                    start = true;
                }
            stop = true;

                /*if(!start){
                    if(!startDown){
                        dolphin.GetComponent<Animation>().Play("StartSwimSearch");
                        startDown = true; 
                    }else{
                        dolphin.GetComponent<Animation>().PlayQueued("Swimming");
                    }
                }*/
            }
            if (goUp)
            {
                position = position + tf.up * velocityApplied * Time.deltaTime;
                tf.position = position;
                if (start == true)
                {
                    dolphin.GetComponent<Animation>().PlayQueued("Swimming");
                }
                else
                {
                    dolphin.GetComponent<Animation>().Play("StartSwimSearch");
                    start = true;
                }
            stop = true;


            /*if(!tabKey){
                if(!startUp){
                    dolphin.GetComponent<Animation>().Play("GoUp");
                    startUp = true; 
                }else{
                    dolphin.GetComponent<Animation>().PlayQueued("Swimming");
                }
            }*/
        }
        if (goForward)
            {
                position = position + tf.forward * velocityApplied * Time.deltaTime;
                tf.position = position;
                if (start)
                {
                    dolphin.GetComponent<Animation>().PlayQueued("Swimming");
                }
                else
                {
                    dolphin.GetComponent<Animation>().Play("StartSwimSearch");
                    start = true;
                }
            stop = true;

        }
        if (goBackward)
            {
                position = position - tf.forward * velocityApplied * Time.deltaTime;
                tf.position = position;
                if (start)
                {
                    dolphin.GetComponent<Animation>().PlayQueued("Swimming");
                }
                else
                {
                    dolphin.GetComponent<Animation>().Play("StartSwimSearch");
                    start = true;
                }
            stop = true;
            
        }

        if (!goUp && !goDown && !goForward && !goBackward)
        {
            start = false;
            dolphin.GetComponent<Animation>().PlayQueued("Idle");
            if(stop){
                dolphin.GetComponent<Animation>().Play("Stopping");
                stop = false;
            }

            //MagicRoomLightManager.instance.sendColour(Colo.blue);
        }


            

        



            /*if (Input.GetKeyUp(KeyCode.UpArrow))
            {
                startUp = false;
                dolphin.GetComponent<Animation>().Play("Stopping_from_going_up");
                dolphin.GetComponent<Animation>().PlayQueued("Idle");
            }*/


            /*if (Input.GetKeyUp(KeyCode.DownArrow))
            {
                startUp = false;
                dolphin.GetComponent<Animation>().Play("stopping_from_going_down");
                dolphin.GetComponent<Animation>().PlayQueued("Idle");

            }*/



        



        

        if (!delfinoFound)
        {
            if (GameObject.Find("Dolphin1") != null)
            {
                MagicRoomSmartToyManager.instance.openEventChannelSmartToy("Dolphin1");
                MagicRoomSmartToyManager.instance.openStreamSmartToy("Dolphin1", 10f);
                dolphinController = GameObject.Find("Dolphin1").GetComponent<SmartToy>();
                /*Vector3[] gyroscope = dolphinController.objectposition.gyroscope;
                Debug.Log("GIROSCOPIO " + gyroscope[0]);
                StartCoroutine(waittoStartGreenLight());*/
                dolphinController.executeCommandLightController(Color.green, 100, "parthead");
                Debug.Log("Light On.");
                delfinoFound = true;
            }
        }


        if (tf.position.y > 60f)
        {
            tf.position = new Vector3(tf.position.x, 60f, tf.position.z);
        }
        if(tf.position.y < 6f){
            tf.position = new Vector3(tf.position.x, 6f, tf.position.z);
        }

    }




    /*IEnumerator waittoStartGreenLight(){
        yield return new WaitForSeconds(1);
        dolphinController.executeCommandLightController(Color.green, 0, "parthead");
    }*/




    /*private IEnumerator MoveFromTo(Vector3 start, Vector3 end, float time)
    {
        if (!moving)
        {               // Do nothing if already moving
            moving = true;           // Set flag to true
            float t = 0f;
            while (t < 1.0f)
            {
                t += Time.deltaTime / time; // Sweeps from 0 to 1 in time seconds
                transform.position = Vector3.Lerp(start, end, t); // Set position proportional to t
                yield return 0;      // Leave the routine and return here in the next frame
            }
            moving = false;        // Finished moving
        }
    }*/

}
