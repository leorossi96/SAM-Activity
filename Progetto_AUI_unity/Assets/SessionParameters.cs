using System.Collections;
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

	// Use this for initialization
	void Start () {
        sec = 0;
        stopChrono = false;
        posArray = new ArrayList();
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
        Debug.Log(posArray.Count);
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
}
