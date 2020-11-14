using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BusWaitDetection : EventScript
{
    public Animator BusAnimator;
    private bool flashing;
    private float flashingStarted;
    public float FlashingDuration = 5;

    public AreaDetection AreaDetector;
    public override void Initialize()
    {
        StartBlinking();
        Pass = false;
    }


    // Update is called once per frame
    void Update()
    {
        if(flashing && Time.time > flashingStarted + FlashingDuration && !AreaDetector.Completed)
        {
            Pass = true;
            StopFlashing();
        }
    }

    void StartBlinking()
    {
        flashing = true;
        flashingStarted = Time.time;
        BusAnimator.SetBool("RedLightBlinking", true);
        AreaDetector.EnterPass = false;
    }

    void StopFlashing()
    {
        flashing = false;
        BusAnimator.SetBool("RedLightBlinking", false);
        AreaDetector.EnterPass = true;
    }
}
