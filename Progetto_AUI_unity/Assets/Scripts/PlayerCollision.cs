using UnityEngine;
using System;

public class PlayerCollision : MonoBehaviour
{

    public PlayerMovement movement;

    public Boolean isTriggerLeft = false;

    public Boolean isTriggerRight = false;


    private void OnTriggerEnter(Collider collider)
    {

        if (collider.tag == "TurningPoint Left")
        {
            isTriggerLeft = true;
            movement.enabled = false;

            /*Vector3 savedVelocity = GetComponent<Rigidbody>().velocity; 
*/

            GetComponent<Rigidbody>().velocity= Vector3.zero;
                         
                  //asking input to the user
            /*transform.Rotate(new Vector3(0, -90, 0));


            //reset the velocity
            movement.enabled = true; */
            
                    //invece di mettere variabile con riferimento da associare anche nell'inspector
            //uso find object così quando cambio personaggio non perdo il riferimento a Game Manager
            //FindObjectOfType<GameManager>().EndGame();
        }


       else if(collider.tag == "TurningPoint Right")
        {
            isTriggerRight = true;

            movement.enabled = false;

            GetComponent<Rigidbody>().velocity = Vector3.zero;
        }


    }



    void Update()
    {
        if(Input.GetKeyDown(KeyCode.LeftArrow) && isTriggerLeft == true)
        {
            transform.Rotate(new Vector3(0, -90, 0));

            movement.enabled = true;

            isTriggerLeft = false;
        }


       else if (Input.GetKeyDown(KeyCode.RightArrow) && isTriggerRight == true)
        {
            transform.Rotate(new Vector3(0, 90, 0));

            movement.enabled = true;

            isTriggerRight = false;
        }



    }


}
