using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlinkerDetection : EventScript
{
    
    public DashHandler.Indicator Indicator = DashHandler.Indicator.right;
    private bool initialized;
    private DashHandler dash;
    public override void Initialize()
    {
        Completed = false;
        Pass = false;
        initialized = true;
    }

    private void Start()
    {
        dash = FindObjectOfType<DashHandler>();
    }

    // Update is called once per frame
    void Update()
    {
        if (initialized)
        {
            if (dash.GetIndicator(Indicator))
            {
                Pass = true;
                Completed = true;
            }
        }
    }
}
