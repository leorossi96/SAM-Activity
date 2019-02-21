
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagnifierMovement : MonoBehaviour {

    public GameObject player;


    public float velocityApplied = 0.5f;

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
            transform.position = new Vector3(transform.position.x, 0.2f, transform.position.z);

            if (Input.anyKey)
            {
                Vector3 position = transform.position;

                if (Input.GetKey(KeyCode.UpArrow))
                {
                    /*position = position + transform.forward * velocityApplied * Time.deltaTime;
                    transform.position = position;*/
                    transform.position = Vector3.MoveTowards(transform.position, transform.position + new Vector3(0, 0, 1), velocityApplied);

                }
                if (Input.GetKey(KeyCode.DownArrow))
                {
                    /*position = position - transform.forward * velocityApplied * Time.deltaTime;
                    transform.position = position;*/
                    transform.position = Vector3.MoveTowards(transform.position, transform.position + new Vector3(0, 0, -1), velocityApplied);

                }
                if (Input.GetKey(KeyCode.LeftArrow))
                {
                    /*position = position - transform.right * velocityApplied * Time.deltaTime;
                    transform.position = position;*/
                    transform.position = Vector3.MoveTowards(transform.position, transform.position + new Vector3(-1, 0, 0), velocityApplied);

                }
                if (Input.GetKey(KeyCode.RightArrow))
                {
                    /*position = position + transform.right * velocityApplied * Time.deltaTime;
                    transform.position = position;*/
                    transform.position = Vector3.MoveTowards(transform.position, transform.position + new Vector3(1, 0, 0), velocityApplied);

                }
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
