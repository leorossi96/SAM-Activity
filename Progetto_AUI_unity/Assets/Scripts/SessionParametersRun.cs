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
            HashSet<int> alreadyDisabled = new HashSet<int>();
            if(toDisable>=0){
                for (int i = 0; i < toDisable; i++){

                    ran = Random.Range(0, staticObs.Length);

                    if(  alreadyDisabled.Contains(ran)){
                        Debug.Log("SObstacle " + ran + "is not active.");
                        i--;
                    }else if(staticObs[ran].activeSelf){
                        staticObs[ran].SetActive(false);
                        alreadyDisabled.Add(ran);

                    }else{
                        i--;
                    }
                
                }  

            }

            toDisable = dynObs.Length - levelSet.levelRun[actualLevel].dynamic_obstacle;
            alreadyDisabled.Clear();
            if (toDisable >= 0)
            {
                for (int i = 0; i < toDisable; i++)
                {

                    ran = Random.Range(0, dynObs.Length);

                    if (alreadyDisabled.Contains(ran))
                    {
                        Debug.Log("DObstacle " + ran + "is not active.");
                        i--;
                    }
                    else if(dynObs[ran].activeSelf)
                    {
                        dynObs[ran].SetActive(false);
                        alreadyDisabled.Add(ran);
                    } else {
                        i--;
                    }

                }

            }

            toDisable = powerUps.Length - levelSet.levelRun[actualLevel].power_up;
            alreadyDisabled.Clear();
            if (toDisable >= 0)
            {
                for (int i = 0; i < toDisable; i++)
                {

                    ran = Random.Range(0, powerUps.Length);

                    if (alreadyDisabled.Contains(ran))
                    {
                        Debug.Log("PowerUp " + ran + "is not active.");
                        i--;
                    }
                    else if(powerUps[ran].activeSelf)
                    {
                        powerUps[ran].SetActive(false);
                        alreadyDisabled.Add(ran);
                    }else {
                        i--;
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
