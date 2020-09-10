using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestEvent : MonoBehaviour
{
    public EventScript[] events;
    // Start is called before the first frame update
    void Start()
    {
        events = GetComponents<EventScript>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
