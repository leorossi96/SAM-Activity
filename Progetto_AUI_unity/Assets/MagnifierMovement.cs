<<<<<<< HEAD
﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagnifierMovement : MonoBehaviour {

    public GameObject player;
    

    public float velocityApplied = 10f;

    GameObject collectibleArea;

    static bool searchPhase = false;

	// Use this for initialization
	void Start () {
        transform.position = player.transform.position;
=======
﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagnifierMovement : MonoBehaviour {

    public GameObject player;
    

    public float velocityApplied = 10f;
    public float rotationSpeed = 3f;

    static bool searchPhase = false;

	// Use this for initialization
	void Start () {
        transform.position = player.transform.position;
>>>>>>> cfb11b9305e66852f163dc7126a17bbee8d3ba94
	}

    // Update is called once per frame
    void Update()
<<<<<<< HEAD
    {
        Debug.Log("SearchPhase = "+ searchPhase);
        if (searchPhase)
        {

            if (Input.anyKey)
=======
    {

        if (searchPhase)
        {
            if (Input.anyKey)
>>>>>>> cfb11b9305e66852f163dc7126a17bbee8d3ba94
            {
                Vector3 position = transform.position;

                if (Input.GetKey(KeyCode.UpArrow))
                {
                    position = position + transform.forward * velocityApplied * Time.deltaTime;
                    transform.position = position;
                }
                if (Input.GetKey(KeyCode.DownArrow))
                {
                    position = position - transform.forward * velocityApplied * Time.deltaTime;
                    transform.position = position;
                }
                if (Input.GetKey(KeyCode.LeftArrow))
                {
                    position = position - transform.right * velocityApplied * Time.deltaTime;
                    transform.position = position;

                }
                if (Input.GetKey(KeyCode.RightArrow))
                {
                    position = position + transform.right * velocityApplied * Time.deltaTime;
                    transform.position = position;
                }
            }
            else
            {
                Vector3 onGroundPosition = new Vector3(0, player.transform.position.y, 0);
                transform.position = player.transform.position - onGroundPosition;
            }
<<<<<<< HEAD
        }
    }

    public static void SetSearchPhase(bool value){
        searchPhase = value;
    }

    public void SetCollectibleArea(GameObject o){
        collectibleArea = o;
    }

}
=======
        }
    }

    public static void SetSearchPhase(bool value){
        searchPhase = value;
    }

}
>>>>>>> cfb11b9305e66852f163dc7126a17bbee8d3ba94
