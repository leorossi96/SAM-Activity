﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SessionParameters : MonoBehaviour {

    //parameters to detect execution time
    public float sec;
    public int min;
    public int hrs;
    public bool stopChrono;

    //parameters to compute heatmap
    public Vector2 pos;
    public ArrayList posArray;
    public float interval;
    public float timer = 0;

    //stop collecting parameters
    public bool endGame = false;
    public int heatmapCount = 1; //per evitare che l'update faccia calolare l'heatmap più di una volta

    //marker per la posizione
    public GameObject prefab;

    //Array delle possibili posizioni delle zone di search
    public int zoneCount;
    public int collectiblesPerZoneCount;

    public GameObject collectiblePrefab;
    public GameObject zonePrefab;

    public Vector3[] zonePositions = new[] { new Vector3(241.1f, 3.110005f, 156.4f), new Vector3(160.3f, 3.110005f, 121.4f), new Vector3(408.3f, 3.110005f, 127.4f), new Vector3(121.7f, 3.110005f, 367.7f), new Vector3(121.7f, 3.110005f, 213.7f), new Vector3(313.1f, 3.110005f, 125f), new Vector3(300.7f, 3.110005f, 267.7f), new Vector3(241.7f, 3.110005f, 374.7f), new Vector3(136.6f, 3.110005f, 295.2f), new Vector3(154.8f, 3.110005f, 114.7f) };
    public HashSet<int> zonePositionIndexes;

	// Use this for initialization
	void Start () {
        sec = 0;
        stopChrono = false;
        posArray = new ArrayList();

        zonePositionIndexes = new HashSet<int>();

        for (int i = 0; i < zoneCount; i++){
            int ran = (int)Random.Range(0, 9);
            Debug.Log("LEVEL GENERATION ran : " + ran);
            Debug.Log("Già uscito : "+ zonePositionIndexes.Contains(ran));
            if (zonePositionIndexes.Contains(ran)){
                i--;
            }
            else{
                Vector3 position = zonePositions[ran];
                GameObject zoneInstantiated = Instantiate(zonePrefab, position, Quaternion.identity);
                PopulateZone(zoneInstantiated, collectiblesPerZoneCount);
            }
        }

    }
	
	// Update is called once per frame
	void Update () {
        //update del bool endGame
        if (Input.anyKey && Input.GetKey(KeyCode.E) && !endGame)
        {
            endGame = true;
            SetStopChrono(true);
        }
        if(!endGame)
        {
            if (!stopChrono)
                UpdateChrono();

            timer += Time.deltaTime;
            if (timer >= interval)
            {
                posArray = StorePosition();
                timer = 0;
            }
        }
        else //se è stato premuto E
        {
            if (heatmapCount == 1){
                heatmapCount -= 1;
                GenerateHeatmap(posArray);
            }
        }
	}

    void GenerateHeatmap(ArrayList a){
        foreach(Vector2 v in a){
            Instantiate(prefab, new Vector3(v.x, 90f, v.y), Quaternion.identity);
        }
    }

    ArrayList StorePosition(){
        pos = new Vector2(this.transform.position.x, this.transform.position.z);
        posArray.Add(pos);
        Debug.Log("Adding :" + pos.ToString());
        return posArray;
    }
    

    void UpdateChrono(){
        sec += Time.deltaTime;
        sec = (float)decimal.Round((decimal)sec, 2);
        if (sec >= 60)
        {
            min++;
            sec = 0;
        }
        if (min >= 60)
        {
            hrs++;
            min = 0;
        }
        //Debug.Log("CHRONO " + hrs.ToString() + " : " + min.ToString() + " : " + sec.ToString());
    }

    public string GetChrono(){
        string chrono =  hrs.ToString() + " : " + min.ToString() + " : " + sec.ToString();
        return chrono;
    }

    public void SetStopChrono(bool b){
        stopChrono = b;
    }

    private void PopulateZone(GameObject zone, int nCollectibles){
        for (int i = 0; i < nCollectibles; i++)
        {
            float randomX = Random.Range(-25f, 25f);
            float randomZ = Random.Range(-25f, 25f);
            GameObject collectibleInstantiated = Instantiate(collectiblePrefab, new Vector3(0f, -4.2f, 0f), Quaternion.identity, zone.transform);
            collectibleInstantiated.transform.SetParent(zone.transform);
            collectibleInstantiated.transform.localPosition = new Vector3(randomX, -4.2f, randomZ);
            collectibleInstantiated.transform.Rotate(-90f, 0, 0, Space.World);
        }
    }

}