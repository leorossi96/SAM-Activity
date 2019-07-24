using System.Collections;
using System.Text;
using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.IO;
using UnityEngine.Networking;
using SimpleJSON;


public class Login : MonoBehaviour
{


    public GameObject email;
    public GameObject password;
    public Button loginButton;
    public LoginData loginData;
    public GameObject loginMenu;
    public GameObject show;
    public GameObject patientMenu;
    public PatientData[] patientData;
   // public ShowPatient showPatient = new ShowPatient();
    public GameObject buttonPrefab;
    //public GameObject panelToAttach;
    public ScrollRect scrollView;
    public GameObject scrollContent;
    public PatientData selectedPatient = new PatientData();

    
    
   


    // Use this for initialization
    void Start()
    {
        loginData = new LoginData();
        loginButton.onClick.AddListener(TaskOnClick);

    }

    public void TaskOnClick()
    {
        string json = JsonUtility.ToJson(loginData);
        Debug.Log(json);
        StartCoroutine(SendPost(json));
        //UnityWebRequest request = UnityWebRequest.Put(url, jsonString);
        //request.SetRequestHeader("Content-Type", "application/json");
        //yield return request.Send();
    }



    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Tab))
        {
            if (email.GetComponent<InputField>().isFocused)
            {
                password.GetComponent<InputField>().Select();
            }
            if (password.GetComponent<InputField>().isFocused)
            {
                email.GetComponent<InputField>().Select();
            }
        }
        loginData.email = email.GetComponent<InputField>().text;
        loginData.password = password.GetComponent<InputField>().text;

    }


    void ClickAction(int i)
    {
        Debug.Log("sono dentro click action");
        PatientData selected = patientData[i];
        show.SetActive(false);
        patientMenu.SetActive(true);
        selectedPatient.last_name = selected.last_name;
        selectedPatient.first_name = selected.first_name;
        selectedPatient.date_of_birth = selected.date_of_birth;
        selectedPatient.comment = selected.comment;
        selectedPatient.id = selected.id;
        selectedPatient.type_of_disability = selected.type_of_disability;
        selectedPatient.user_id = selected.user_id;


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


        if (request.isHttpError || request.isNetworkError)
        {
            Debug.Log("questo e' l'errore" + request.error);
        }


        string jsonString = request.downloadHandler.text;
        /* data = JSON.Parse(jsonString);
        string last_name = data[0]["last_name"].Value;
        
    Debug.Log(last_name);*/
        Debug.Log("prima di utility");
        patientData = JsonHelper.getJsonArray<PatientData>(jsonString);
        Debug.Log("Dopo Utility");

        Debug.Log(patientData[0].last_name);
        Debug.Log(patientData.Length);
        loginMenu.SetActive(false);
        show.SetActive(true);

        GameObject[] button = new GameObject[patientData.Length];

        for (int i = 0; i < patientData.Length; i++)
        {
            int temp = i;
            button[i] = Instantiate(buttonPrefab);
            button[i].transform.SetParent(scrollContent.transform, false);//Setting button parent
            button[i].GetComponent<Button>().onClick.AddListener(() => ClickAction(temp));//Setting what button does when clicked                                                   //Next line assumes button has child with text as first gameobject like button created from GameObject->UI->Button
            button[i].transform.GetChild(0).GetComponent<Text>().text = patientData[i].last_name + ' ' + patientData[i].first_name;
        }
        //showPatient.setButton();
        /*for (int i = 0; i < patientData.Length; i++)
        {
            patientLastName.text = patientData[i].last_name;
            patientFirstName.text = patientData[i].first_name;
            
            Debug.Log("sono nel for " + patientLastName.text + patientFirstName);
        }*/


        /*
        Debug.Log("prima di utility");
        pat = JsonUtility.FromJson<Patients>(jsonString);
        Debug.Log("questa è pat: " + pat);
        Debug.Log("Dopo Utility");

        Debug.Log(pat);
        */


        /*UnityWebRequest www = UnityWebRequest.Put("http://127.0.0.1:5000/login/unity", json);
        www.SetRequestHeader("Content-Type", "application/json");
        yield return www.SendWebRequest();
        if (www.isNetworkError || www.isHttpError)
        {
            Debug.Log(www.error);
        }
        else
        {
            // Show results as text
            Debug.Log("Risposta da Flask" + www.downloadHandler.text);

            // Or retrieve results as binary data
            byte[] results = www.downloadHandler.data;*/

    }
}
