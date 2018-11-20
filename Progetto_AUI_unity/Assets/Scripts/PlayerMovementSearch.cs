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

    public bool moving;

    public bool delfinoFound = false;


    // Update is called once per frame
    void FixedUpdate()
    {
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
                position = position - tf.up  * velocityApplied * Time.deltaTime;
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
                /*moving = false;
                StartCoroutine(MoveFromTo(position, position + tf.forward, lerpSpeed));*/
            }
            if (shiftKey)
            {
                position = position - tf.forward * velocityApplied * Time.deltaTime;
                tf.position = position;
            }
        }
        if (!delfinoFound)
        {
            if (GameObject.Find("Dolphin1") != null)
            {
                MagicRoomSmartToyManager.instance.openEventChannelSmartToy("Dolphin1");
                MagicRoomSmartToyManager.instance.openStreamSmartToy("Dolphin1", 10f);
                delfinoFound = true;
            }
        }
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
