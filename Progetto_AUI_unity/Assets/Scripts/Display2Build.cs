using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Display2Build : MonoBehaviour {

    private void Awake()
    {
        for (int i = 1; i < Display.displays.Length; i++)
        {
            Display.displays[i].Activate();
        }
    }
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
