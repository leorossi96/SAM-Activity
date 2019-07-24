using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShowPatient : MonoBehaviour
{


    public Login login;
    public GameObject button;
    public int x = 0;
    public int y = 150;

    // Use this for initialization
    void Start()
    {





    }

    // Update is called once per frame
    public void setButton()
    {

        Debug.Log("sono nello start");
        for (int i = 0; i < login.patientData.Length; i++)
        {
            Debug.Log("sto per istanziare");
            Instantiate(button, new Vector3(x, y, 0), Quaternion.identity);
            y = y - 10;
            Debug.Log("instanziato");
            //button.GetComponentInChildren = login.patientData[i].first_name;

            Debug.Log("showPatients: " + login.patientData[i].last_name + login.patientData[i].first_name);

        }
    }
}
