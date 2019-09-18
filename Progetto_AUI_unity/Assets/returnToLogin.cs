using System.Collections;
using System.Text;
using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.IO;
using UnityEngine.Networking;
using SimpleJSON;
using TMPro;

public class returnToLogin : MonoBehaviour
{

    public ScrollRect scrollView;
    public GameObject scrollContent;
    public GameObject buttonPrefab;
    public GameObject show;
    public Button backLoginButton;
    public LevelSet levelSet = null;
    public PatientData[] patientData;
    public GameObject playModeMenu;
    public PatientData selectedPatient = new PatientData();
    public bool sameScene = false;


    // Use this for initialization
    void Start()
    {
        backLoginButton.onClick.AddListener(TaskOnClick);
        levelSet = GameObject.Find("LevelSet").GetComponent<LevelSet>();

    }

    public void TaskOnClick()
    {
        string json = JsonUtility.ToJson(levelSet.loginData);
        Debug.Log("TaskOnClick: -> " + json);
        StartCoroutine(SendPost(json));
    }


    void ClickAction(int i)
    {

        PatientData selected = patientData[i];
        //
        selectedPatient.last_name = selected.last_name;
        selectedPatient.first_name = selected.first_name;
        selectedPatient.date_of_birth = selected.date_of_birth;
        selectedPatient.comment = selected.comment;
        selectedPatient.id = selected.id;
        selectedPatient.type_of_disability = selected.type_of_disability;
        selectedPatient.user_id = selected.user_id;
        Debug.Log("sono dentro click action" + selectedPatient.id);
        levelSet.patient_last_name = selected.last_name;
        levelSet.patient_first_name = selected.first_name;
        levelSet.StartCoroutine(this);
        show.SetActive(false);
        playModeMenu.SetActive(true);

    }

    // Update is called once per frame
    void Update()
    {

    }


    IEnumerator SendPost(string json)
    {
        Debug.Log("entro nella coroutine");
        var request = new UnityWebRequest("http://127.0.0.1:5000/login/unity", "POST");
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


            string jsonString = request.downloadHandler.text;
            /* data = JSON.Parse(jsonString);
            string last_name = data[0]["last_name"].Value;

        Debug.Log(last_name);*/
            //Debug.Log("prima di utility");
            patientData = JsonHelper.getJsonArray<PatientData>(jsonString);
            //Debug.Log("Dopo Utility");
            Debug.Log("PATIENT DATA LENGTH: " + patientData.Length);
            //Debug.Log(patientData[0].last_name);
            //Debug.Log(patientData.Length);
            playModeMenu.SetActive(false);
            show.SetActive(true);

            if (sameScene == false)
            {
                GameObject[] button = new GameObject[patientData.Length];

                for (int i = 0; i < patientData.Length; i++)
                {
                    int temp = i;
                    button[i] = Instantiate(buttonPrefab);
                    button[i].transform.SetParent(scrollContent.transform, false);//Setting button parent
                    button[i].GetComponent<Button>().onClick.AddListener(() => ClickAction(temp));//Setting what button does when clicked                                                   //Next line assumes button has child with text as first gameobject like button created from GameObject->UI->Button
                    button[i].transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = patientData[i].last_name + ' ' + patientData[i].first_name;
                    Debug.Log("PATIENT " + i + ": LAST_NAME: " + patientData[i].last_name + ": FIRST_NAME: " + patientData[i].first_name);
                }
                scrollView.verticalNormalizedPosition = 1;

            }
            sameScene = true;
            //showPatient.setButton();
            /*for (int i = 0; i < patientData.Length; i++)
            {
                patientLastName.text = patientData[i].last_name;
                patientFirstName.text = patientData[i].first_name;

                Debug.Log("sono nel for " + patientLastName.text + patientFirstName);
            }*/

        }


    }

}