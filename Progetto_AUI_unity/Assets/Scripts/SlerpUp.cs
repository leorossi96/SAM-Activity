using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlerpUp: MonoBehaviour {

    public Vector3 initalPlayerPosition;
    
    
    public GameObject dolphin;
    float timeCounter = 0;
    float speed=18f;
    float height;
    float width=50;
    public PlayerMovement movement;
    int count = 0;
    public int referenceAxis;
    public int dir;




    void Start()
    {
        this.enabled = false;
    }


    // Update is called once per frame
    void Update()
    {

        if (dolphin.transform.position.y >= initalPlayerPosition.y)
        {
            timeCounter += Time.deltaTime * (speed);
            if (referenceAxis == 0)
            {
                float x = initalPlayerPosition.x;
                //float y = Mathf.Sin(Mathf.PI * (dolphin.transform.position.z - (initalPlayerPosition.z - 2)) / 80) * height;
                float z = timeCounter;

                if (dir == 1)
                {
                    float y = Mathf.Sin(Mathf.PI * (dolphin.transform.position.z - (initalPlayerPosition.z - 2)) / 80) * height;
                    dolphin.transform.position = new Vector3(x, initalPlayerPosition.y + y, initalPlayerPosition.z + z);                
                    if (dolphin.transform.position.y > (initalPlayerPosition.y + 8) && dolphin.transform.position.z > (initalPlayerPosition.z + 40))     //z>40
                    {                        
                        movement.enabled = true;
                    }

                    if (dolphin.transform.position.y > initalPlayerPosition.y + 5 && dolphin.transform.position.z > initalPlayerPosition.z + 60)
                        speed = 15f;

                }
                else if (dir == 0)
                {
                    float y = -Mathf.Sin(Mathf.PI * (dolphin.transform.position.z - (initalPlayerPosition.z + 2)) / 80) * height;
                    dolphin.transform.position = new Vector3(x, initalPlayerPosition.y + y, initalPlayerPosition.z - z);
                    if (dolphin.transform.position.y > (initalPlayerPosition.y + 8) && dolphin.transform.position.z > (initalPlayerPosition.z - 40))     //z>40
                        movement.enabled = true;

                    if (dolphin.transform.position.y > initalPlayerPosition.y + 5 && dolphin.transform.position.z > initalPlayerPosition.z - 60)
                        speed = 15f;
                }


            }
            else if (referenceAxis == 1)
            {
                float x = timeCounter;
                //float y = Mathf.Sin(Mathf.PI * (dolphin.transform.position.x - (initalPlayerPosition.x - 2)) / 80) * height;
                float z = initalPlayerPosition.z;


                if (dir == 1)
                {
                    float y = Mathf.Sin(Mathf.PI * (dolphin.transform.position.x - (initalPlayerPosition.x - 2)) / 80) * height;
                    dolphin.transform.position = new Vector3(initalPlayerPosition.x + x, initalPlayerPosition.y + y, z);
                    if (dolphin.transform.position.y > (initalPlayerPosition.y + 8) && dolphin.transform.position.x > (initalPlayerPosition.x + 40))     //z>40
                        movement.enabled = true;

                    if (dolphin.transform.position.y > initalPlayerPosition.y + 5 && dolphin.transform.position.x > initalPlayerPosition.x + 60)
                        speed = 15f;

                }
                else if (dir == 0)
                {
                    float y = -Mathf.Sin(Mathf.PI * (dolphin.transform.position.x - (initalPlayerPosition.x + 2)) / 80) * height;
                   // Debug.Log(-Mathf.Sin(Mathf.PI * (dolphin.transform.position.x - (initalPlayerPosition.x + 2)) / 80) * height);
                    dolphin.transform.position = new Vector3(initalPlayerPosition.x - x, initalPlayerPosition.y + y, z);
                    if (dolphin.transform.position.y > (initalPlayerPosition.y + 8) && dolphin.transform.position.x > (initalPlayerPosition.x - 40))     //z>40
                        movement.enabled = true;

                    if (dolphin.transform.position.y > initalPlayerPosition.y + 5 && dolphin.transform.position.x > initalPlayerPosition.x - 60)
                        speed = 15f;
                }


            }




        }       else
                        {
                            this.enabled = false;
                            movement.start = true;
                            timeCounter = 0;
                            count += 1;
                        }

    }


    public void SetPlayerPosition(Vector3 playerPos, int refAx, int direction)
    {
        initalPlayerPosition = playerPos;
        if (playerPos.y < 2)
        {
            height = 35;
        }
        if (playerPos.y < 6 && playerPos.y>=2)
        {
            height = 30;
        }
        if (playerPos.y < 10 && playerPos.y >= 6)
        {
            height = 25;
        }
        if (playerPos.y < 15 && playerPos.y >= 10)
        {
            height = 20;
        }
        if (playerPos.y < 15 && playerPos.y >= 10)
        {
            height = 17;
        }
        if (playerPos.y < 20 && playerPos.y >= 15)
        {
            height = 15;
        }
        if (playerPos.y < 24 && playerPos.y >= 20)
        {
            height = 13;
        }
        if (playerPos.y <= 27 && playerPos.y >= 24)
        {
            height = 10;
        }

        referenceAxis = refAx;
        dir = direction;

    }
}



/*if (dolphin.transform.position.y >= initalPlayerPosition.y)
        {
            timeCounter += Time.deltaTime* (speed);
            
            float x = initalPlayerPosition.x;
float y = Mathf.Sin(Mathf.PI * (dolphin.transform.position.z - (initalPlayerPosition.z - 2)) / 80) * height;
float z = timeCounter;


dolphin.transform.position = new Vector3(x, initalPlayerPosition.y+y, initalPlayerPosition.z+z);
            if (dolphin.transform.position.y<(initalPlayerPosition.y + 15) && dolphin.transform.position.z> (initalPlayerPosition.z+40))     //z>40
                    movement.enabled = true;
               
                
            
            if (dolphin.transform.position.y<initalPlayerPosition.y + 5 && dolphin.transform.position.z> initalPlayerPosition.z + 60)         
                    speed = 15f;
            
        }
        else
        {   


            this.enabled = false;
            movement.start = true;           
            timeCounter = 0;
            count += 1;


        }*/