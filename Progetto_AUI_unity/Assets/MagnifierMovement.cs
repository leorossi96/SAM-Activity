
using System.Collections;
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

	}

    // Update is called once per frame
    void Update()
    {
        Debug.Log("SearchPhase = "+ searchPhase);
        if (searchPhase)
        {

            if (Input.anyKey)
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
        }
    }

    public static void SetSearchPhase(bool value){
        searchPhase = value;
    }

    public void SetCollectibleArea(GameObject o){
        collectibleArea = o;
    }

}
