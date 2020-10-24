using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeadTurnDetection : EventScript
{
    public float TurnHeadThreshold = 15;
    private CameraController head;

    public override void Initialize()
    {
        //throw new System.NotImplementedException();  can be used as a reminder to write code later
        head = FindObjectOfType<CameraController>();
        Pass = false;
        Completed = false;
    }
    private void Awake()
    {
        EventType = EventTypes.HeadTurn;
    }
    private void Update()
    {
        if (!Completed)
        {
            if (head.Yaw > TurnHeadThreshold || head.Yaw < -TurnHeadThreshold)
            {
                Pass = true;
                Completed = true;
            }
        
        }  
    }
}
