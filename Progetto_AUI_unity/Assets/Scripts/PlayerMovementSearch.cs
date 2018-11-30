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
    }

    void Start()
    {
        StartCoroutine(AnimationSet());
    }




        // Update is called once per frame
        void FixedUpdate()
    {
        if (start)
        {
            dolphin.GetComponent<Animation>().Play("Swimming");
        }

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
                    dolphin.GetComponent<Animation>().Play("Swimming");
                }
            }
            if (shiftKey)
            {
                position = position - tf.forward * velocityApplied * Time.deltaTime;
                tf.position = position;
            }
        }
        else
            dolphin.GetComponent<Animation>().Play("Idle");

        MagicRoomLightManager.instance.sendColour(Color.black);

        if (!delfinoFound)
        {
            if (GameObject.Find("Dolphin1") != null)
            {
                MagicRoomSmartToyManager.instance.openEventChannelSmartToy("Dolphin1");
                MagicRoomSmartToyManager.instance.openStreamSmartToy("Dolphin1", 10f);
                dolphinController = GameObject.Find("Dolphin1").GetComponent<SmartToy>();
                //dolphinController.objectposition.gyroscope();
                StartCoroutine(waittoStartGreenLight());
                delfinoFound = true;
            }
        }
    }

    IEnumerator waittoStartGreenLight(){
        yield return new WaitForSeconds(1);
        dolphinController.executeCommandLightController(Color.green, 0, "parthead");
    }




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
