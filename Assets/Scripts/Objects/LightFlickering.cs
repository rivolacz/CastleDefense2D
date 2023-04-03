using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.Rendering.Universal;

[RequireComponent(typeof(Light2D))]
public class LightFlickering : MonoBehaviour
{
    private new Light2D light;
    public float minIntensity = 0.5f;
    public float maxIntensity = 1.0f;
    public float minFlickerSpeed = 0.1f;
    public float maxFlickerSpeed = 0.3f;

    private IEnumerator flickerCoroutine;

    private void Awake()
    {
        light = GetComponent<Light2D>();
    }

    private void OnEnable()
    {
        flickerCoroutine = Flicker();
        StartCoroutine(flickerCoroutine);
    }

    private void OnDisable()
    {
        StopCoroutine(flickerCoroutine);
    }

    private IEnumerator Flicker()
    {
        while (true)
        {
            light.intensity = Random.Range(minIntensity, maxIntensity);
            float waitTime = Random.Range(minFlickerSpeed, maxFlickerSpeed);
            yield return new WaitForSeconds(waitTime);
        }
    }
}
