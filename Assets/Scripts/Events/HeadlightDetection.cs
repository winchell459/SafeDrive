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

    public Transform[] AICars;
    public Transform Player;
    public float LeewayDistance = 50;
    public bool Failed;

    public override void Initialize()
    {
        Dash = FindObjectOfType<DashHandler>();
        timerStart = Time.fixedTime;
        initialized = true;
        Completed = true;
    }

    private void Update()
    {
        if(/*!Completed && */ initialized && !Failed)
        {
            foreach (Transform AICar in AICars)
            {
                bool isInRange = false;
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
                            else
                            {
                                failTest();
                            }
                        }
                        else if (TargetState == LightStates.On)
                        {
                            if (Dash.LowBeamsOn() || Dash.HighBeamsOn())
                            {
                                Completed = true;
                                Pass = true;
                            }
                            else
                            {
                                failTest();
                            }
                        }
                        else if (TargetState == LightStates.LowBeamsOn)
                        {
                            if (Dash.LowBeamsOn())
                            {
                                Completed = true;
                                Pass = true;
                            }
                            else
                            {
                                failTest();
                            }
                        }
                        else if (TargetState == LightStates.HighBeamsOff)
                        {
                            if (!Dash.HighBeamsOn())
                            {
                                Completed = true;
                                Pass = true;
                            }
                            else
                            {
                                failTest();
                            }
                        }
                        else
                        {
                            failTest();
                        }
                    }
                }
                if (Failed) break;
                Debug.Log("isInRange: " + isInRange);
            }
        }
    }

    private void failTest()
    {
        Pass = false;
        Failed = true;
    }
}
