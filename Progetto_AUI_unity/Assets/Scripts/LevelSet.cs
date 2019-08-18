using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using System.Text;

public class LevelSet : MonoBehaviour {

    public LevelRun[] levelRun;
    public LevelSearch[] levelSearch;
    public ZoneLevelSearch[] zoneLevelSearch;
    

    public void StartCoroutine(Login login)
    {
        Debug.Log("LOGIN.SELECTED PATIENT: " + login.selectedPatient);
        string json = JsonUtility.ToJson(login.selectedPatient);
        Debug.Log("String json awake" + json);
        StartCoroutine(SendLevelPostRun(json));
        StartCoroutine(SendLevelPostSearch(json));
    }

    IEnumerator SendLevelPostRun(string json)
    {
        Debug.Log("entro nella coroutine");
        var request = new UnityWebRequest("http://127.0.0.1:5000/prova/levels/run", "POST");
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
        levelRun = JsonHelper.getJsonArray<LevelRun>(jsonString);
        Debug.Log(levelRun[0].name);
    }

    IEnumerator SendLevelPostSearch(string json)
    {
        Debug.Log("entro nella coroutine");
        var request = new UnityWebRequest("http://127.0.0.1:5000/prova/levels/search", "POST");
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
        levelSearch = JsonHelper.getJsonArray<LevelSearch>(jsonString);
        Debug.Log(levelSearch[0].id);
        string json_z = JsonUtility.ToJson(levelSearch[0]);
        Debug.Log("String json ZONES COROUTINE" + json_z);
        StartCoroutine(SendLevelPostZones(json_z));
    }

    IEnumerator SendLevelPostZones(string json_z)
    {
        Debug.Log("entro nella coroutine");
        var request = new UnityWebRequest("http://127.0.0.1:5000/prova/levels/search/zones", "POST");
        byte[] bodyRaw = Encoding.UTF8.GetBytes(json_z);
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
        zoneLevelSearch = JsonHelper.getJsonArray<ZoneLevelSearch>(jsonString);
        Debug.Log(zoneLevelSearch[0].number);
    }


}
