using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VelocityDetection : EventScript
{
    public DashHandler Dash;
    public float TargetVelocity = 0.01f;
    public float TimeLimit = 1;
    private bool initialized = false;
    private float timeLimitStart;
    private bool timedStarted;
    //public override bool Pass { get { return Dash.GetSpeed(); } set {; } }

    private void Awake()
    {
        EventType = EventTypes.Velocity;
    }
    public override void Initialize()
    {
        Dash = FindObjectOfType<DashHandler>();
        initialized = true;
    }

    private void Update()
    {
        if (initialized && !Completed)
        {
            if (!timedStarted )
            {
                if(Dash.GetSpeed() > TargetVelocity - 0.1f && Dash.GetSpeed() < TargetVelocity + 0.1f)
                {
                    timedStarted = true;
                    timeLimitStart = Time.fixedTime;
                }
            }

            if (timedStarted)
            {
                //Debug.Log(Dash.GetSpeed());
                if (Dash.GetSpeed() > TargetVelocity - 0.1f && Dash.GetSpeed() < TargetVelocity + 0.1f)
                {
                    if(TimeLimit + timeLimitStart < Time.fixedTime)
                    {
                        Completed = true;
                        Pass = true;
                    }
                }
                else
                {
                    timedStarted = false;
                }
            }
        }
    }
}
