﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaDetection : EventScript
{
    private bool initialized;
    public string otherTag = "Player";
    public bool EnterPass = true;
    public override void Initialize()
    {
        initialized = true;
        Pass = true;
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
            else if(!Completed && EnterPass)
            {
                Pass = true;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.CompareTag(otherTag) && initialized)
        {
            if(!Completed && EnterPass)
            {
                Pass = false;
            }
        }
    }

}
