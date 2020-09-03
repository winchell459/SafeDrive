﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EventScript : MonoBehaviour
{
    public virtual bool Pass {
        get { return _pass; }
        set { _pass = value; }
    }
    public bool _pass;
    public bool _completed;
    
    public virtual bool Completed { 
        get { return _completed; }
        set { _completed = value; }
    }

    public abstract void Initialize();
}
