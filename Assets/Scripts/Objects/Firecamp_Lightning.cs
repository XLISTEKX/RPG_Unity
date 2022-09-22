using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class Firecamp_Lightning : MonoBehaviour
{
    public float timeToChange;
    public Light2D firecampLight;

    float randomValue;

    private void Start()
    {
        randomValue = Random.Range(0.6f, 0.8f);
        InvokeRepeating("randValue", timeToChange, timeToChange);
    }
    private void Update()
    {
        firecampLight.intensity = Mathf.Lerp(firecampLight.intensity, randomValue, timeToChange);
    }

    private void randValue()
    {
        randomValue = Random.Range(0.6f, 0.8f);
    }
}
