using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SessionParametersRun : MonoBehaviour {

    public LevelSet levelSet = null;
    public GameObject[] staticObs;
    public GameObject[] dynObs;
    public GameObject[] powerUps;
    public PlayerCollision lifes;
    public PlayerMovement movement;
    public int actualLevel;






    // Use this for initialization
	void Awake () {
        actualLevel = 1;
        levelSet = GameObject.Find("LevelSet").GetComponent<LevelSet>();
        if(levelSet!=null){
            int ran = 0;
            Debug.Log("Level set diverso da null");
            Debug.Log("ACTUAL LEVEL BEGIN: " + actualLevel);

            int toDisable = staticObs.Length - levelSet.levelRun[actualLevel].static_obstacle;
            if(toDisable>=0){
                for (int i = 0; i < toDisable; i++){

                    ran = Random.Range(0, staticObs.Length);

                    if(!staticObs[ran].activeSelf){
                        Debug.Log("SObstacle " + ran + "is not active.");
                        i--;
                    }else{
                        staticObs[ran].SetActive(false);
                    }
                
                }  

            }

            toDisable = dynObs.Length - levelSet.levelRun[actualLevel].dynamic_obstacle;
            if (toDisable >= 0)
            {
                for (int i = 0; i < toDisable; i++)
                {

                    ran = Random.Range(0, dynObs.Length);

                    if (!dynObs[ran].activeSelf)
                    {
                        Debug.Log("DObstacle " + ran + "is not active.");
                        i--;
                    }
                    else
                    {
                        dynObs[ran].SetActive(false);
                    }

                }

            }

            toDisable = powerUps.Length - levelSet.levelRun[actualLevel].power_up;
            if (toDisable >= 0)
            {
                for (int i = 0; i < toDisable; i++)
                {

                    ran = Random.Range(0, powerUps.Length);

                    if (!powerUps[ran].activeSelf)
                    {
                        Debug.Log("PowerUp " + ran + "is not active.");
                        i--;
                    }
                    else
                    {
                        powerUps[ran].SetActive(false);
                    }

                }

            }

            lifes.lifeCount = levelSet.levelRun[actualLevel].lives;
            lifes.max_time = levelSet.levelRun[actualLevel].max_time;
            Debug.Log("ACTUAL LEVEL: " + actualLevel);
            Debug.Log("MAX TIME RUN: " + lifes.max_time);

        }

	}
	
	
}
