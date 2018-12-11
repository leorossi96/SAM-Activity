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
     
    public bool rightArrow;
    public bool leftArrow;
    public bool downArrow;
    public bool upArrow;
    public bool tabKey;
    public bool shiftKey;

    public GameObject dolphin;  
    public bool start = false;


    SmartToy dolphinController;

    public bool moving;

    public bool delfinoFound = false;

    private void Awake()
    {
        for (int i = 1; i < Display.displays.Length; i++)
        {
            Display.displays[i].Activate();
        }

       
    }



        // Update is called once per frame
    void FixedUpdate(){

        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;

        // If use keyboard (ex. as in Unity or Standalone built)
        rightArrow = Input.GetKey(KeyCode.RightArrow);
        leftArrow = Input.GetKey(KeyCode.LeftArrow);
        downArrow = Input.GetKey(KeyCode.DownArrow);
        upArrow = Input.GetKey(KeyCode.UpArrow);
        tabKey = Input.GetKey(KeyCode.Tab);
        shiftKey = Input.GetKey(KeyCode.LeftShift);


        if (Input.anyKey)
        {
            Vector3 position = tf.position;

            if (rightArrow)
            {
                tf.Rotate(0, rotationSpeed * Time.deltaTime, 0);
            }
            if (leftArrow)
            {
                tf.Rotate(0, -rotationSpeed * Time.deltaTime, 0); //Usa quaternioni
            }
            if (downArrow)
            {
                position = position - tf.up * velocityApplied * Time.deltaTime;
                tf.position = position;
            }
            if (upArrow)
            {
                position = position + tf.up * velocityApplied * Time.deltaTime;
                tf.position = position;
            }
            if (tabKey)
            {
                position = position + tf.forward * velocityApplied * Time.deltaTime;
                tf.position = position;
                if (start)
                {
                    dolphin.GetComponent<Animation>().PlayQueued("Swimming");
                }
                else{

                    dolphin.GetComponent<Animation>().Play("StartSwimSearch");
                    start = true;
                }



                    
            }
            if (shiftKey)
            {
                position = position - tf.forward * velocityApplied * Time.deltaTime;
                tf.position = position;
            }
        }
        else
        {
            if (start)
            {
                start = false;
                dolphin.GetComponent<Animation>().Play("Stopping");
            }
            else
            dolphin.GetComponent<Animation>().PlayQueued("Idle");
        }

        MagicRoomLightManager.instance.sendColour(Color.blue);

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
