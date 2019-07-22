using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

public class activateParticle : MonoBehaviour {

    public Animator anim;
    private bool notstarted;
    public PlayerMovement movement;
    public Canvas canvas;


    private IEnumerator stopAfterAWHile(){
        yield return new WaitForSeconds(1.0f);
        this.GetComponent<ParticleSystem>().enableEmission = false;
        Image[] images = canvas.GetComponentsInChildren<Image>();
        for (int i = 0; i < images.Length; i++)
        {
            if (images[i].name == "chest_food1")
            {
                images[i].GetComponent<Image>().enabled = false;
            }
        }

        movement.powerUp("chest_food1");

        movement.start = true;
        movement.enabled = true; 
        this.enabled = false; 

    }



	private void Start()

	{
        notstarted = true;
        this.GetComponent<ParticleSystem>().enableEmission= false;
	}

	// Update is called once per frame
	void Update () {
        if(anim.GetCurrentAnimatorStateInfo(0).IsName("chest_disappearing")&&notstarted){

            this.GetComponent<ParticleSystem>().enableEmission = true;
            notstarted = false;
            StartCoroutine(stopAfterAWHile()); 
        }
	}
}
