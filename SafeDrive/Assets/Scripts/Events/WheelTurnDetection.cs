using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityStandardAssets.Vehicles.Car;

public class WheelTurnDetection : EventScript
{
    /*
     * - x angle on car vehicle when pointing upwards
     * + x angle on car vehicle when pointing downwards
     * 
     * 
     */
    public float WheelTurnThreshold = 30;
    private Transform car;

    private void Awake()
    {
        EventType = EventTypes.Wheel;
    }
    public override bool Pass { get => CheckWheelAngle(); set => base.Completed = value; }

    public override void Initialize()
    {
        car = FindObjectOfType<CarUserControl>().transform;
        Completed = true;
    }

    private bool CheckWheelAngle()
    {
        bool pass = false;

        float angle = car.localEulerAngles.x;
        if (angle >= 180) angle -= 360;
        if (angle < 0) //car is pointed uphill
        {
            if (FindObjectOfType<DashHandler>().GetWheelAngle() > WheelTurnThreshold)
            {
                pass = true;
            }

        }
        else //car is pointed downhill
        {
            if (FindObjectOfType<DashHandler>().GetWheelAngle() < -WheelTurnThreshold)
            {
                pass = true;
            }
        }

        return pass;
    }

    /*private void Update()
    {
        if (!car) Initialize();

        Debug.Log(car.localEulerAngles.x);
    }     */
}
