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
    public int referenceAxis;
    public int dir;






    void Start()
    {
        this.enabled = false;
    }


    // Update is called once per frame
    void Update()                   //referenceAxis=1 asse x        referenceAxis=0 asse z
    {                               //dir=1 direzione crescente     dir=0 direzione decrescente

        if (dolphin.transform.position.y <= initalPlayerPosition.y)
        {
            timeCounter += Time.deltaTime * (speed);
            if (referenceAxis == 0)
            {
                    float x = initalPlayerPosition.x;
                    
                    float z = timeCounter;

                if (dir == 1)
                {
                    float y = -Mathf.Sin(Mathf.PI * (dolphin.transform.position.z - (initalPlayerPosition.z - 2)) / 80) * height;
                    dolphin.transform.position = new Vector3(x, initalPlayerPosition.y + y, initalPlayerPosition.z + z);
                    if (dolphin.transform.position.y > (initalPlayerPosition.y - 15) && dolphin.transform.position.z > (initalPlayerPosition.z + 40))     //z>40
                        movement.enabled = true;

                    if (dolphin.transform.position.y > initalPlayerPosition.y - 5 && dolphin.transform.position.z > initalPlayerPosition.z + 60)
                        speed = 15f;

                }
                else if (dir == 0) {
                                    float y = Mathf.Sin(Mathf.PI * (dolphin.transform.position.z - (initalPlayerPosition.z + 2)) / 80) * height;
                                    dolphin.transform.position = new Vector3(x, initalPlayerPosition.y + y, initalPlayerPosition.z - z);
                                    if (dolphin.transform.position.y > (initalPlayerPosition.y - 15) && dolphin.transform.position.z > (initalPlayerPosition.z - 40))     //z>40
                                        movement.enabled = true;

                                    if (dolphin.transform.position.y > initalPlayerPosition.y - 5 && dolphin.transform.position.z > initalPlayerPosition.z - 60)
                                        speed = 15f;
                                    }
                                
                    
            } else if(referenceAxis==1)
                    {
                        float x = timeCounter;
                        float z = initalPlayerPosition.z;


                if (dir == 1)
                {
                    float y = -Mathf.Sin(Mathf.PI * (dolphin.transform.position.x - (initalPlayerPosition.x - 2)) / 80) * height;
                    dolphin.transform.position = new Vector3(initalPlayerPosition.x + x, initalPlayerPosition.y + y, z);
                            if (dolphin.transform.position.y > (initalPlayerPosition.y - 15) && dolphin.transform.position.x > (initalPlayerPosition.x + 40))     //z>40
                                movement.enabled = true;

                            if (dolphin.transform.position.y > initalPlayerPosition.y - 5 && dolphin.transform.position.x > initalPlayerPosition.x + 60)
                                speed = 15f;

                        }
                        else if (dir == 0)
                        {                         
                            float y = Mathf.Sin(Mathf.PI * (dolphin.transform.position.x - (initalPlayerPosition.x + 2)) / 80) * height;                          
                            dolphin.transform.position = new Vector3(initalPlayerPosition.x - x, initalPlayerPosition.y + y, z);
                            if (dolphin.transform.position.y > (initalPlayerPosition.y - 15) && dolphin.transform.position.x > (initalPlayerPosition.x - 40))     //z>40
                                movement.enabled = true;

                            if (dolphin.transform.position.y > initalPlayerPosition.y - 5 && dolphin.transform.position.x > initalPlayerPosition.x - 60)
                                speed = 15f;
                        }


            }




        }   else
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
        if (playerPos.y < 42)
        {
            height = 25; //5
        }
        if (playerPos.y < 46 && playerPos.y >= 42)
        {
            height = 30; //7
        }
        if (playerPos.y < 50 && playerPos.y >= 46)
        {
            height = 35;    //12
        }
        if (playerPos.y < 55 && playerPos.y >= 50)
        {
                height = 40;       //15
            }
        if (playerPos.y < 60 && playerPos.y >= 55)
        {
            height = 45;        //20
        }
        if (playerPos.y < 65 && playerPos.y >= 60)
        {
            height = 50;        //25
        }
        if (playerPos.y < 70 && playerPos.y >= 65)
        {
            height = 55;        //30
        }
        if (playerPos.y < 75 && playerPos.y >= 70)
        {
            height = 60;        //35
        }
        if (playerPos.y <= 85 && playerPos.y >= 75)
        {
            height = 65;        //35
        }

        referenceAxis = refAx;
        dir = direction;
       
    }
}

