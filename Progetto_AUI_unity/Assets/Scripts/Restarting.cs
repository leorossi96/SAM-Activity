using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.Remoting;
using UnityEngine.SceneManagement;
using System.Runtime.ConstrainedExecution;

public class Restarting : MonoBehaviour
{
    public string sceneName; 

    // Use this for initialization
    void Start()
    {
        this.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        
        if (GameObject.Find("Dolphin1") != null){

            if (GameObject.Find("Dolphin1").GetComponent<SmartToy>().touchsensor.touchpoints[0].touched)
            {
                SceneManager.LoadScene("Menu2");
            }

            if (GameObject.Find("Dolphin1").GetComponent<SmartToy>().touchsensor.touchpoints[5].touched)
            {
                SceneManager.LoadScene(sceneName);
            }



        }else{
            if (Input.GetKey(KeyCode.R))
            {
                SceneManager.LoadScene(sceneName);

            }

            if (Input.GetKey(KeyCode.E))
            {
                SceneManager.LoadScene("Menu2");
            }
        }

    }
}