using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PatientShowSearch : MonoBehaviour {

    public Login login;
    public LevelSet levelSet;
    public GameObject patientShow;







    // Update is called once per frame
    void Update()
    {
        TextMeshProUGUI textPatient = patientShow.GetComponentInChildren<TextMeshProUGUI>();
        textPatient.text = login.selectedPatient.last_name + " " + login.selectedPatient.first_name;
        // textPatient.GetComponent<GUIText>().text = login.selectedPatient.last_name + " " + login.selectedPatient.first_name;
        InputField[] inpfields = patientShow.GetComponentsInChildren<InputField>();
        Debug.Log("UPDATE PATIENT SHOW SEARCH POWERUP: " + levelSet.levelSearch.power_up);
        for(int i=0; i < inpfields.Length; i++)
        {
            if(inpfields[i].name == "StaticObstacle")
            {
                inpfields[i].text = levelSet.levelSearch.static_obstacle.ToString();
            }

            if (inpfields[i].name == "DynamicObstacle")
            {
                inpfields[i].text = levelSet.levelSearch.dynamic_obstacle.ToString();
            }

            if (inpfields[i].name == "MaxTime")
            {
                inpfields[i].text = levelSet.levelSearch.max_time.ToString();
            }

            if (inpfields[i].name == "PowerUp")
            {
                inpfields[i].text = levelSet.levelSearch.power_up.ToString();
            }

            if (inpfields[i].name == "Lives")
            {
                inpfields[i].text = levelSet.levelSearch.lives.ToString();
            }
        }



    }
}
