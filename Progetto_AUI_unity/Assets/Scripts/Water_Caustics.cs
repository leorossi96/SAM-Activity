using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Water_Caustics : MonoBehaviour {

    public Texture2D[] frames;
    public Projector projector;

    public float rate = 20.0f; 
    private int frameindex; 

	// Use this for initialization
	void Start () {

        projector = GetComponent<Projector>();
        frameindex = 0;
        nextFrame();
        InvokeRepeating("nextFrame", 1 / rate, 1 / rate); 
	}
	
	// Update is called once per frame
	void nextFrame () {

        projector.material.SetTexture("_ShadowTex", frames[frameindex]);

        frameindex = (frameindex + 1) % frames.Length;
        Debug.Log(frameindex); 
	}
}
