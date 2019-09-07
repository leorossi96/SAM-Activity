﻿using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class PatientShowSecondRunMenu2 : MonoBehaviour {

    
    public LevelSet levelSet = null;
    public GameObject patientShow;
    InputField[] inpfields;



    void Awake()
    {
        levelSet = GameObject.Find("LevelSet").GetComponent<LevelSet>();
        TextMeshProUGUI textPatient = patientShow.GetComponentInChildren<TextMeshProUGUI>();
        textPatient.text = levelSet.patient_last_name + " " + levelSet.patient_first_name;
        // textPatient.GetComponent<GUIText>().text = login.selectedPatient.last_name + " " + login.selectedPatient.first_name;
        inpfields = patientShow.GetComponentsInChildren<InputField>();
        //Debug.Log("UPDATE PATIENT SHOW SEARCH POWERUP: " + levelSet.levelSearch.power_up);
        for (int i = 0; i < inpfields.Length; i++)
        {
            if (inpfields[i].name == "StaticObstacle")
            {
                inpfields[i].text = levelSet.levelRun[1].static_obstacle.ToString();
            }

            if (inpfields[i].name == "DynamicObstacle")
            {
                inpfields[i].text = levelSet.levelRun[1].dynamic_obstacle.ToString();
            }

            if (inpfields[i].name == "MaxTime")
            {
                inpfields[i].text = levelSet.levelRun[1].max_time.ToString();
            }

            if (inpfields[i].name == "PowerUp")
            {
                inpfields[i].text = levelSet.levelRun[1].power_up.ToString();
            }

            if (inpfields[i].name == "Lives")
            {
                inpfields[i].text = levelSet.levelRun[1].lives.ToString();
            }
        }
    }




    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < inpfields.Length; i++)
        {
            if (inpfields[i].name == "StaticObstacle")
            {
                levelSet.levelRun[1].static_obstacle = int.Parse(inpfields[i].text);
            }

            if (inpfields[i].name == "DynamicObstacle")
            {
                levelSet.levelRun[1].dynamic_obstacle = int.Parse(inpfields[i].text);
            }

            if (inpfields[i].name == "MaxTime")
            {
                levelSet.levelRun[1].max_time = int.Parse(inpfields[i].text);
                //Debug.Log("MAX TIME AGGIORNATO: " + levelSet.levelRun[1].max_time);
            }

            if (inpfields[i].name == "PowerUp")
            {
               levelSet.levelRun[1].power_up = int.Parse(inpfields[i].text);
            }

            if (inpfields[i].name == "Lives")
            {
                levelSet.levelRun[1].lives = int.Parse(inpfields[i].text);
            }
            

        }

    }
}
