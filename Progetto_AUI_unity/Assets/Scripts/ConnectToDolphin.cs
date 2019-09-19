using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConnectToDolphin : MonoBehaviour {

    SmartToy dolphinController;
    public bool openedStream = false;

    // Use this for initialization
    /*void Start () {

           if (GameObject.Find("Dolphin1") != null)
            {
                UDPListenerForMagiKRoom.instance.StartReceiver(10);
                //MagicRoomSmartToyManager.instance.openEventChannelSmartToy("Dolphin1");
                MagicRoomSmartToyManager.instance.openStreamSmartToy("Dolphin1", 10f);
            StartCoroutine(delayedStream());
                dolphinController = GameObject.Find("Dolphin1").GetComponent<SmartToy>();
                //dolphinController.objectposition.gyroscope();
                StartCoroutine(waittoStartGreenLight());
            }
        }*/

    private void Awake()
    {
        for (int i = 1; i < Display.displays.Length; i++)
        {
            Display.displays[i].Activate();
        }
    }

    void Update()
    {
        
        if (GameObject.Find("Dolphin1") != null && !openedStream)
        {

            openedStream = true;
            UDPListenerForMagiKRoom.instance.StartReceiver(10);
            //MagicRoomSmartToyManager.instance.openEventChannelSmartToy("Dolphin1");
            MagicRoomSmartToyManager.instance.openStreamSmartToy("Dolphin1", 10f);
            StartCoroutine(delayedStream());
            dolphinController = GameObject.Find("Dolphin1").GetComponent<SmartToy>();
            //dolphinController.objectposition.gyroscope();
            StartCoroutine(DolphinConnectedGreenLight());
        }
    }
    IEnumerator DolphinConnectedGreenLight()
    {
        dolphinController.executeCommandLightController(Color.green, 100, "parthead");
        yield return new WaitForSeconds(3);
        dolphinController.executeCommandLightController(Color.black, 0, "parthead");
    }

    public IEnumerator delayedStream()
    {
        yield return new WaitForSeconds(1.0f);
        MagicRoomSmartToyManager.instance.openEventChannelSmartToy("Dolphin1");

    }

    /*    void Awake()
        {
            GameObject controller = GameObject.FindGameObjectWithTag("ControllerManager");
            GameObject dolphin1 = GameObject.FindGameObjectWithTag("Dolphin1");

            DontDestroyOnLoad(controller);
            DontDestroyOnLoad(dolphin1);
        }*/
}

