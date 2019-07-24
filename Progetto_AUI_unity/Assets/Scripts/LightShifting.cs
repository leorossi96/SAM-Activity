using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightShifting : MonoBehaviour {

    public bool finish = true;
    public bool light = false;
    public int count = 100;
    public int temp = 100;
    int sign = 1; 

	// Use this for initialization
	void Start () {
		
	}

    private IEnumerator fadecolor()
    {



        /*if (!light)
        {
            MagicRoomLightManager.instance.sendColour("#A5C1E5", 255);
            light = true; 
        } else
        {
            MagicRoomLightManager.instance.sendColour("#A5C1E5", 50);
            light = false;
        }*/
        yield return new WaitForSeconds(1.0f);
        MagicRoomLightManager.instance.sendColour(Color.blue, count);
        temp = count + 50*sign;
        if (temp > 150 || temp < 100)
        {
            sign = -sign;
            temp = count + 50 * sign;
        }
        count = temp; 
        
        StartCoroutine(fadecolor()); 
    }

    // Update is called once per frame
    void Update () {

        /*if (finish)
        {
            finish = false; 
            StartCoroutine(fadecolor()); 
        }*/
        if (finish)
        {
            finish = false;
            StartCoroutine(fadecolor()); 
        }
            
       
        
    }

    public void OnDestroy()
    {
        MagicRoomLightManager.instance.sendColour(Color.black, 255);
    }
}
