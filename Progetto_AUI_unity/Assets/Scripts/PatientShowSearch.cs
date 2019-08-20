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
    public Button deleteButton;
    public int numberOfZones;


    void Awake()
    {
        addButton.onClick.AddListener(TaskOnClick);
        deleteButton.onClick.AddListener(TaskOnClickDelete);
        TextMeshProUGUI textPatient = patientShow.GetComponentInChildren<TextMeshProUGUI>();
        textPatient.text = login.selectedPatient.last_name + " " + login.selectedPatient.first_name;
        // textPatient.GetComponent<GUIText>().text = login.selectedPatient.last_name + " " + login.selectedPatient.first_name;
        inpfields = patientShow.GetComponentsInChildren<InputField>();
        //numberOfZones = levelSet.zoneLevelSearch.Length;
        numberOfZones = levelSet.zoneLevelSearchList.Count;
        Debug.Log("NUMBER OF ZONES: " + numberOfZones);
        Debug.Log("INPUT FIELD LENGTH: " + inpfields.Length);  // Length = 6
        for (int i=numberOfZones; i < inpfields.Length; i++)
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
                inpfields[i].text = levelSet.zoneLevelSearchList[i].number_stars_per_zone.ToString();
            }

        }



    }

    void Update()
    {
        numberOfZones = levelSet.zoneLevelSearchList.Count;
        Debug.Log("THE ARRAY OF ZONE HAS A LENGHT OF: " + numberOfZones);
        for (int i = numberOfZones; i < inpfields.Length; i++)
        {
            string str_i = i.ToString();
            if (inpfields[i].name == ("Zone_Search_" + str_i));
            {
                inpfields[i].gameObject.SetActive(false);
            }
        }


        for (int i = 0; i < numberOfZones; i++)
        {
            if (inpfields[i].name == "Zone_Search_" + i.ToString())
            {
                //inpfields[i].text = levelSet.zoneLevelSearchList[i].number_stars_per_zone.ToString();
                levelSet.zoneLevelSearchList[i].number_stars_per_zone = int.Parse(inpfields[i].text);
            }

        }

    }


    public void TaskOnClick()
    {
        int old_num_zones = numberOfZones;
        Debug.Log("SONO DENTRO ADD BUTTON");
        ZoneLevelSearch new_zone = new ZoneLevelSearch();
        new_zone.number = numberOfZones + 1;
        new_zone.number_stars_per_zone = 3;
        levelSet.zoneLevelSearchList.Add(new_zone);
        inpfields[old_num_zones].gameObject.SetActive(true);
        inpfields[old_num_zones].text = new_zone.number_stars_per_zone.ToString();

        //int new_size = numberOfZones + 1;
        //Array.Resize(ref levelSet.zoneLevelSearch, new_size);
        //levelSet.zoneLevelSearch[new_size - 1].number = new_size;
        //levelSet.zoneLevelSearch[new_size - 1].number_stars_per_zone = 3;
    }

    public void TaskOnClickDelete()
    {

        levelSet.zoneLevelSearchList.RemoveAt(levelSet.zoneLevelSearchList.Count - 1);
    }

}
