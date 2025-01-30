using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;

public class ShadowDetection : MonoBehaviour
{
    public Light[] lights;
    public Transform[] origins;
    private player playerScript;
    public int firstStagePercentage = 90;
    public int secondStagePercentage = 70;
    public int thirdStagePercentage = 50;
    public int fourthStagePercentage = 30;
    public int fifthStagePercentage = 1;

    public LayerMask ignoreGroundLayer; // Dodana zmienna LayerMask

    void Start()
    {
        playerScript = GetComponent<player>();
    }

    void Update()
    {
        float lightCount = 0;
        float percentage = 100;

        lights = Object.FindObjectsByType<Light>(FindObjectsSortMode.None);
        foreach (var light in lights)
        {
            if (light.shadows != LightShadows.None)
            {
                lightCount++;
                float hitCount = 0;
                if (light.type == UnityEngine.LightType.Directional)
                {
                    Vector3 lightDirection = -light.transform.forward;
                    foreach (var origin in origins)
                    {
                        // Dodano layerMask do raycast
                        if (Physics.Raycast(origin.position, lightDirection, out RaycastHit hit, Mathf.Infinity, ~ignoreGroundLayer))
                        {
                            hitCount++;
                            Debug.DrawRay(origin.position, lightDirection * 100, Color.green);
                        }
                        else
                        {
                            Debug.DrawRay(origin.position, lightDirection * 100, Color.red);
                        }
                    }

                }
                else
                {
                    foreach (var origin in origins)
                    {
                        // Dodano layerMask do raycast
                        if (Physics.Raycast(origin.position, (light.transform.position - origin.position), out RaycastHit hit, Vector3.Distance(origin.position, light.transform.position), ~ignoreGroundLayer))
                        {
                            hitCount++;
                            Debug.DrawRay(origin.position, (light.transform.position - origin.position), Color.green);

                        }
                        else
                        {
                            Debug.DrawRay(origin.position, (light.transform.position - origin.position), Color.red);
                        }

                    }
                }
                float tymPercentage = (hitCount / (float)origins.Length) * 100;
                if (percentage > tymPercentage)
                {
                    percentage = tymPercentage;
                }

            }
        }

        if (percentage > firstStagePercentage)
        {
            playerScript.shadowExposure = 0;
        }
        else if (percentage > secondStagePercentage)
        {
            playerScript.shadowExposure = 1;
        }
        else if (percentage > thirdStagePercentage)
        {
            playerScript.shadowExposure = 2;
        }
        else if (percentage > fourthStagePercentage)
        {
            playerScript.shadowExposure = 3;
        }
        else if (percentage > fifthStagePercentage)
        {
            playerScript.shadowExposure = 4;
        }
        else
        {
            playerScript.shadowExposure = 5;
        }
    }
}