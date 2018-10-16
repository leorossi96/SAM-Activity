using UnityEngine;

public class PlayerMovement : MonoBehaviour {

    public Rigidbody rb;

    public float forwardForce = 2000f;
    public float movementForce = 500f;

    public bool rightArrow;
    public bool leftArrow;
    public bool downArrow;
    public bool upArrow;


    // Update is called once per frame
    void FixedUpdate () {

        

        rb.AddRelativeForce(0, 0, forwardForce * Time.deltaTime);



        // If use keyboard (ex. as in Unity or Standalone built)
        rightArrow = Input.GetKey(KeyCode.RightArrow);
        leftArrow = Input.GetKey(KeyCode.LeftArrow);
        downArrow = Input.GetKey(KeyCode.DownArrow);
        upArrow = Input.GetKey(KeyCode.UpArrow);


        if (Input.anyKey)
        {
            if (rightArrow)
            {
                rb.AddRelativeForce(movementForce * Time.deltaTime, 0, 0, ForceMode.VelocityChange);
            }
            if (leftArrow)
            {
                rb.AddRelativeForce(-movementForce * Time.deltaTime, 0, 0, ForceMode.VelocityChange);
            }
            if (downArrow)
            {
                rb.AddRelativeForce(0, -movementForce * Time.deltaTime, 0, ForceMode.VelocityChange);
            }
            if (upArrow)
            {
                rb.AddRelativeForce(0, movementForce * Time.deltaTime, 0, ForceMode.VelocityChange);
            }
        }
/*       if ((rb.position.y <= -1 || rb.position.y.ToString().Equals("NaN")) && false)
        {
            FindObjectOfType<GameManager>().EndGame();
       }
       */

        

    }
}
