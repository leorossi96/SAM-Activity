using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System;

public class PatientShowSearch : MonoBehaviour {

    public Login login;
    public LevelSet levelSet;
    public GameObject patientShow;
    InputField[] inpfields;
    public Button addButton;
    public int numberOfZones;


    void Awake()
    {
        addButton.onClick.AddListener(TaskOnClick);
        TextMeshProUGUI textPatient = patientShow.GetComponentInChildren<TextMeshProUGUI>();
        textPatient.text = login.selectedPatient.last_name + " " + login.selectedPatient.first_name;
        // textPatient.GetComponent<GUIText>().text = login.selectedPatient.last_name + " " + login.selectedPatient.first_name;
        inpfields = patientShow.GetComponentsInChildren<InputField>();
        numberOfZones = levelSet.zoneLevelSearch.Length;
        Debug.Log("THE ARRAY OF ZONE HAS A LENGHT OF: " + numberOfZones);
        for(int i=numberOfZones; i < inpfields.Length; i++)
        {
            string str_i = i.ToString();
            if (inpfields[i].name == ("Zone_Search_" + str_i));
            {
                inpfields[i].gameObject.SetActive(false);
            }
        }

        for(int i=0; i < numberOfZones; i++)
        {
            if(inpfields[i].name == "Zone_Search_" + i.ToString())
            {
                inpfields[i].text = levelSet.zoneLevelSearch[i].number_stars_per_zone.ToString();
            }

        }



    }

    void Update()
    {
        numberOfZones = levelSet.zoneLevelSearch.Length;
        Debug.Log("THE ARRAY OF ZONE HAS A LENGHT OF: " + numberOfZones);
        for (int i = numberOfZones; i < inpfields.Length; i++)
        {
            string str_i = i.ToString();
            if (inpfields[i].name == ("Zone_Search_" + str_i)) ;
            {
                inpfields[i].gameObject.SetActive(false);
            }
        }

        for (int i = 0; i < numberOfZones; i++)
        {
            if (inpfields[i].name == "Zone_Search_" + i.ToString())
            {
                inpfields[i].text = levelSet.zoneLevelSearch[i].number_stars_per_zone.ToString();
            }

        }

    }


    public void TaskOnClick()
    {
        Debug.Log("SONO DENTRO ADD BUTTON");
        int new_size = numberOfZones + 1;
        Array.Resize(ref levelSet.zoneLevelSearch, new_size);
        levelSet.zoneLevelSearch[new_size - 1].number = new_size;
        levelSet.zoneLevelSearch[new_size - 1].number_stars_per_zone = 3;
    }

}
