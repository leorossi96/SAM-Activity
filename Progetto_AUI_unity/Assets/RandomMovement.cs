using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class RandomMovement : MonoBehaviour {

    NavMeshAgent navMeshAgent;

    public float timeForNewPath;

	// Use this for initialization
	void Start () {
    
	}

    public Vector3 getRandomPosition (){

        float x = Random.range(0, 490);
        float y = Random.range(5, 10);
        float z = Random.range(3, 490);

        return new Vector3(x, y, z);
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}