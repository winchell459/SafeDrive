using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeadlightDetection : EventScript
{
    public DashHandler Dash;
    //public override bool Pass { get { return Dash.(); } set {; } }
    public enum LightStates
    {
        Off,
        On,
        LowBeamsOn,
        HighBeamsOff
    }
    public LightStates TargetState;
    public float LeewayTime = 5;
    private float timerStart;

    private bool initialized = false;

    private void Awake()
    {
        EventType = EventTypes.Headlight;
    }
    public override void Initialize()
    {
        Dash = FindObjectOfType<DashHandler>();
        timerStart = Time.fixedTime;
        initialized = true;
    }

    private void Update()
    {
        if(!Completed && initialized)
        {
            if(LeewayTime + timerStart < Time.fixedTime)
            {
                Completed = true;
                Pass = false;
            }
            else
            {
                if(TargetState == LightStates.Off)
                {
                    if(!Dash.LowBeamsOn() && !Dash.HighBeamsOn())
                    {
                        Completed = true;
                        Pass = true;
                    }
                }
                else if(TargetState == LightStates.On)
                {
                    if(Dash.LowBeamsOn() || Dash.HighBeamsOn())
                    {
                        Completed = true;
                        Pass = true;
                    }
                }
                else if (TargetState == LightStates.LowBeamsOn)
                {
                    if (Dash.LowBeamsOn())
                    {
                        Completed = true;
                        Pass = true;
                    }
                }
                else if (TargetState == LightStates.HighBeamsOff)
                {
                    if (!Dash.HighBeamsOn())
                    {
                        Completed = true;
                        Pass = true;
                    }
                }
            }
        }
    }
}
