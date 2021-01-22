using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class AICarInitialize : EventScript
{
    public AICar[] Cars;
    public override void Initialize()
    {
        foreach (AICar Car in Cars)
        {
            Car.gameObject.SetActive(true);
            Car.MovementPause(false);
            Pass = true;
            Completed = true;
            Car.GetComponent<AICarRaycastInitialize>().StartDetection();
        }
    }
}
