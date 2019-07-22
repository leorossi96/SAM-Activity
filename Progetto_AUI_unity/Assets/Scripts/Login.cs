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
    public PatientData[] patientData;
    public ShowPatient showPatient = new ShowPatient();
    
   


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
        loginData.email = email.GetComponent<InputField>().text;
        loginData.password = password.GetComponent<InputField>().text;

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
