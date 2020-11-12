using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrafficLightTrigger : EventScript
{
    public TrafficLight LightToTrigger;

    public override void Initialize()
    {
        LightToTrigger.RedWaitUntilTriggered = false;
        Completed = true;
        Pass = true;
    }

    
}
