using UnityEngine;
using System.Collections;

public class TorchFlickerTest : MonoBehaviour
{

    public float flickerTime = 0;
    public float flickerThreshold = 5f;

    private Light lightComponent;

    void Start()
    {
        lightComponent = GetComponent<Light>();
    }

    // Update is called once per frame
    void Update()
    {
        flickerTime += Random.Range(1, 2);
        if (flickerTime > flickerThreshold)
        {
            if (lightComponent.intensity <= 0.22f)
            {
                lightComponent.intensity = 0.25f;
            }
            else
            {
                lightComponent.intensity = 0.2f;
            }
            flickerTime = 0;
        }
    }
}
