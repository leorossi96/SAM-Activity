using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenChestSearch : MonoBehaviour {

    private Animator animator;
    bool opened = false;

    // Use this for initialization
    void Start () {
    }
	
	// Update is called once per frame
	void Update () {
        if (!opened)
        {
            animator = this.GetComponent<Animator>();
            animator.SetTrigger("conflict");
            opened = true;
        }
    }
}
