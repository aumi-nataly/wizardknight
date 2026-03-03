using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class FlickerLight : MonoBehaviour
{
    private Light2D light;
    private float baseIntensive;

    private void Awake()
    {
        light = GetComponent<Light2D>();
        baseIntensive = light.intensity;
    }

   
    void Update()
    {
        //изменение интенсивности от 0.9 до 1.1 плавно
        light.intensity = baseIntensive + Mathf.Sin(Time.time * 8f) * 0.1f;
    }
}
