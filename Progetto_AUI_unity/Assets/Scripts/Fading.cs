using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fading : MonoBehaviour {

    public bool fading;
    public float fadingRate;
    private Animator animator; 



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
                    if (Input.GetKeyDown(KeyCode.I))
                    {
                        animator.SetTrigger("eaten"); 
                    }
                    break; 
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
