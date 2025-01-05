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
    void Start()
    {
        playerScript = GetComponent<player>();
    }

    // Update is called once per frame
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
                    Vector3 lightDirection = -light.transform.forward; // Kierunek œwiat³a jest przeciwny do jego "forward"
                    foreach (var origin in origins)
                    {
                        // Wykonujemy raycast w kierunku œwiat³a
                        if (Physics.Raycast(origin.position, lightDirection, out RaycastHit hit))
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
                        if (Physics.Raycast(origin.position, light.transform.position, out RaycastHit hit, Vector3.Distance(origin.position, light.transform.position)))
                        {
                            hitCount++;
                            Debug.DrawRay(origin.position, light.transform.position, Color.green);

                        }
                        else
                        {
                            Debug.DrawRay(origin.position, light.transform.position, Color.red);

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

        //percentage = (hitCount / (lightCount * (float)origins.Length)) * 100;
        //Debug.Log(percentage);
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
