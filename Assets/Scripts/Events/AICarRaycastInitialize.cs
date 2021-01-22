using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AICarRaycastInitialize : MonoBehaviour
{
    public Transform target;
    public float DistanceDetection = 50;
    public TestEvent EventToTrigger;
    private bool eventTriggered = false;
    private bool findTarget = false;
    public Transform RaycastOrigin;


    public void StartDetection()
    {
        findTarget = true;
        eventTriggered = false;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }
    
    // Update is called once per frame
    void Update()
    {
        if(findTarget && !eventTriggered)
        {
            RaycastHit hit;
            if (Physics.Raycast(RaycastOrigin.position, (target.position - RaycastOrigin.position).normalized, out hit))
            {
                Debug.Log("RaycastDistance: " + hit.transform.name + " " + hit.distance);
                if (hit.transform.CompareTag("Player") && hit.distance < DistanceDetection)
                {
                    EventToTrigger.Initialized = true;
                    eventTriggered = true;
                }
            }
        }
    }
}
