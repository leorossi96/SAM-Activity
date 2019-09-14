using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System;
using UnityEngine.Networking;
using System.Collections;
using System.Collections.Generic;
using System.Text;

public class PatientShowSearchMenu2 : MonoBehaviour {

   
    public LevelSet levelSet = null;
    public GameObject patientShow;
    InputField[] inpfields;
    public Button addButton;
    public Button deleteButton;
    public int numberOfZones;
    public GameObject zls_0;
    public GameObject zls_1;
    public GameObject zls_2;
    public GameObject zls_3;
    public GameObject zls_4;
    public GameObject zls_5;
    public GameObject zls_6;
    public GameObject zls_7;
    public GameObject zls_8;
    public GameObject zls_9;
    public List<GameObject> list_zones = new List<GameObject>();
    private bool hasToBeActivated = false;


    void Awake()
    {
        levelSet = GameObject.Find("LevelSet").GetComponent<LevelSet>();
        addButton.onClick.AddListener(TaskOnClick);
        deleteButton.onClick.AddListener(TaskOnClickDelete);
        TextMeshProUGUI textPatient = patientShow.GetComponentInChildren<TextMeshProUGUI>();
        textPatient.text = levelSet.patient_last_name + " " + levelSet.patient_first_name;
        list_zones.Add(zls_0);
        list_zones.Add(zls_1);
        list_zones.Add(zls_2);
        list_zones.Add(zls_3);
        list_zones.Add(zls_4);
        list_zones.Add(zls_5);
        list_zones.Add(zls_6);
        list_zones.Add(zls_7);
        list_zones.Add(zls_8);
        list_zones.Add(zls_9);
        // textPatient.GetComponent<GUIText>().text = login.selectedPatient.last_name + " " + login.selectedPatient.first_name;
        inpfields = patientShow.GetComponentsInChildren<InputField>();
        //numberOfZones = levelSet.zoneLevelSearch.Length;
        numberOfZones = levelSet.zoneLevelSearchList.Count;
        for (int i = numberOfZones; i < list_zones.Count; i++)
        {
            //string str_i = i.ToString();
            list_zones[i].gameObject.SetActive(false);

        }
        for (int i = 0; i < numberOfZones; i++)
        {
            list_zones[i].GetComponentInChildren<InputField>().text = levelSet.zoneLevelSearchList[i].number_stars_per_zone.ToString();

        }



    }

    void Update()
    {
        numberOfZones = levelSet.zoneLevelSearchList.Count;
        /*Debug.Log("THE ARRAY OF ZONE HAS A LENGHT OF: " + numberOfZones);
        for (int i = 0; i < inpfields.Length; i++)
        {
            bool hasToBeActivated = i < numberOfZones;
            string str_i = i.ToString();
            if (inpfields[i].name == ("Zone_Search_" + str_i)) ;
            {
                inpfields[i].gameObject.SetActive(hasToBeActivated);
            }
        }*/

        for (int i = 0; i < list_zones.Count; i++)
        {
            if (i < numberOfZones)
            {
                bool hasToBeActivated = true;
                list_zones[i].gameObject.SetActive(hasToBeActivated);
            }
            else
            {
                bool hasToBeActivated = false;
                list_zones[i].gameObject.SetActive(hasToBeActivated);
            }


        }

        /*
        for (int i = 0; i < numberOfZones; i++)
        {
            if (inpfields[i].name == "Zone_Search_" + i.ToString())
            {
                if (!(string.IsNullOrEmpty(inpfields[i].text)))
                {
                    //inpfields[i].text = levelSet.zoneLevelSearchList[i].number_stars_per_zone.ToString();
                    levelSet.zoneLevelSearchList[i].number_stars_per_zone = int.Parse(inpfields[i].text);
                }
            }
        }*/

        for (int i = 0; i < numberOfZones; i++)

        {
            if (!(string.IsNullOrEmpty(list_zones[i].GetComponentInChildren<InputField>().text)))
            {
                levelSet.zoneLevelSearchList[i].number_stars_per_zone = int.Parse(list_zones[i].GetComponentInChildren<InputField>().text);
            }


        }
    }


    public void TaskOnClick()
    {
        if (levelSet.zoneLevelSearchList.Count < 10)
        {
            int old_num_zones = numberOfZones;
            Debug.Log("SONO DENTRO ADD BUTTON");
            ZoneLevelSearch new_zone = new ZoneLevelSearch();
            new_zone.number = numberOfZones + 1;
            new_zone.number_stars_per_zone = 3;
            levelSet.zoneLevelSearchList.Add(new_zone);
            list_zones[old_num_zones].gameObject.SetActive(true);
            list_zones[old_num_zones].GetComponentInChildren<InputField>().text = new_zone.number_stars_per_zone.ToString();
        }

        //if (levelSet.zoneLevelSearchList.Count >= 10)
        //{
        //    string json = JsonUtility.ToJson(levelSet);
        //    Debug.Log("JSON DA INVIARE PER SALVATAGGIO: " + json);
        //    StartCoroutine(SendPost(json));
        //}


        //int new_size = numberOfZones + 1;
        //Array.Resize(ref levelSet.zoneLevelSearch, new_size);
        //levelSet.zoneLevelSearch[new_size - 1].number = new_size;
        //levelSet.zoneLevelSearch[new_size - 1].number_stars_per_zone = 3;
    }

    public void TaskOnClickDelete()
    {
        if (levelSet.zoneLevelSearchList.Count > 1)
        {
            levelSet.zoneLevelSearchList.RemoveAt(levelSet.zoneLevelSearchList.Count - 1);
        }

    }


    IEnumerator SendPost(string json)
    {
        Debug.Log("entro nella coroutine");
        var request = new UnityWebRequest("http://127.0.0.1:5000/unity/save", "POST");
        byte[] bodyRaw = Encoding.UTF8.GetBytes(json);
        request.uploadHandler = (UploadHandler)new UploadHandlerRaw(bodyRaw);
        request.downloadHandler = (DownloadHandler)new DownloadHandlerBuffer();
        request.SetRequestHeader("Content-Type", "application/json");
        yield return request.SendWebRequest();

        Debug.Log(request.downloadHandler.text);


        if (request.isHttpError || request.isNetworkError || request.downloadHandler.text.Equals("login_unsuccessful!"))
        {
            Debug.Log("questo e' l'errore");


        }

        else
        {

        }
    }
}
