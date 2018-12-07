using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlerpDown : MonoBehaviour
{

    public Vector3 initalPlayerPosition;


    public GameObject dolphin;
    float timeCounter = 0;
    float speed = 18f;
    float height;
    float width = 50;
    public PlayerMovement movement;
    int count = 0;
    




    void Start()
    {
        this.enabled = false;
    }


    // Update is called once per frame
    void Update()
    {

        if (dolphin.transform.position.y <= initalPlayerPosition.y)
        {
            timeCounter += Time.deltaTime * (speed);

            float x = initalPlayerPosition.x;
            float y = -Mathf.Sin(Mathf.PI * (dolphin.transform.position.z - (initalPlayerPosition.z - 2)) / 80) * height;
            //Debug.Log(Mathf.Sin(Mathf.PI * (dolphin.transform.position.z - (initalPlayerPosition.z - 2)) / 80) * height);
            float z = timeCounter;
            // Debug.Log(z);

            dolphin.transform.position = new Vector3(x, initalPlayerPosition.y + y, initalPlayerPosition.z + z);
            if (dolphin.transform.position.y > (initalPlayerPosition.y - 15) && dolphin.transform.position.z > (initalPlayerPosition.z + 40))     //z>40
            {
                movement.enabled = true;
                Debug.Log("ho fatto player movement enable");

            }
            if (dolphin.transform.position.y > initalPlayerPosition.y - 5 && dolphin.transform.position.z > initalPlayerPosition.z + 60)
            {
                Debug.Log("Reducing the speed");
                speed = 15f;
            }
        }
        else
        {
            Debug.Log(movement.start);
            this.enabled = false;
            movement.start = true;
            Debug.Log("FINAL TIME COUNTER");
            Debug.Log(timeCounter);
            timeCounter = 0;
            count += 1;
            Debug.Log("Numero iterazione");
            Debug.Log(count);
        }
    }


    public void SetPlayerPosition(Vector3 playerPos)
    {
        initalPlayerPosition = playerPos;
        if (playerPos.y < 42)
        {
            height = 5;
        }
        if (playerPos.y < 46 && playerPos.y >= 42)
        {
            height = 7;
        }
        if (playerPos.y < 50 && playerPos.y >= 46)
        {
            height = 12;
        }
        if (playerPos.y < 55 && playerPos.y >= 50)
        {
            height = 15;
        }
        if (playerPos.y < 60 && playerPos.y >= 55)
        {
            height = 20;
        }
        if (playerPos.y < 65 && playerPos.y >= 60)
        {
            height = 25;
        }
        if (playerPos.y < 70 && playerPos.y >= 65)
        {
            height = 30;
        }
        if (playerPos.y <= 75 && playerPos.y >= 70)
        {
            height = 35;
        }


        Debug.Log("initialPosition");
        Debug.Log(initalPlayerPosition);
    }
}

