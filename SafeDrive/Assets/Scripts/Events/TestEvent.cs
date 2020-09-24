using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestEvent : MonoBehaviour
{
    public EventScript[] events;
    public CollisionDetection carCollisionEvent;
    private AreaDetection areaDetector;
    public TestEvent NextEvent, PrevEvent;
    public bool Initialized = false;
    public bool CheckEngine = false;
    public bool EventCompleted = false;

    public float score;
    private bool eventsInitialized = false;
    // Start is called before the first frame update
    void Start()
    {
        setupEvents();
        if (areaDetector) areaDetector.Initialize();
    }

    // Update is called once per frame
    //Used to control sequence of events (initiates events in a certain order, activates or deactivates events)
    void Update()
    {
        if (Initialized && !EventCompleted)
        {
            if (carCollisionEvent.Completed && !carCollisionEvent.Pass) //then fail all tests
            {
                EventCompleted = true;
                score = 0;
            }
            if (areaDetector)
            {
                if(areaDetector.Completed && areaDetector.Pass)
                {
                    if (!eventsInitialized)
                    {
                        foreach (EventScript myEvent in events)
                        {
                            if (myEvent.EventType != EventScript.EventTypes.Area) myEvent.Initialize();
                        }
                        eventsInitialized = true;
                        if (NextEvent)
                        {
                            NextEvent.Initialized = true;
                            NextEvent.PrevEvent = this;
                        }
                    }
                    if (isEventComplete() || (CheckEngine && !FindObjectOfType<DriverHandler>().EngineState()))
                    {
                        score = scoreEvent();
                        EventCompleted = true;
                    }
                }
            }
            else
            {
                /* bool complete = true;
                 bool pass = true;
                 foreach(EventScript myEvent in events)  */
                //(T || (...)) or (F || T && T)
                if (!eventsInitialized)
                {
                    foreach (EventScript myEvent in events)
                    {
                        if (myEvent.EventType != EventScript.EventTypes.Area) myEvent.Initialize();
                    }
                    eventsInitialized = true;
                }
                if (isEventComplete() || (CheckEngine && !FindObjectOfType<DriverHandler>().EngineState()))
                {
                    score = scoreEvent();
                    EventCompleted = true;
                } 
            }
        }
    }

    private bool isEventComplete()
    {
        bool complete = true;
        foreach (EventScript myEvent in events)
        {
            if (!myEvent.Completed) complete = false; 
        }
        return complete;
    }
    private float scoreEvent()
    {
        float score = 0;
        float total = 0;
        foreach (EventScript myEvent in events)
        {
            if (myEvent.Pass)
            {
                score += myEvent.Weight;
           
            }
            total += myEvent.Weight;
        }
 
        TestEvent prevEvent = PrevEvent;

        while (prevEvent)
        {
            foreach (EventScript myEvent in prevEvent.events)
            {
                if (myEvent.Pass)
                {
                    score += myEvent.Weight;

                }
                total += myEvent.Weight;
            }
            prevEvent = prevEvent.PrevEvent;
        }
        return score / total;
    }

    private void setupEvents()  //purpose to add carCollisionEvent to list of events
    {
        events = GetComponents<EventScript>();
        carCollisionEvent = GameObject.FindGameObjectWithTag("Player").GetComponent<CollisionDetection>();
        //EventScript[] newEvents = new EventScript[events.Length + 1];

        //int i = 0;
        foreach (EventScript MyEvents in events)
        {
            //newEvents[i] = events[i];
            //i += 1;
            if (MyEvents.EventType == EventScript.EventTypes.Area) areaDetector = (AreaDetection)MyEvents;
        }
        //newEvents[i] = carCollisionEvent;  
        //events = newEvents;
    }
}
