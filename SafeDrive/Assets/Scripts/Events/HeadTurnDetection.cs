using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeadTurnDetection : EventScript
{
    public float TurnHeadThreshold = 15;
    private CamController head;

    private void Awake()
    {
        EventType = EventTypes.HeadTurn;
    }
    public override void Initialize()
    { 
        head = FindObjectOfType<CamController>();
        Pass = false;
        Completed = false;
    }

    private void Update()
    {
        if (!Completed)
        {
            if(head.Yaw > TurnHeadThreshold || head.Yaw < -TurnHeadThreshold){
                Pass = true;
                Completed = true;
            }
        }
    }
}
