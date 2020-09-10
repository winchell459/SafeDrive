using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrakeDetection : EventScript
{
    public DashHandler Dash;
    public override bool Pass { get {return Dash.GetHandbrake(); } set {; } }

    private void Awake()
    {
        EventType = EventTypes.Brake;
    }
    public override void Initialize()
    {
        Completed = true;
        Dash = FindObjectOfType<DashHandler>();
    }
}
