using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestEvent : MonoBehaviour
{
    public EventScript[] events;
    public CollisionDetection carCollisionEvent;
    private AreaDetection areaDetector;
    public List<TestEvent> NextEvents;
    public TestEvent NextEvent, PrevEvent;
    public bool Initialized = false;
    public bool CheckEngine = false;
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
        if (NextEvent && !NextEvents.Contains(NextEvent)) NextEvents.Add(NextEvent);
        setupEvents();
        if (areaDetector) areaDetector.Initialize();
    }
                                                                                        
    // Update is called once per frame
    //Used to control sequence of events (initiates events in a certain order, activates or deactivates events)
    void Update()
    {
        if (Initialized && !EventCompleted)
        {
            if (!carCollisionEvent) carCollisionEvent = GameObject.FindGameObjectWithTag("Player").GetComponent<CollisionDetection>();
            else if (carCollisionEvent.Completed && !carCollisionEvent.Pass) //then fail all tests
            {
                EventCompleted = true;
                score.Total = 0;
                score.Labels = "Avoided Collisions \n\n Event Failed!";
                score.Values = "False";
            }
            if (areaDetector)
            {
                if(areaDetector.Completed && (areaDetector.EnterPass && areaDetector.Pass || !areaDetector.EnterPass && !areaDetector.Pass)) //enterPass && pass || !enterPass && !pass
                {
                    handleEvents();
                }
            }
            else
            {
                handleEvents();
            }
        }
    }
    void handleEvents()
    {
        /* bool complete = true;
                 bool pass = true;
                 foreach(EventScript myEvent in events)  */
        //(T || (...)) or (F || T && T)
        if (!eventsInitialized)
        {
            InitializeEvents();
        }
        if (isEventComplete() || (CheckEngine && !FindObjectOfType<DriverHandler>().EngineState()))
        {
            score = scoreEvent();
            EventCompleted = true;
        }
    }
    private void InitializeEvents()
    {
        //initializes detectors
        foreach (EventScript myEvent in events)
        {
            if (myEvent.EventType != EventScript.EventTypes.Area || myEvent != areaDetector) myEvent.Initialize();
        }
        eventsInitialized = true;

        //initializes next events
        foreach(TestEvent nextEvent in NextEvents)
        {
            nextEvent.Initialized = true;
            nextEvent.PrevEvent = this;
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
        card.Labels = "Avoided Collsions \n\n";
        card.Values = "True \n\n";
        float score = 0;
        float total = 0;
        foreach (EventScript myEvent in events)
        {

            if (myEvent.Pass)
            {
                score += myEvent.Weight;
           
            }

            if(myEvent.Weight > 0)
            {
                card.Labels += myEvent.Label + "\n";
                card.Values += (myEvent.Pass ? myEvent.Weight.ToString() : "0") + "/" + myEvent.Weight.ToString() + "\n"; // 60/60 or 0/60
            }
            else  if(myEvent.IncludeInScoreCard)
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
        //Debug.Log(transform.name + " score:" + card.Score + "total: " + card.Total);
        return card;
    }

    private void setupEvents()  //purpose to add carCollisionEvent to list of events
    {
        events = GetComponents<EventScript>();
        //EventScript[] newEvents = new EventScript[events.Length + 1];

        //int i = 0;
        foreach (EventScript MyEvents in events)
        {
            //newEvents[i] = events[i];
            //i += 1;
            if (MyEvents.EventType == EventScript.EventTypes.Area && !((AreaDetection)MyEvents).notAreaDetector) areaDetector = (AreaDetection)MyEvents;
        }
        //newEvents[i] = carCollisionEvent;  
        //events = newEvents;
    }
}
