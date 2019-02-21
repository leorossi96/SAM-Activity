using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class RandomMovement : MonoBehaviour {

    NavMeshAgent navMeshAgent;

    public float timeForNewPath;

    bool inCoroutine;

    public Vector3 newpos; 

	// Use this for initialization
	void Start () {
        
        navMeshAgent = GetComponent<NavMeshAgent>();
        GetNewPath(); 
        inCoroutine = false;
	}

    // Update is called once per frame
    void Update()
    {
        
        if(!inCoroutine){
            StartCoroutine("DoSomething");

        }
            
    }



    public Vector3 GetRandomPosition (){

        float x = UnityEngine.Random.Range(0, 490);

        float y = UnityEngine.Random.Range(5, 10);
        float z = UnityEngine.Random.Range(3, 490);

        return new Vector3(x, y, z);
    }
	
    IEnumerator DoSomething(){
        
        this.inCoroutine = true;
        yield return new WaitForSeconds(timeForNewPath);
        GetNewPath();
        this.inCoroutine = false;

    }

    private void GetNewPath()
    {   
        this.newpos = GetRandomPosition();
        navMeshAgent.SetDestination(newpos);
        
    }
}