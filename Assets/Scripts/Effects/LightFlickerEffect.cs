using System.Collections;
using UnityEngine;

public class LightFlickerEffect : MonoBehaviour
{
    [SerializeField] float minInterval = 0.01f;
    [SerializeField] float maxInterval = 0.2f;
    
    float flickerInterval;
    new Light light;

    void Start()
    {
        light = GetComponentInChildren<Light>();
        StartCoroutine(FlickerEffect());
    }

    IEnumerator FlickerEffect()
    {
        while (true)
        {
            light.enabled = false;
            flickerInterval = Random.Range(minInterval, maxInterval);
            yield return new WaitForSeconds(flickerInterval);

            light.enabled = true;
            flickerInterval = Random.Range(minInterval, maxInterval);
            yield return new WaitForSeconds(flickerInterval);
        }
    }
}