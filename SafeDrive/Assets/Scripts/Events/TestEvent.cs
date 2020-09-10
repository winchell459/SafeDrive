using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestEvent : MonoBehaviour
{
    public EventScript[] events;
    public CollisionDetection carCollisionEvent;
    private AreaDetection areaDetector;
    public bool Initialized = false;
    // Start is called before the first frame update
    void Start()
    {
        setupEvents();

    }

    // Update is called once per frame
    void Update()
    {
        if (Initialized)
        {
            if (areaDetector)
            {

            }
            else
            {
                bool complete = true;
                bool pass = true;
                foreach(EventScript myEvent in events)
                {
                    if(myEvent.Completed)
                    {

                    }
                }
            }
        }
    }

    private void setupEvents()
    {
        events = GetComponents<EventScript>();
        carCollisionEvent = GameObject.FindGameObjectWithTag("Player").GetComponent<CollisionDetection>();
        EventScript[] newEvents = new EventScript[events.Length + 1];

        int i = 0;
        foreach (EventScript myEvent in events)//foreach(DataType variableName in Array/List)
        {
            newEvents[i] = myEvent;
            i += 1;
            if (myEvent.EventType == EventScript.EventTypes.Area) areaDetector = (AreaDetection)myEvent;
        }
        newEvents[i] = carCollisionEvent;
        events = newEvents;
    }
}
