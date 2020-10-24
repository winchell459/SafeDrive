using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class TimeManager : MonoBehaviour
{
    public float StartTime;
    /// <summary>
    /// time in seconds
    /// </summary>
    public float SunSet = 20 * 60 * 60;
    public float SunRise = 6 * 60 * 60;
    private float dayLength = 24 * 60 * 60;
    public float CurrentTime;
    public float TimeStep = 1; //game minutes per real seconds

    public Color DarkestTint = Color.black;
    public Color BrightestTint = Color.white;
    public float DarkestExposure = 0;
    public float BrightestExposure = 5;

    public Light TheLight;
    public float MaxIntensity = 1;
    public float MinIntensity = 0;

    // Start is called before the first frame update
    void Start()
    {
        CurrentTime = StartTime;
    }

    // Update is called once per frame
    void Update()
    {
        CurrentTime += TimeStep * 60 * Time.deltaTime;
        CurrentTime %= dayLength;

        if (CurrentTime > 12 * 60 * 60)
        {
            float l0 = SunSet - (12 * 60 * 60);
            float l1 = SunSet - CurrentTime;
            float h0 = BrightestExposure - DarkestExposure;
            float h1 = l1 * h0 / l0;
            //RenderSettings.skybox.shader.exp
            //Debug.Log(h1 - DarkestExposure);
            RenderSettings.skybox.SetFloat("_Exposure", h1);

            float hr0 = BrightestTint.r - DarkestTint.r;
            float hg0 = BrightestTint.g - DarkestTint.g;
            float hb0 = BrightestTint.b - DarkestTint.b;

            float hr1 = Mathf.Clamp(DarkestTint.r + l1 * hr0 / l0, 0, 1);
            float hg1 = Mathf.Clamp(DarkestTint.g + l1 * hg0 / l0, 0, 1);
            float hb1 = Mathf.Clamp(DarkestTint.b + l1 * hb0 / l0, 0, 1);

            RenderSettings.skybox.SetColor("_Tint", new Color(hr1, hg1, hb1));

            float hl0 = MaxIntensity - MinIntensity;
            float hl1 = Mathf.Clamp(MinIntensity + l1 * hl0 / l0, 0, MaxIntensity);
            TheLight.intensity = hl1;
        }
        else
        {
            float l0 = (12 * 60 * 60) - SunRise;
            float l1 = CurrentTime - SunRise;
            float h0 = BrightestExposure - DarkestExposure;
            float h1 = l1 * h0 / l0;
            //RenderSettings.skybox.shader.exp
            //Debug.Log(h1 - DarkestExposure);
            RenderSettings.skybox.SetFloat("_Exposure", h1);

            float hr0 = BrightestTint.r - DarkestTint.r;
            float hg0 = BrightestTint.g - DarkestTint.g;
            float hb0 = BrightestTint.b - DarkestTint.b;

            float hr1 = Mathf.Clamp(DarkestTint.r + l1 * hr0 / l0, 0, 1);
            float hg1 = Mathf.Clamp(DarkestTint.g + l1 * hg0 / l0, 0, 1);
            float hb1 = Mathf.Clamp(DarkestTint.b + l1 * hb0 / l0, 0, 1);

            RenderSettings.skybox.SetColor("_Tint", new Color(hr1, hg1, hb1));

            float hl0 = MaxIntensity - MinIntensity;
            float hl1 = Mathf.Clamp(MinIntensity + l1 * hl0 / l0, 0, MaxIntensity);
            TheLight.intensity = hl1;
        }
    }
}
