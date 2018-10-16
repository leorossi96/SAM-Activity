using UnityEngine;
using System;

public class PlayerCollision : MonoBehaviour
{

    public PlayerMovement movement;
     

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.tag == "Turning Point")
        {

            movement.enabled = false;

            Vector3 savedVelocity = GetComponent<Rigidbody>().velocity; 


            GetComponent<Rigidbody>().velocity= Vector3.zero;


            //asking input to the user
            transform.Rotate(new Vector3(0, -90, 0));


            //reset the velocity
            movement.enabled = true; 



            //invece di mettere variabile con riferimento da associare anche nell'inspector
            //uso find object così quando cambio personaggio non perdo il riferimento a Game Manager
            //FindObjectOfType<GameManager>().EndGame();
        }
    }
}
