using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fading : MonoBehaviour {

    public bool fading;
    public float fadingRate;
    private Animator animator;

    SmartToy dolphinController; 

    public bool chest_food1_act = false; 



	// Use this for initialization
	void Start () {
        fading = false;
    }
	
	// Update is called once per frame
	void Update () {

        if (fading)
        {
            

            switch (this.tag)
            {
                case "chest_food1":
                    if (GameObject.Find("Dolphin1") != null){
                        if (GameObject.Find("Dolphin1").GetComponent<SmartToy>().rfidsensor.cardReader[3].read)
                        {
                            
                            animator.SetTrigger("eaten");
                            this.enabled = false;

                                
                        }
                        break; 
                    }else {
                        if (Input.GetKeyDown(KeyCode.I))
                        {
                            animator.SetTrigger("eaten");
                            this.enabled = false; 
                        }
                        break; 
                    }


            }
        }
	}

	private void OnCollisionEnter(Collision collision)
	{
        fading = true;
        animator = this.GetComponent<Animator>();
        animator.SetTrigger("conflict"); 
        



    }

	
}
