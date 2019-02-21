using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VolumeChange : MonoBehaviour {


    public Slider slider;
    public AudioSource myMusic;
	
	// Update is called once per frame
	void Update () {
        myMusic.volume = slider.value;
	}
}
