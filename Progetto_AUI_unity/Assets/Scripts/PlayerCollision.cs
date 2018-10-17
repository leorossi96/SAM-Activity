using UnityEngine;
using System;
using UnityStandardAssets.Utility;
using System.Net;
using System.Security.Cryptography;

public class PlayerCollision : MonoBehaviour
{

    public PlayerMovement movement;
    public RotationsSlow rotate; 

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.tag == "Turning Point")
        {

            movement.enabled = false;

            /*Vector3 savedVelocity = GetComponent<Rigidbody>().velocity; 
*/

            GetComponent<Rigidbody>().velocity= Vector3.zero;

            //asking input to the user
            rotate.setUpRotation(new Vector3(0+transform.rotation.eulerAngles.x, 
                                             -90+transform.rotation.eulerAngles.y,
                                             0+transform.rotation.eulerAngles.z));  





             

            //invece di mettere variabile con riferimento da associare anche nell'inspector
            //uso find object così quando cambio personaggio non perdo il riferimento a Game Manager
            //FindObjectOfType<GameManager>().EndGame();
        }


    }



	






}
