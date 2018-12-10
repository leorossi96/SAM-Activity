using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerMovement : MonoBehaviour {

    public Rigidbody rb;

    public float forwardForce = 2000f;      //2000f valore vero
    public float movementForce = 500f;

    public bool rightArrow;
    public bool leftArrow;
    public bool downArrow;
    public bool upArrow;
    public GameObject dolphin;
    public bool start = false;
    public bool isIdle = false;
    


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
	void FixedUpdate () {


        //rb.AddRelativeForce(Vector3.forward);
        rb.AddRelativeForce(0, 0, forwardForce * Time.deltaTime);
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
                rb.AddRelativeForce(Vector3.right * Time.deltaTime,  ForceMode.VelocityChange);
            }
            if (leftArrow)
            {
                //rb.AddRelativeForce(-movementForce * Time.deltaTime, 0, 0, ForceMode.VelocityChange);
                rb.AddRelativeForce(-Vector3.right , ForceMode.VelocityChange);
            }
            if (downArrow)
            {
                //rb.AddRelativeForce(0, -movementForce * Time.deltaTime, 0, ForceMode.VelocityChange);
                rb.AddRelativeForce(-Vector3.up, ForceMode.VelocityChange);
            }
            if (upArrow)
            {
                rb.AddRelativeForce(Vector3.up , ForceMode.VelocityChange);
            }
        }
/*       if ((rb.position.y <= -1 || rb.position.y.ToString().Equals("NaN")) && false)
        {
            FindObjectOfType<GameManager>().EndGame();
       }
       */




        

    }
}
