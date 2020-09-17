using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrakeDetection : EventScript
{
    public DashHandler Dash;
    public override bool Pass { get { return Dash.GetHandbrake(); } set {; } }

    public override void Initialize()
    {
        Dash = FindObjectOfType<DashHandler>();
    }
    private void Awake()
    {
        EventType = EventTypes.Brake;
    }
}