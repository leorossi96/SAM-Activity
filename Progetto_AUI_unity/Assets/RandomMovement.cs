using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class RandomMovement : MonoBehaviour {

    NavMeshAgent navMeshAgent;

    public float timeForNewPath;

    bool inCoroutine;

	// Use this for initialization
	void Start () {
    
	}

    // Update is called once per frame
    void Update()
    {
        if (!inCoroutine)
            DoSomething();

    }



    public Vector3 GetRandomPosition (){

        float x = UnityEngine.Random.Range(0, 490);
        float y = UnityEngine.Random.Range(5, 10);
        float z = UnityEngine.Random.Range(3, 490);

        return new Vector3(x, y, z);
    }
	
    IEnumerator DoSomething(){
        inCoroutine = true;
        yield return new WaitForSeconds(timeForNewPath);
        GetNewPath();
        inCoroutine = false;

    }

    private void GetNewPath()
    {
        navMeshAgent.SetDestination(GetRandomPosition());
    }
}