using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class AICarInitialize : EventScript
{
    public AICar Car;
    public override void Initialize()
    {
        Car.MovementPause(false);
        Pass = true;
        Completed = true;
    }
}
