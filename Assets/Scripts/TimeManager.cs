using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeManager : MonoBehaviour
{
    public float StartTime;
    /// <summary>
    /// time in seconds
    /// </summary>
    public float SunSet = 20 * 60 * 60;
    public float SunRise = 6 * 60 * 60;
    public float MidnightTime = 24 * 60 * 60;
    public float SunSetAngle = 185;
    public float SunRiseAngle = -5;
    public float MidnightAngle = -107;
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

    public enum SunStyle                                                                                                      
    {
        SunRotate,
        ExposureLevel
    }
    public SunStyle style;

    float sunYAngleDefault;
    float sunZAngleDefault;
    // Start is called before the first frame update
    void Start()
    {
        CurrentTime = StartTime;
        sunYAngleDefault = TheLight.transform.eulerAngles.y;
        sunZAngleDefault = TheLight.transform.eulerAngles.z;
    }

    // Update is called once per frame
    void Update()
    {
        CurrentTime += TimeStep * 60 * Time.deltaTime;
        CurrentTime %= dayLength;

        if (style == SunStyle.ExposureLevel)
            ExposureLevel();
        else if (style == SunStyle.SunRotate)
            SunRotate();
    }
    void SunRotate()
    {
        if(CurrentTime < SunSet && CurrentTime >= SunRise)
        {
            float slope = (SunSetAngle - SunRiseAngle) / (SunSet - SunRise);
            float currentAngle = CurrentTime * slope + SunRiseAngle - slope * SunRise;
            Debug.Log("currentAngle: " + currentAngle + " slope: " + slope + " timeframe: " + (SunSet - SunRise) + " CurrentTime: " + CurrentTime);
            TheLight.transform.localEulerAngles = new Vector3(currentAngle, sunYAngleDefault, sunZAngleDefault);
        }
        else if (CurrentTime < MidnightTime && CurrentTime >= SunSet)
        {
            float slope = loopLength(SunSetAngle, SunRiseAngle, 360) / loopLength(SunSet, SunRise, 86400);
            float currentAngle = CurrentTime * slope + SunSetAngle - slope * SunSet;
            Debug.Log("currentAngle: " + currentAngle + " slope: " + slope + " timeframe: " + loopLength(SunSet, SunRise, 86400) +  " CurrentTime: " + CurrentTime);
            TheLight.transform.localEulerAngles = new Vector3(currentAngle, sunYAngleDefault, sunZAngleDefault);
        }
        else
        {
            float slope = loopLength(SunSetAngle, SunRiseAngle, 360) / loopLength(SunSet, SunRise, 86400);
            float currentAngle = CurrentTime * slope + MidnightAngle;
            Debug.Log("currentAngle: " + currentAngle + " slope: " + slope + " timeframe: " + (SunRise - MidnightTime) + " CurrentTime: " + CurrentTime);
            TheLight.transform.localEulerAngles = new Vector3(currentAngle, sunYAngleDefault, sunZAngleDefault);
        }
        
    }
    float loopLength(float start, float end, float size)
    {
        if (start < end)
        {
            return end - start;
        }
        else
        {
            return size - start + end; //360 - 185 + -5 = 170
        }
    }

    void ExposureLevel()
    {
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
