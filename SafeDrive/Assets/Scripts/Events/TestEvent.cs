using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestEvent : MonoBehaviour
{
    public EventScript[] events;
    public TestEvent NextEvent, PrevEvent;
    
    public CollisionDetection carCollisionEvent;
    private AreaDetection areaDetector;
    public bool Initialized = false;
    public bool CheckEngineOn = false;
    public bool EventCompleted = false;

    
    private bool eventsInitialized = false;

    public ScoreCard score;
    public struct ScoreCard
    {
        public int Total;
        public int Score;
        public string Labels;
        public string Values;
    }

    // Start is called before the first frame update
    void Start()
    {
        
        setupEvents();
        if (areaDetector) areaDetector.Initialize();
    }

    // Update is called once per frame
    void Update()
    {
        if (Initialized && !EventCompleted)
        {
            if(carCollisionEvent.Completed && !carCollisionEvent.Pass) //then we failed all tests
            {
                EventCompleted = true;
                score.Total = 0;
                score.Labels = "Collision Avoided \n\n Event Failed!";
                score.Values = "False";

            }
            if (areaDetector)
            {
                if(areaDetector.Completed && areaDetector.Pass)
                {
                    if(!eventsInitialized)
                    {
                        foreach (EventScript myEvent in events)
                        {
                            if(myEvent.EventType != EventScript.EventTypes.Area) myEvent.Initialize();
                        }
                        eventsInitialized = true;
                        if (NextEvent)
                        {
                            NextEvent.Initialized = true;
                            NextEvent.PrevEvent = this;
                        }
                    }
                    if (isEventComplete() || (CheckEngineOn && !FindObjectOfType<DriverHandler>().EngineState()))
                    {
                        score = scoreEvent();
                        EventCompleted = true;
                    }
                }
            }
            else
            {
                if (!eventsInitialized)
                {
                    foreach (EventScript myEvent in events)
                    {
                        if (myEvent.EventType != EventScript.EventTypes.Area) myEvent.Initialize();
                    }
                    eventsInitialized = true;
                }
                if (isEventComplete() || (CheckEngineOn && !GetComponent<DriverHandler>().EngineState()))
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

    private ScoreCard scoreEvent()
    {
        ScoreCard card = new ScoreCard();
        card.Labels = "Collision Avoided \n\n";
        card.Values = "True \n\n";
        float score = 0;
        float total = 0;
        foreach (EventScript myEvent in events)
        {

            if (myEvent.Pass)
            {
                score += myEvent.Weight;
                Debug.Log(myEvent.EventType + " Passed");
            }

            if(myEvent.Weight > 0)
            {
                card.Labels += myEvent.Label + "\n";
                card.Values += (myEvent.Pass ? myEvent.Weight.ToString() : "0") + "/"+ myEvent.Weight.ToString() + "\n"; // 60/60 or 0/60
            }
            else if(myEvent.IncludeInScoreCard)
            {
                card.Labels += myEvent.Label + "\n";
                card.Values += myEvent.Pass.ToString() + "\n";
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
                    Debug.Log(myEvent.EventType + " Passed");
                }

                if (myEvent.Weight > 0)
                {
                    card.Labels += myEvent.Label + "\n";
                    card.Values += (myEvent.Pass ? myEvent.Weight.ToString() : "0") + "/" + myEvent.Weight.ToString() + "\n"; // 60/60 or 0/60
                }
                else if (myEvent.IncludeInScoreCard)
                {
                    card.Labels += myEvent.Label + "\n";
                    card.Values += myEvent.Pass.ToString() + "\n";
                }

                total += myEvent.Weight;
            }
            prevEvent = prevEvent.PrevEvent;
        }
        card.Score = (int)score;
        card.Total = (int)total;
        return card;
    }

    private void setupEvents()
    {
        events = GetComponents<EventScript>();
        carCollisionEvent = GameObject.FindGameObjectWithTag("Player").GetComponent<CollisionDetection>();
        //EventScript[] newEvents = new EventScript[events.Length + 1];

        //int i = 0;
        foreach (EventScript myEvent in events)//foreach(DataType variableName in Array/List)
        {
            //newEvents[i] = myEvent;
            //i += 1;
            if (myEvent.EventType == EventScript.EventTypes.Area) areaDetector = (AreaDetection)myEvent;
        }
        //newEvents[i] = carCollisionEvent;
        //events = newEvents;
    }

    public void SetEvents(EventScript[] events)
    {
        this.events = events;
    }
}
