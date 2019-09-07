using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomVerticalSearch : MonoBehaviour {

    private Transform[] children;
    private float[] distances;
    private Vector3[] targetPos;

	// Use this for initialization
	void Start () {



        children = GetComponentsInChildren<Transform>();
        distances = new float[children.Length];
        targetPos = new Vector3[children.Length];

        for (int i = 0; i < distances.Length; i++){
            distances[i] = UnityEngine.Random.RandomRange(2.0f, 4.0f);
            targetPos[i] = distances[i] * this.transform.up + children[i].localPosition;
        }

	}
	
	// Update is called once per frame
	void Update () {



        for (int i = 0; i < children.Length;i++){

            if (Vector3.Distance(children[i].localPosition, targetPos[i]) < 0.01f){
                distances[i] = -distances[i];
                targetPos[i] = distances[i] * this.transform.up + children[i].localPosition;
            }
               


            children[i].localPosition = Vector3.MoveTowards(children[i].localPosition, targetPos[i], 0.2f);

        }

	}
}
