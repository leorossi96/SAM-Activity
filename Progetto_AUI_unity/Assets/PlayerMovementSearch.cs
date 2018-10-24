using UnityEngine;

public class PlayerMovementSearch : MonoBehaviour
{

    public Rigidbody rb;
    public Transform tf;

    public float movementForce = 500f;
    public float velocityApplied = 10f;
    public float viscosityResistence = 250f;
    public float rotationSpeed = 3f;
     
    public bool rightArrow;
    public bool leftArrow;
    public bool downArrow;
    public bool upArrow;
    public bool tabKey;
    public bool shiftKey;


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
            Vector3 vect = tf.position;

            if (rightArrow)
            {
                transform.Rotate(0, rotationSpeed * Time.deltaTime, 0);
            }
            if (leftArrow)
            {
                transform.Rotate(0, -rotationSpeed * Time.deltaTime, 0); //Usa quaternioni
            }
            if (downArrow)
            {
                vect = vect - tf.up  * velocityApplied * Time.deltaTime;
                tf.position = vect;
            }
            if (upArrow)
            {
                vect = vect + tf.up * velocityApplied * Time.deltaTime;
                tf.position = vect;
            }
            if (tabKey)
            {
                vect = vect + tf.forward * velocityApplied * Time.deltaTime;
                tf.position = vect;
            }
            if (shiftKey)
            {
                vect = vect - tf.forward * velocityApplied * Time.deltaTime;
                tf.position = vect;
            }
        }
    }
}
