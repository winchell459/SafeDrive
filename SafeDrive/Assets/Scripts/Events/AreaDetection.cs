using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Net;
using UnityEngine;

public class AreaDetection : EventScript
{
    private bool initialized;
    public string otherTag = "Player";
    public bool EnterPass = true; //determines whether or not area should be entered
    public override void Initialize()
    {
        initialized = true;
        Pass = true;
        //if (EnterPass) Completed = true;
    }

    private void Awake()
    {
        EventType = EventTypes.Area;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(otherTag) && initialized)
        {
            if (!EnterPass && !Completed)
            {
                Pass = false;
                Completed = true;
            }      
            else if (/*!Completed &&*/ EnterPass)
            {
                Pass = true;
                Completed = true;
            }
        }
    } 

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag(otherTag) && initialized)
        {
            if (EnterPass /*&& !Completed*/)
            {
                Pass = false;
                Completed = false;
            }
        }
    }
}
