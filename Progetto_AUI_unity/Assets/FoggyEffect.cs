/*using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoggyEffect : MonoBehaviour {

    public Color normalColor;
    public Color underwaterColor;

    public bool isUnderwater;

    public float fogDensityNormal = 0.01f;
    public float fogDensityUnderwater = 0.1f;


    public Transform waterSurface;

    void Start()
    {
        normalColor = new Color(0.5f, 0.5f, 0.5f, 0.5f);
        underwaterColor = new Color(0.22f, 0.65f, 0.77f, 0.5f);
    }

    // Update is called once per frame
    void Update()
    {
        if ((transform.position.y < waterSurface.transform.position.y) != isUnderwater)
        {
            isUnderwater = transform.position.y < waterSurface.transform.position.y;
            if (isUnderwater) SetUnderwater();
            if (!isUnderwater) SetNormal();
        }
    }

    void SetNormal()
    {
        RenderSettings.fogColor = normalColor;
        RenderSettings.fogDensity = fogDensityNormal;

    }

    void SetUnderwater()
    {
        RenderSettings.fogColor = underwaterColor;
        RenderSettings.fogDensity = fogDensityUnderwater; 

    }
}
*/