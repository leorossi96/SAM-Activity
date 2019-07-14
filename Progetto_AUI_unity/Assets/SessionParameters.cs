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

	// Use this for initialization
	void Start () {
        sec = 0;
        stopChrono = false;
        posArray = new ArrayList();
	}
	
	// Update is called once per frame
	void Update () {
        if (!stopChrono)
            UpdateChrono();

        timer += Time.deltaTime;
        if (timer >= interval)
        {
            posArray = StorePosition();
            timer = 0;
        }
	}

    ArrayList StorePosition(){
        pos = new Vector2(this.transform.position.x, this.transform.position.z);
        posArray.Add(pos);
        Debug.Log(posArray);
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
