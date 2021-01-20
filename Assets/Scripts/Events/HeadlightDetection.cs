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
    private void Awake()
    {
        EventType = EventTypes.Headlight;
    }
    public LightStates TargetState;
    public float LeewayTime = 5;
    private float timerStart;

    private bool initialized = false;

    public Transform AICar;
    public Transform Player;
    public float LeewayDistance = 50;
    public bool isInRange;

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
            if (!isInRange && Vector3.Distance(Player.position, AICar.position) < 50)
            {
                isInRange = true;
            }
            if (isInRange)
            {
                Vector3 displacement1 = Player.position - AICar.position;
                float dotP1 = Vector3.Dot(displacement1, AICar.forward);
                Vector3 displacement2 = AICar.position - Player.position;
                float dotP2 = Vector3.Dot(displacement2, Player.forward);//Player.forward = velocity
                if (dotP1 < 0 && dotP2 < 0)
                {
                    if (TargetState == LightStates.Off)
                    {
                        if (!Dash.LowBeamsOn() && !Dash.HighBeamsOn())
                        {
                            Completed = true;
                            Pass = true;
                        }
                    }
                    else if (TargetState == LightStates.On)
                    {
                        if (Dash.LowBeamsOn() || Dash.HighBeamsOn())
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
                    else
                    {
                        Completed = true;
                        Pass = false;
                    }
                }
            }
            Debug.Log("isInRange: " + isInRange);
        //    if(LeewayTime + timerStart < Time.fixedTime)
        //    {
        //        Completed = true;
        //        Pass = false;
        //    }
        //    else
        //    {
        //        if(TargetState == LightStates.Off)
        //        {
        //            if(!Dash.LowBeamsOn() && !Dash.HighBeamsOn())
        //            {
        //                Completed = true;
        //                Pass = true;
        //            }
        //        }else if (TargetState == LightStates.On)
        //        {
        //            if (Dash.LowBeamsOn() || Dash.HighBeamsOn())
        //            {
        //                    Completed = true;
        //                    Pass = true;
        //            }
        //        }
        //        else if (TargetState == LightStates.LowBeamsOn)
        //        {
        //            if (Dash.LowBeamsOn())
        //            {
        //                Completed = true;
        //                Pass = true;
        //            }
        //        }
        //        else if (TargetState == LightStates.HighBeamsOff)
        //        {
        //            if (!Dash.HighBeamsOn())
        //            {
        //                Completed = true;
        //                Pass = true;
        //            }
        //        }
        //    }
        }
    }
}
