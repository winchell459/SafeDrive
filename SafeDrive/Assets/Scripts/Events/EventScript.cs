using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EventScript : MonoBehaviour
{
    public virtual bool Pass { get { return Pass; } set { Pass = value; } }
    
    public bool Completed = false;

    public abstract void Initialize();
}
