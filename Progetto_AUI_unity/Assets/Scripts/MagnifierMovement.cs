
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

    public int mainPlayerKinectElement =0;
    public Camera cameraSearch;

    float kinect_roomcenter_z = 1.3f; // da aggiornare per ogni stanza
    float room_max_x = -1.0f; // da aggiornare per ogni stanza
    float game_max_x = -17.0f;
    float room_max_z = 0.8f;
    float game_max_z = 17.0f;


    public float prva;
    public float offset_z;

    static bool searchPhase = false;

    // Use this for initialization
    void Start () {
        transform.position = player.transform.position;

            if (GameObject.Find("Dolphin1") != null)
            { 
                dolphinController = GameObject.Find("Dolphin1").GetComponent<SmartToy>();
                delfinoFound = true;
            }
        MagicRoomKinectV2Manager.instance.setUpKinect(10, 1);
        MagicRoomKinectV2Manager.instance.startSamplingKinect(KinectSamplingMode.Streaming);
       
        gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (MagicRoomKinectV2Manager.instance.skeletons[mainPlayerKinectElement].SpineBase.z < 0.1f)
        {
            float min = 10000;
            for (int i = 0; i < 6; i++)
            {
                if (MagicRoomKinectV2Manager.instance.skeletons[i].SpineBase.z < min && MagicRoomKinectV2Manager.instance.skeletons[i].SpineBase.z > 0.5f)
                {
                    min = MagicRoomKinectV2Manager.instance.skeletons[i].SpineBase.z;
                    mainPlayerKinectElement = i;
                }
            }
        }
        

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

            float kinect_x = MagicRoomKinectV2Manager.instance.skeletons[mainPlayerKinectElement].SpineBase.x;
            float kinect_z = MagicRoomKinectV2Manager.instance.skeletons[mainPlayerKinectElement].SpineBase.z;
            

            float camera_x = cameraSearch.transform.position.x;
            float camera_z = cameraSearch.transform.position.z;
            float scale_x = (game_max_x - cameraSearch.transform.position.x) / room_max_x;
            float scale_z = (game_max_z - cameraSearch.transform.position.z) / room_max_z;



            transform.position = new Vector3((float)cameraSearch.transform.position.x+kinect_x*prva, 0.1f, (float)cameraSearch.transform.position.z+(kinect_z-kinect_roomcenter_z)*(-prva)+offset_z);

            //transform.position = new_pos + player.transform.position; 


                //Vector3 position = transform.position;

            /* IN CASO DI UTILIZZO CON IL DELFINO 
             * if (dolphinController.touchsensor.touchpoints[0].touched)
            {
            //position = position + transform.forward * velocityApplied * Time.deltaTime;
            //transform.position = position;
            transform.position = Vector3.MoveTowards(transform.position, transform.position + new Vector3(0, 0, 1*temp), velocityApplied);

        }
        if (dolphinController.touchsensor.touchpoints[4].touched)
            {
                //position = position - transform.forward * velocityApplied * Time.deltaTime;
                //transform.position = position;
                transform.position = Vector3.MoveTowards(transform.position, transform.position + new Vector3(0, 0, -1*temp), velocityApplied);

            }
            if (dolphinController.touchsensor.touchpoints[2].touched)
            {
                //position = position - transform.right * velocityApplied * Time.deltaTime;
                //transform.position = position;
                transform.position = Vector3.MoveTowards(transform.position, transform.position + new Vector3(-1*temp, 0, 0), velocityApplied);

            }
        if (dolphinController.touchsensor.touchpoints[1].touched)
        {
            //position = position + transform.right * velocityApplied * Time.deltaTime;
            //transform.position = position;
            transform.position = Vector3.MoveTowards(transform.position, transform.position + new Vector3(1*temp, 0, 0), velocityApplied);

        }*/
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
