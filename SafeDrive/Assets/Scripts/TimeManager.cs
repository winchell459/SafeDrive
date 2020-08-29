using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeManager : MonoBehaviour
{
    public float StartTime = 12 * 60 * 60;
    public float SunSet = 20 * 60 * 60;
    public float SunRise = 6 * 60 * 60;
    public float CurrentTime;
    public float TimeStep = 1; //game minutes per real seconds

    public Color DarkestTint = Color.black;
    public Color BrighestTint = Color.white;
    public float DarkestExposure = 0;
    public float BrighestExposure = 5;

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

        if(CurrentTime > 12 * 60 * 60)
        {
            float l0 = (SunSet - 12 * 60 * 60);
            float l1 = (SunSet - CurrentTime);
            float h0 = BrighestExposure - DarkestExposure;
            float h1 = DarkestExposure + l1 * h0 / l0;
            //RenderSettings.skybox.shader.exp
            Debug.Log(h1);
            RenderSettings.skybox.SetFloat("_Exposure", h1);

            float hr0 = BrighestTint.r - DarkestTint.r;
            float hb0 = BrighestTint.b - DarkestTint.b;
            float hg0 = BrighestTint.g - DarkestTint.g;

            float hr1 = Mathf.Clamp(DarkestTint.r + l1 * hr0 / l0, 0, 1);
            float hg1 = Mathf.Clamp(DarkestTint.g + l1 * hg0 / l0, 0, 1);
            float hb1 = Mathf.Clamp(DarkestTint.b + l1 * hb0 / l0, 0, 1);

            RenderSettings.skybox.SetColor("_Tint", new Color(hr1, hg1, hb1));

            float hl0 = MaxIntensity - MinIntensity;
            float hl1 = Mathf.Clamp(MinIntensity + l1 * hl0 / l0, 0, MaxIntensity);
            TheLight.intensity = hl1;
            TheLight.color = new Color(hr1, hg1, hb1);
        }
        else
        {
            float l0 = (12 * 60 * 60 - SunRise);
            float l1 = (CurrentTime - SunRise);
            float h0 = BrighestExposure - DarkestExposure;
            float h1 = DarkestExposure + l1 * h0 / l0;
            RenderSettings.skybox.SetFloat("_Exposure", h1);

            float hr0 = BrighestTint.r - DarkestTint.r;
            float hb0 = BrighestTint.b - DarkestTint.b;
            float hg0 = BrighestTint.g - DarkestTint.g;

            float hr1 = Mathf.Clamp(DarkestTint.r + l1 * hr0 / l0, 0, 1);
            float hg1 = Mathf.Clamp(DarkestTint.g + l1 * hg0 / l0, 0, 1);
            float hb1 = Mathf.Clamp(DarkestTint.b + l1 * hb0 / l0, 0, 1);
            Debug.Log("red: " + hr1);
            RenderSettings.skybox.SetColor("_Tint", new Color(hr1, hg1, hb1));

            float hl0 = MaxIntensity - MinIntensity;
            float hl1 = Mathf.Clamp(MinIntensity + l1 * hl0 / l0, 0, MaxIntensity);
            TheLight.intensity = hl1;
            TheLight.color = new Color(hr1, hg1, hb1);
        }
    }
}
