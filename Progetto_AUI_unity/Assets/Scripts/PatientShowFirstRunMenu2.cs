using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class PatientShowFirstRunMenu2 : MonoBehaviour {

    
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
                inpfields[i].text = levelSet.levelRun[0].static_obstacle.ToString();
            }

            if (inpfields[i].name == "DynamicObstacle")
            {
                inpfields[i].text = levelSet.levelRun[0].dynamic_obstacle.ToString();
            }

            if (inpfields[i].name == "MaxTime")
            {
                inpfields[i].text = levelSet.levelRun[0].max_time.ToString();
            }

            if (inpfields[i].name == "PowerUp")
            {
                inpfields[i].text = levelSet.levelRun[0].power_up.ToString();
            }

            if (inpfields[i].name == "Lives")
            {
                inpfields[i].text = levelSet.levelRun[0].lives.ToString();
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
                if (!(string.IsNullOrEmpty(inpfields[i].text)))
                {
                    levelSet.levelRun[0].static_obstacle = int.Parse(inpfields[i].text);
                    Debug.Log("UPDATE PATIENT SHOW SEARCH POWERUP: " + levelSet.levelRun[0].static_obstacle);
                }
            }

            if (inpfields[i].name == "DynamicObstacle")
            {
                if (!(string.IsNullOrEmpty(inpfields[i].text)))
                {
                    levelSet.levelRun[0].dynamic_obstacle = int.Parse(inpfields[i].text);
                }
            }

            if (inpfields[i].name == "MaxTime")
            {
                if (!(string.IsNullOrEmpty(inpfields[i].text)))
                {
                    levelSet.levelRun[0].max_time = int.Parse(inpfields[i].text);
                }
            }

            if (inpfields[i].name == "PowerUp")
            {
                if (!(string.IsNullOrEmpty(inpfields[i].text)))
                {
                    levelSet.levelRun[0].power_up = int.Parse(inpfields[i].text);
                }
            }

            if (inpfields[i].name == "Lives")
            {
                if (!(string.IsNullOrEmpty(inpfields[i].text)))
                {
                    levelSet.levelRun[0].lives = int.Parse(inpfields[i].text);
                }
            }
            

        }

    }
}
