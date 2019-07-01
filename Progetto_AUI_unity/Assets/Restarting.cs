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
        

        if(Input.GetKey(KeyCode.R)){
            SceneManager.LoadScene(sceneName); 
        }
    }
}