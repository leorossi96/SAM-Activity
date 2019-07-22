
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagnifierMovement : MonoBehaviour {

    public GameObject player;

    public bool delfinoFound = false;
    SmartToy dolphinController;

    public float velocityApplied = 0.5f;
    public float temp = 0.1f;

    GameObject collectibleArea;

    static bool searchPhase = false;

    // Use this for initialization
    void Start () {
        transform.position = player.transform.position;

            if (GameObject.Find("Dolphin1") != null)
            { 
                dolphinController = GameObject.Find("Dolphin1").GetComponent<SmartToy>();
                delfinoFound = true;
            }
    }

    // Update is called once per frame
    void Update()
    {
        if (delfinoFound)
        {
            if (GameObject.Find("Dolphin1") != null)
            {
                dolphinController = GameObject.Find("Dolphin1").GetComponent<SmartToy>();
                delfinoFound = true;
            }
        }
        Debug.Log("SearchPhase = "+ searchPhase);
        if (searchPhase)
        {
            transform.position = new Vector3(transform.position.x, 0.2f, transform.position.z);

                Vector3 position = transform.position;

                if (dolphinController.touchsensor.touchpoints[0].touched)
                {
                /*position = position + transform.forward * velocityApplied * Time.deltaTime;
                transform.position = position;*/
                transform.position = Vector3.MoveTowards(transform.position, transform.position + new Vector3(0, 0, 1*temp), velocityApplied);

            }
            if (dolphinController.touchsensor.touchpoints[4].touched)
                {
                    /*position = position - transform.forward * velocityApplied * Time.deltaTime;
                    transform.position = position;*/
                    transform.position = Vector3.MoveTowards(transform.position, transform.position + new Vector3(0, 0, -1*temp), velocityApplied);

                }
                if (dolphinController.touchsensor.touchpoints[2].touched)
                {
                    /*position = position - transform.right * velocityApplied * Time.deltaTime;
                    transform.position = position;*/
                    transform.position = Vector3.MoveTowards(transform.position, transform.position + new Vector3(-1*temp, 0, 0), velocityApplied);

                }
            if (dolphinController.touchsensor.touchpoints[1].touched)
            {
                /*position = position + transform.right * velocityApplied * Time.deltaTime;
                transform.position = position;*/
                transform.position = Vector3.MoveTowards(transform.position, transform.position + new Vector3(1*temp, 0, 0), velocityApplied);

            }
        }
    }

    public static bool GetSearchPhase(){
        return searchPhase;
    }
    public static void SetSearchPhase(bool value){
        searchPhase = value;
    }

    public void SetCollectibleArea(GameObject o){
        collectibleArea = o;
    }

}
