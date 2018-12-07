using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConnectToDolphin : MonoBehaviour {

    SmartToy dolphinController;

    // Use this for initialization
    void Start () {

           if (GameObject.Find("Dolphin1") != null)
            {
                MagicRoomSmartToyManager.instance.openEventChannelSmartToy("Dolphin1");
                MagicRoomSmartToyManager.instance.openStreamSmartToy("Dolphin1", 10f);
                dolphinController = GameObject.Find("Dolphin1").GetComponent<SmartToy>();
                //dolphinController.objectposition.gyroscope();
                StartCoroutine(waittoStartGreenLight());
            }
        }

    IEnumerator waittoStartGreenLight()
    {
        yield return new WaitForSeconds(1);
        dolphinController.executeCommandLightController(Color.green, 0, "parthead");
    }

    void Awake()
    {
        GameObject controller = GameObject.FindGameObjectWithTag("ControllerManager");
        GameObject dolphin1 = GameObject.FindGameObjectWithTag("Dolphin1");

        DontDestroyOnLoad(controller);
        DontDestroyOnLoad(dolphin1);
    }
}

